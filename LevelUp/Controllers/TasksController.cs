using Microsoft.AspNetCore.Mvc;

namespace LevelUp.Controllers
{
    public class TasksController : Controller
    {

        [Route("/tasks")]
        public IActionResult TasksPage()
        {
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

    }
}
