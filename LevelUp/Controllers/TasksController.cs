using LevelUp.DataAccess;
using LevelUp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

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
            if (user == null) return Redirect("/users/login");
            return View(user);
            //check id from cookie
        }


        [Route("/tasks/new")]
        public IActionResult NewTask(string tasktype)
        {
            ViewBag.TaskType = tasktype;
            return View();
        }

        [HttpPost]
        [Route("/tasks/newdaily")]
        public IActionResult CreateDailyTask(DailyTask task)
        {
            task.XpReward = task.Difficulty;
            task.AttributeReward = 1;

            var user = GetActiveUser(Request);
            if (user == null) return Redirect("/users/login");

            user.DailyTasks.Add(task);
            _context.Users.Update(user);

            _context.SaveChanges();
            return Redirect("/tasks");
        }

        [HttpPost]
        [Route("/tasks/newweekly")]
        public IActionResult CreateWeeklyTask(WeeklyTask task)
        {
            task.XpReward = 3 * task.Difficulty;
            task.AttributeReward = 1;

            var user = GetActiveUser(Request);
            if (user == null) return Redirect("/users/login");

            user.WeeklyTasks.Add(task);
            _context.Users.Update(user);

            _context.SaveChanges();
            return Redirect("/tasks");
        }

        [HttpPost]
        [Route("/tasks/newtodo")]
        public IActionResult CreateToDoTask(ToDoTask task)
        {
            task.XpReward = task.Difficulty;
            task.AttributeReward = 1;

            var user = GetActiveUser(Request);
            if (user == null) return Redirect("/users/login");

            user.ToDoTasks.Add(task);
            _context.Users.Update(user);

            _context.SaveChanges();
            return Redirect("/tasks");
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

        [HttpGet]
        [Route("/tasks/{taskId:int}")]
        public IActionResult TaskShowPage(int taskId)
        {
            ITask task = null;
            if (Request.Cookies["tasktype"] == "daily")
            {
                 task = _context.DailyTasks.Find(taskId);
            }
            else if(Request.Cookies["tasktype"] == "weekly")
            {
                 task = _context.WeeklyTasks.Find(taskId);
            }
            else if(Request.Cookies["tasktype"] == "todo")
            {
                task = _context.ToDoTasks.Find(taskId);
            }
            return View(task);
        }

        [HttpPost]
        [Route("/tasks/delete/{taskId:int}")]
        public IActionResult Delete(int taskId)
        {
            if (Request.Cookies["tasktype"] == "daily")
            {
                var dailyTask = _context.DailyTasks.Find(taskId);
                _context.DailyTasks.Remove(dailyTask);
            }
            else if (Request.Cookies["tasktype"] == "weekly")
            {
                var weeklyTask = _context.WeeklyTasks.Find(taskId);
                _context.WeeklyTasks.Remove(weeklyTask);

            }
            else if (Request.Cookies["tasktype"] == "todo")
            {
                var todoTask = _context.ToDoTasks.Find(taskId);
                _context.ToDoTasks.Remove(todoTask);
            }
            _context.SaveChanges();
            return Redirect("/tasks");
        }
    }
}
