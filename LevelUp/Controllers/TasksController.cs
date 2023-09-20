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
        public IActionResult NewTask()
        {
            return View();
        }

        [HttpPost]
        [Route("/tasks/create")]
        public IActionResult CreateTask()
        {
            //All the code for creating and adding a task
            return Redirect("/tasks");
        }
    }
}
