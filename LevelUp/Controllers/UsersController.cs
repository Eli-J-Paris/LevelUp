using LevelUp.DataAccess;
using LevelUp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.TagHelpers.Cache;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;


namespace LevelUp.Controllers
{
    public class UsersController : Controller
    {
        private readonly LevelUpContext _context;
        public UsersController(LevelUpContext context)
        {
            _context = context;
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
                user.Password = user.Encrypt(user.Password);
                _context.Users.Add(user);
                _context.SaveChanges();

                Response.Cookies.Append("activeUser", user.Id.ToString());
                Response.Cookies.Append("userAuth", user.Encrypt(user.Username));

                return Redirect("/profile");
            }
            else
            {
                TempData["FailedLogin"] = true;
                
                return Redirect("/users/signup");
            }
        }

        [HttpGet("users/login")]
        public IActionResult LogIn(string username)
        {
            Response.Cookies.Append("activeUser", "");
            Response.Cookies.Append("userAuth", "");
            ViewBag.Username = username;

            return View();
        }

        [HttpPost("users/login")]
        public IActionResult LogIn(User userToLogin)
        {
            // Fetch the user with the given username from the database
            var user = _context.Users.FirstOrDefault(u => u.Username == userToLogin.Username);

            // If the user exists and the password is correct
            if (user != null && VerifyPassword(user, userToLogin))
            {
                Response.Cookies.Append("activeUser", user.Id.ToString());
                Response.Cookies.Append("userAuth", user.Encrypt(user.Username));
                return Json(new { success = true, redirectUrl = Url.Action("Profile", "Users") });
            }

            // If login fails, send a JSON response.
            return Json(new { success = false, message = "LogIn Failed" });
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

            User? user = _context.Users.Include(u => u.DailyTasks).Include(u => u.WeeklyTasks).Include(u => u.ToDoTasks).FirstOrDefault(u => u.Id == userId);

            if (user != null)
            {
                if (user.Encrypt(user.Username) != userAuth)
                {
                    user = null;
                }
            }

            return user;
        }

        [Route("/profile")]
        public IActionResult Profile()
        {
            var user = GetActiveUser(Request);
            if (user == null) return Redirect("/users/login");
            user.Reset();
            return View(user);
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
            if(type == "daily")
            {
                var task = _context.DailyTasks.Include(t => t.User).FirstOrDefault(t => t.Id == id);
                if (!task.IsCompleted)
                {
                    task.Complete();
                }
                else
                {
                    task.UndoComplete();
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
                }
                else
                {
                    task.UndoComplete();
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
                }
                else
                {
                    task.UndoComplete();
                }
                _context.ToDoTasks.Update(task);
                _context.SaveChanges();
            }
            return Json(new { success = true, message = "task checked" }); ;
        }
    }
}
