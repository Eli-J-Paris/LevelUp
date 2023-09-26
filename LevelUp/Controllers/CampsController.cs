using LevelUp.DataAccess;
using LevelUp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            if (user == null) return Redirect("/users/login");
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
            var user = GetActiveUser(Request);
            if (user == null) return Redirect("/users/login");
            
            user.Camps.Add(camp);
            _context.SaveChanges();
            return Redirect("/camps");
        }

        [Route("/camps/{campId:int}")]
        public IActionResult Show(int campId)
        {
            var camp = _context.Camps.Where(c => c.Id == campId).Include(c => c.MessageBoard).ThenInclude(c=>c.User).First();
            return View(camp);
        }

        [HttpPost]
        [Route("/camps/{campId:int}/addmessage")]
        public IActionResult AddMessage(int campId, string message)
        {
            var newmessage = new Message(message);

            var user = GetActiveUser(Request);
            if (user == null) return Redirect("/users/login");

            newmessage.User = user;
            var camp = _context.Camps.Where(c=>c.Id == campId).Include(c => c.MessageBoard).First();
            camp.MessageBoard.Add(newmessage);
            _context.SaveChanges();

            return Redirect($"/camps/{campId}");
        }


        [Route("/camps/search")]
        public IActionResult Search(string query)
        {
            var camps = _context.Camps.Where(c => c.Title.Contains(query)).ToList();
            return View(camps);
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
