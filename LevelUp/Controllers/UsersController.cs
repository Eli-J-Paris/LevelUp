using LevelUp.DataAccess;
using LevelUp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.TagHelpers.Cache;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using OpenAI_API.Images;
using OpenAI_API;
using System.Collections;
using Serilog;

namespace LevelUp.Controllers
{
    public class UsersController : Controller
    {
        private readonly LevelUpContext _context;
        private readonly IConfiguration _configuration;
        public UsersController(LevelUpContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(User user)
        {
            User validateUser;
            try
            {
                validateUser = _context.Users.Where(u => u.Username == user.Username).First();
            }
            catch (InvalidOperationException)
            {
                validateUser = null;
            }
            if (validateUser == null)
            {
                Response.Cookies.Append("name", user.Name);
                user.PfpUrl = user.GetAIGeneratedAvatar(_configuration["LEVELUP_APICONNECTIONKEY"]).Result;
                user.Password = user.Encrypt(user.Password);
                
                _context.Users.Add(user);
                _context.SaveChanges();

                Log.Information("User successfully created and saved to database.");

                Response.Cookies.Append("activeUser", user.Id.ToString());
                Response.Cookies.Append("userAuth", user.Encrypt(user.Username));

                return Redirect("/tutorial");
            }
            else
            {
                TempData["FailedLogin"] = true;
       
                return Redirect("/users/signup");
            }
        }

        [Route("/tutorial")]
        public IActionResult Tutorial()
        {
            return View();
        }

        [HttpGet("users/login")]
        public IActionResult LogIn(string username)
        {
                Response.Cookies.Append("name", "");
                Response.Cookies.Append("activeUser", "");
                Response.Cookies.Append("userAuth", "");
                ViewBag.Username = username;
 
            return View();
        }

        [HttpPost("users/login")]
        public IActionResult LogIn(User userToLogin)
        {
            try
            {
                 // Fetch the user with the given username from the database
                var user = _context.Users.FirstOrDefault(u => u.Username == userToLogin.Username);

                 // If the user exists and the password is correct
                if (user != null && VerifyPassword(user, userToLogin))
                {
                    Response.Cookies.Append("activeUser", user.Id.ToString());
                    Response.Cookies.Append("userAuth", user.Encrypt(user.Username));
                    Response.Cookies.Append("name", user.Name);
                    
                    return Json(new { success = true, redirectUrl = Url.Action("Profile", "Users") });
                }

                     // If login fails, send a JSON response.
                    return Json(new { success = false, message = "LogIn Failed" });
            }
            catch (Exception ex) // You can be more specific with the exception type if needed.
            {
                // Log the exception for diagnostic purposes. 
                // Use a logging framework like Serilog, NLog, etc.
                Log.Error(ex, "Error occurred during login");

                return Json(new { success = false, message = "An error occurred while processing your request." });
            }
        }

        private bool VerifyPassword(User userStored, User userToLogin)
        {
            var hashedPasswordBase64 = userStored.Encrypt(userToLogin.Password);
            return userStored.Password == hashedPasswordBase64;
        }

        private User? GetActiveUser(HttpRequest request)
        {
            var userId = Convert.ToInt32(request.Cookies["activeUser"]);
            var userAuth = request.Cookies["userAuth"];

            User? user = _context.Users.Include(u => u.DailyTasks).Include(u => u.WeeklyTasks).Include(u => u.ToDoTasks).Include(u =>u.Achievements)
                .ThenInclude(a =>a.Hygenie5Achievement)
                        .Include(a=> a.Achievements).ThenInclude(a =>a.HabitBuilding5Achievement)
                                .Include(a => a.Achievements).ThenInclude(a => a.Mindfulness5Achievement)
                                                .Include(a => a.Achievements).ThenInclude(a => a.Productivity5Achievement)
                                                                .Include(a => a.Achievements).ThenInclude(a => a.Wellness5Achievement)
                                                                .FirstOrDefault(u => u.Id == userId);

            if (user != null)
            {
                if (user.Encrypt(user.Username) != userAuth)
                {
                    user = null;
                }
            }
            else
            {
                Log.Error("User came back null.");
            }

            return user;
        }

        [Route("/profile")]
        public IActionResult Profile()
        {
            var user = GetActiveUser(Request);
            if (user == null) return Redirect("/users/login");

            var radarChartData = GetRadarChartData(user);
             
            _context.Users.Update(user);
            _context.SaveChanges();
            var viewModel = new UserProfileView
            {
                User = user,
                DailyAffirmation = _GetAffirmation().Result,
                RadarChart = radarChartData
            };

            user.Reset(_context, _configuration["LEVELUP_APICONNECTIONKEY"]);

            return View(viewModel);
        }

        [Route("/profile/streaks")]
        public IActionResult Streaks()
        {
            var user = GetActiveUser(Request);
            if (user == null) return Redirect("/users/login");
            return View(user);
        }

        [HttpPost]
        [Route("/users/checktask")]
        public IActionResult CheckTask(string? type, int? id)
        {
            var user = GetActiveUser(Request);
            if (user == null) return Redirect("/users/login");

            if (type == "daily")
            {
                var task = _context.DailyTasks.Include(t => t.User).FirstOrDefault(t => t.Id == id);
                if (!task.IsCompleted)
                {

                    task.Complete();
                    user.UpdateAchievement(task.Category, _context);
                }
                else
                {
                    task.UndoComplete();
                    user.UndoAchievement(task.Category,_context);
                }
                _context.DailyTasks.Update(task);
                _context.SaveChanges();
            }
            else if (type == "weekly")
            {
                var task = _context.WeeklyTasks.Include(t => t.User).FirstOrDefault(t => t.Id == id);
                if (!task.IsCompleted)
                {
                    task.Complete();
                    IncrementAtribute(task.Category, user);
                    user.UpdateAchievement(task.Category, _context);
                }
                else
                {
                    task.UndoComplete();
                    DeincrementAtribute(task.Category, user);
                    user.UndoAchievement(task.Category, _context);
                }
                _context.WeeklyTasks.Update(task);
                _context.SaveChanges();
            }
            else if (type == "todo")
            {
                var task = _context.ToDoTasks.Include(t => t.User).FirstOrDefault(t => t.Id == id);
                if (!task.IsCompleted)
                {
                    task.Complete();
                    user.UpdateAchievement(task.Category, _context);
                }
                else
                {
                    task.UndoComplete();
                    user.UndoAchievement(task.Category, _context);
                }
                _context.ToDoTasks.Update(task);
                _context.SaveChanges();
            }
            return Json(new { success = true, message = "task checked" }); ;
        }

        private void IncrementAtribute(string category, User user)
        {
            category = category.ToLower();
            if (category == "hygiene")
            {
                user.Hygiene += 1;
            }
            else if (category == "wellness")
            {
                user.Wellness += 1;
            }
            else if (category == "mindfulness")
            {
                user.Mindfullness += 1;
            }
            else if (category == "productivity")
            {
               user.Productivity += 1;
            }
            else if (category == "habitbuilding")
            {
               user.HabitBuilding += 1;
            }
        }

        private void DeincrementAtribute(string category, User user)
        {
            category = category.ToLower();
            if (category == "hygiene")
            {
                user.Hygiene -= 1;
            }
            else if (category == "wellness")
            {
                user.Wellness -= 1;
            }
            else if (category == "mindfullness")
            {
                user.Mindfullness -= 1;
            }
            else if (category == "productivity")
            {
                user.Productivity -= 1;
            }
            else if (category == "habitbuilding")
            {
                user.HabitBuilding -= 1;
            }
        }

        private readonly string _ApiCallForAffirmation = "https://www.affirmations.dev";

        private async Task<string> _GetAffirmation()
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.GetStringAsync(_ApiCallForAffirmation);
                    var jsonResponse = JObject.Parse(response);
                    Log.Information($"Api call to affirmations.dev successful: {jsonResponse["affirmation"].ToString()}");
                    return jsonResponse["affirmation"].ToString();
                }
            }
            catch (Exception ex)
            {
                Log.Warning(ex, "affirmations.dev not responding");
                return "Error Loading Affirmation";
            }
        }


        private RadarChart GetRadarChartData(User user)
        {
            return new RadarChart
            {
                Labels = new List<string>
            {
                "Hygiene",
                "Productivity",
                "Wellness",
                "Mindfullness",
                "HabitBuilding"
            },
            Values = new List<int>
            {
                user.Hygiene,
                user.Productivity,
                user.Wellness,
                user.Mindfullness,
                user.HabitBuilding
            }
            };
        }

        
    }
}
