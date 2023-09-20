using LevelUp.DataAccess;
using LevelUp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LevelUp.Controllers
{
    public class TasksController : Controller
    {
        private readonly LevelUpContext _context;
        public TasksController(LevelUpContext context)
        {
            _context = context;
        }

        [Route("/tasks")]
        public IActionResult TasksPage()
        {
            // checks cookies to make sure a user is logged in and gets user
            var user = GetActiveUser(Request);
            if (user == null) return Redirect("/");
            return View();
            //check id from cookie
        }


        [Route("/tasks/new")]
        public IActionResult NewTask(string tasktype)
        {
            ViewBag.TaskType = tasktype;
            return View();
        }

        [HttpPost]
        [Route("/tasks/create")]
        public IActionResult CreateTask(Task task)
        {
            //All the code for creating and adding a task
            return Redirect("/tasks");
        }

        private User? GetActiveUser(HttpRequest request)
        {
            var userId = Convert.ToInt32(request.Cookies["activeUser"]);
            var userAuth = request.Cookies["userAuth"];

            User? user = _context.Users.FirstOrDefault(u => u.Id == userId);

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
