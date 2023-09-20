using LevelUp.DataAccess;
using LevelUp.Models;
using Microsoft.AspNetCore.Mvc;

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
            user.Password = user.Encrpyt(user.Password);
            _context.Users.Add(user);
            _context.SaveChanges();

            Response.Cookies.Append("activeUser", user.Id.ToString());
            Response.Cookies.Append("userAuth", user.Encrpyt(user.Username));

            return Redirect("/profile");
        }

        [Route("/profile")]
        public IActionResult Profile()
        {
            return View();
        }
    }
}
