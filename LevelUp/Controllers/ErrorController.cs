using Microsoft.AspNetCore.Mvc;

namespace LevelUp.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult StatusCode(int statusCode)
        {
            if (statusCode == 404)
            {
                return View("NotFound");
            }
            // Handle other status codes if needed
            return View("Error");
        }
    }
}
