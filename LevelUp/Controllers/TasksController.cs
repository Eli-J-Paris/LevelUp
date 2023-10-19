using LevelUp.DataAccess;
using LevelUp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Linq;
using System.Threading.Tasks;

namespace LevelUp.Controllers
{
    public class TasksController : Controller
    {
        private readonly LevelUpContext _context;
        private readonly IConfiguration _configuration;
        public TasksController(LevelUpContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [Route("/tasks")]
        public IActionResult TasksPage()
        {
            // checks cookies to make sure a user is logged in and gets user
            var user = GetActiveUser(Request);
            if (user == null) return Redirect("/users/login");
            user.Reset(_context, _configuration["LEVELUP_APICONNECTIONKEY"]);
            return View(user);
            //check id from cookie
        }

        [Route("/tasks/subscribe")]
        public IActionResult Subscribe()
        {
            //creates user to hold default tasks
            var taskSeed = TaskSeed();
            //gets active user
            var user = GetActiveUser(Request);
            if (user == null) return Redirect("/users/login");
            // send both to view
            List<User> viewData = new List<User> { user, taskSeed };
            return View(viewData);
        }

        [HttpPost]
        [Route("/tasks/subscribe")]
        public IActionResult Subscribe(string? type, string? title)
        {
            // make sure data comes in correctly
            if (type == null || title == null) return BadRequest();
            // get user
            var user = GetActiveUser(Request);
            if (user == null) return Redirect("/users/login");
            // get tasks
            var taskSeed = TaskSeed();
            ITask task = null;
            // find task
            if (type == "daily")
            {
                task = taskSeed.DailyTasks.FirstOrDefault(t => t.Title == title);
            }
            else if (type == "weekly")
            {
                task = taskSeed.WeeklyTasks.FirstOrDefault(t => t.Title == title);
            }
            else return BadRequest(); // ends action due to bad inputs
            if (task == null) return BadRequest(); // ends action due to bad inputs

            // check to see if the task is alredy subscribed to and needs to be removed
            if (type == "daily")
            {
                if (user.DailyTasks.Select(t => t.Title).Contains(task.Title))
                {
                    // removes task
                    user.DailyTasks.Remove(user.DailyTasks.FirstOrDefault(t => t.Title == task.Title));
                    _context.Users.Update(user);
                }
                else
                {
                    // adds task
                    user.AddDaily(task, _context);
                    _context.Users.Update(user);

                }
            }
            else if (type == "weekly")
            {
                if (user.WeeklyTasks.Select(t => t.Title).Contains(task.Title))
                {
                    // removes task
                    user.WeeklyTasks.Remove(user.WeeklyTasks.FirstOrDefault(t => t.Title == task.Title));
                    _context.Users.Update(user);
                }
                else
                {
                    // adds task
                    user.AddWeekly(task, _context);
                    _context.Users.Update(user);
                }
            }
            _context.SaveChanges();
            return Ok();
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

            if (!IsTaskUnique(task, user))
            {
                return Json(new { success = false, message = "Task is not Unique" });
            }
            user.DailyTasks.Add(task);
            _context.Users.Update(user);

            _context.SaveChanges();

            return Json(new { success = true, redirectUrl = Url.Action("","tasks") });
        }

        [HttpPost]
        [Route("/tasks/newweekly")]
        public IActionResult CreateWeeklyTask(WeeklyTask task)
        {
            task.XpReward = 3 * task.Difficulty;
            task.AttributeReward = 1;

            var user = GetActiveUser(Request);
            if (user == null) return Redirect("/users/login");

            if (!IsTaskUnique(task, user)) return Json(new { success = false, message = "Task is not Unique" });

            user.WeeklyTasks.Add(task);
            _context.Users.Update(user);

            _context.SaveChanges();
            return Json(new { success = true, redirectUrl = Url.Action("", "tasks") });
        }

        [HttpPost]
        [Route("/tasks/newtodo")]
        public IActionResult CreateToDoTask(ToDoTask task)
        {
            task.XpReward = task.Difficulty;
            task.AttributeReward = 1;

            var user = GetActiveUser(Request);
            if (user == null) return Redirect("/users/login");
            if (!IsTaskUnique(task, user)) return Json(new { success = false, message = "Task is not Unique" });

            user.ToDoTasks.Add(task);
            _context.Users.Update(user);

            _context.SaveChanges();
            return Json(new { success = true, redirectUrl = Url.Action("", "tasks") });
        }

        private User? GetActiveUser(HttpRequest request)
        {
            var userId = Convert.ToInt32(request.Cookies["activeUser"]);
            var userAuth = request.Cookies["userAuth"];

            User? user = _context.Users.Include(u => u.DailyTasks).Include(u => u.WeeklyTasks).Include(u => u.ToDoTasks).Include(u => u.Achievements)
                .ThenInclude(a => a.Hygenie5Achievement)
                        .Include(a => a.Achievements).ThenInclude(a => a.HabitBuilding5Achievement)
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
        [Route("/tasks/update/daily/{taskId:int}")]
        public IActionResult UpdateDaily( DailyTask task, int taskId)
        {
            task.Id =taskId;
            _context.DailyTasks.Update(task);
            _context.SaveChanges();
            return Redirect("/tasks");
        }

        [HttpPost]
        [Route("/tasks/update/weekly/{taskId:int}")]
        public IActionResult UpdateWeekly(WeeklyTask task, int taskId)
        {
            task.Id = taskId;
            _context.WeeklyTasks.Update(task);
            _context.SaveChanges();
            return Redirect("/tasks");
        }

        [HttpPost]
        [Route("/tasks/update/todo/{taskId:int}")]
        public IActionResult UpdateTodo(ToDoTask task, int taskId)
        {
            task.Id = taskId;
            _context.ToDoTasks.Update(task);
            _context.SaveChanges();
            return Redirect("/tasks");
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



        private bool IsTaskUnique(ITask taskToCheck, User user)
        {
            bool isUniqueInDailyTasks = !user.DailyTasks.Any(t => t.Title == taskToCheck.Title);
            bool isUniqueInWeeklyTasks = !user.WeeklyTasks.Any(t => t.Title == taskToCheck.Title);
            bool isUniqueInToDoTasks = !user.ToDoTasks.Any(t => t.Title == taskToCheck.Title);

            return isUniqueInDailyTasks && isUniqueInWeeklyTasks && isUniqueInToDoTasks;
        }

        private User TaskSeed()
        {
            // creates the daily tasks
            var defaultDailyTasks = new List<DailyTask>
            {
                //new DailyTask {Title = "", Description = "", TaskType = "", Category = "", Difficulty = 0, XpReward = 0, AttributeReward = 0},

                new DailyTask {Title = "Take Supplements", Description = "take your daily vitamins and suppliments", TaskType = "daily", Category = "Wellness", Difficulty = 1, XpReward = 1, AttributeReward = 1},
                new DailyTask {Title = "Make Bed", Description = "you made your bed this morning!", TaskType = "daily", Category = "HabitBuilding", Difficulty = 2, XpReward = 2, AttributeReward = 1},
                new DailyTask {Title = "Brush Teeth", Description = "brush your teeth twice a day", TaskType = "daily", Category = "Hygenie", Difficulty = 1, XpReward = 1, AttributeReward = 0},
                new DailyTask {Title = "Shower", Description = "clean your self", TaskType = "daily", Category = "Hygenie", Difficulty = 1, XpReward = 1, AttributeReward = 0},
                new DailyTask {Title = "Meditate", Description = "clear your mind", TaskType = "daily", Category = "Mindfulness", Difficulty = 2, XpReward = 2, AttributeReward = 0},

            };
            // creates the weekly tasks
            var defaultWeeklyTasks = new List<WeeklyTask>
            {
                //new WeeklyTask {Title = "", Description = "", TaskType = "", Category = "", Difficulty = 0, XpReward = 0, AttributeReward = 0},
                
                new WeeklyTask {Title = "Laundry", Description = "do your laundry for the week", TaskType = "weekly", Category = "HabitBuilding", Difficulty = 1, XpReward = 5, AttributeReward = 2},
                new WeeklyTask {Title = "Grocery Shopping", Description = "stock up on healthy food stuffs", TaskType = "weekly", Category = "Productivity", Difficulty = 2, XpReward = 7, AttributeReward = 1},
                new WeeklyTask {Title = "Reflect", Description = "reflect on your week, what has gone well and what could you have done differently?", TaskType = "weekly", Category = "Mindfulness", Difficulty = 3, XpReward = 10, AttributeReward = 3},
                new WeeklyTask {Title = "Clean Your Home", Description = "A clean space is a canvas for a clear mind", TaskType = "weekly", Category = "Hygenie", Difficulty = 2, XpReward = 6, AttributeReward = 1},
                new WeeklyTask {Title = "Meet Your Nutritional Goals", Description = "make a plan on how you can eat healthly and stick to it!", TaskType = "weekly", Category = "Wellness", Difficulty = 3, XpReward = 9, AttributeReward = 1},




            };
            // creates user to hold tasks
            var returnUser = new User { Id = -1, Name = "TaskSeed", Username = "TaskSeed", Password = "TaskSeed" };
            // puts tasks into user and returns user
            returnUser.DailyTasks.AddRange(defaultDailyTasks);
            returnUser.WeeklyTasks.AddRange(defaultWeeklyTasks);
            return returnUser;
        }
    }
}
