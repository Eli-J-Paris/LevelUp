using LevelUp.DataAccess;
using LevelUp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace LevelUp.Controllers
{
    public class CampsController : Controller
    {
        private readonly LevelUpContext _context;
        public CampsController(LevelUpContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [Route("/camps")]
        public IActionResult CampHome()
        {
            var user = GetActiveUser(Request);
            if (user == null)
            {
                Log.Error("User returned null");
                return Redirect("/users/login");
            }

            return View(user);
        }

        [Route("/camps/new")]
        public IActionResult New()
        {
            return View();
        }

        [HttpPost]
        [Route("/camps/create")]
        public IActionResult Create(Camp camp)
        {
            if (ModelState.IsValid)
            {
                var user = GetActiveUser(Request);
                if (user == null)
                {
                    Log.Error("User returned null");
                    return Redirect("/users/login");
                }

                _context.Camps.Add(camp);
                camp.Members.Add(user);
                camp.Owner = user.Username;

                //user.Camps.Add(camp);
                _context.SaveChanges();
                return Redirect("/camps");
            }
            else
            {
                return View("New", camp);
            }
        }

        [Route("/camps/{campId:int}")]
        public IActionResult Show(int campId)
        {
            var camp = _context.Camps.Where(c => c.Id == campId).Include(c => c.MessageBoard).Include(c => c.Members).First();
           
            if (camp == null)
            {
                Log.Error("Camp returned null");
                return NotFound();
            }

            var user = GetActiveUser(Request);
            if (user == null)
            {
                Log.Error("User returned null");
                return Redirect("/users/login");
            }
            ViewData["ActiveUser"] = user;

            return View(camp);
        }

        [HttpPost]
        [Route("/camps/{campId:int}/addmessage")]
        public IActionResult AddMessage(int campId, string message)
        {
            var newmessage = new Message(message);

            var user = GetActiveUser(Request);
            if (user == null)
            {
                Log.Error("User returned null");
                return Redirect("/users/login");
            }


            newmessage.User = user;
            var camp = _context.Camps.Where(c=>c.Id == campId).Include(c => c.MessageBoard).First();

            if (camp == null)
            {
                Log.Error("Camp returned null");
                return NotFound();
            }

            camp.MessageBoard.Add(newmessage);
            _context.SaveChanges();

            return Redirect($"/camps/{campId}");
        }


        [Route("/camps/search")]
        public IActionResult Search(string query)
        {
            var camps = _context.Camps.Where(c => c.Title.Contains(query)).ToList();

            if (camps == null)
            {
                Log.Error("Camps returned null");
                return NotFound();
            }

            return View(camps);
        }

        [Route("/camps/{campId:int}/join")]
        public IActionResult Join(int campId)
        {
            var camp = _context.Camps.Find(campId);

            if (camp == null)
            {
                Log.Error("Camp returned null");
                return NotFound();
            }

            var user = GetActiveUser(Request);
            if (user == null) 
            {
                Log.Error("User returned null");
                return Redirect("/users/login");
            }


            camp.Members.Add(user);
            _context.SaveChanges();
            return Redirect($"/camps/{campId}");
        }


        [Route("/camps/{campId:int}/details")]
        public IActionResult Details(int campId)
        {
            var user = GetActiveUser(Request);
            if (user == null)
            {
                Log.Error("User returned null");
                return Redirect("/users/login");
            }

            ViewData["Owner"] = user.Username;
            ViewData["ActiveUser"] = user;

            var camp = _context.Camps.Where(c => c.Id == campId).Include(c => c.MessageBoard).Include(c => c.Members).First();

            if (camp == null)
            {
                Log.Error("Camp returned null");
                return NotFound();
            }

            return View(camp);
        }

        [HttpGet]
        [Route("/camps/{campId:int}/delete")]
        public IActionResult DeleteView(int campId)
        {
            var camp = _context.Camps.Where(c => c.Id == campId).Include(c => c.MessageBoard).Include(c => c.Members).First();

            if (camp == null)
            {
                Log.Error("Camp returned null");
                return NotFound();
            }

            return View(camp);
        }


        [HttpPost]
        [Route("/camps/{campId:int}/delete")]
        public IActionResult Delete(int campId)
        {
            var camp = _context.Camps.Where(c => c.Id == campId).Include(c => c.MessageBoard).First();

            if (camp == null)
            {
                Log.Error("Camp returned null");
                return NotFound();
            }

            _context.Camps.Remove(camp);
            _context.SaveChanges();
            return Redirect("/camps");
        }


        [HttpPost]
        [Route("/camps/{campId:int}/{userId:int}/leave")]
        public IActionResult Leave(int campId, int userId)
        {
            var camp = _context.Camps.Where(c => c.Id == campId).Include(c => c.MessageBoard).Include(c => c.Members).First();

            if (camp == null)
            {
                Log.Error("Camp returned null");
                return NotFound();
            }

            var user = _context.Users.Find(userId);

            if (user == null)
            {
                Log.Error("User returned null");
                return NotFound();
            }

            camp.Members.Remove(user);
            _context.SaveChanges();
            return Redirect("/camps");
        }

        private User? GetActiveUser(HttpRequest request)
        {
            var userId = Convert.ToInt32(request.Cookies["activeUser"]);
            var userAuth = request.Cookies["userAuth"];

            User? user = _context.Users.Include(u => u.DailyTasks).Include(u => u.WeeklyTasks).Include(u => u.ToDoTasks).Include(u =>u.Camps).ThenInclude(u => u.MessageBoard).FirstOrDefault(u => u.Id == userId);

            if (user != null)
            {
                if (user.Encrypt(user.Username) != userAuth)
                {
                    user = null;
                }
            }

            return user;
        }
    }
}
