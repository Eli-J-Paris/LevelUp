using LevelUp.DataAccess;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace LevelUp.Controllers
{
    public class ErrorController : Controller
    {
        private readonly LevelUpContext _context;

        public ErrorController(LevelUpContext context)
        {
            _context = context;
        }

        public IActionResult Error(int? statusCode = null)
        {
            if (statusCode.HasValue)
            {
                // Log the error
                Log.Error("An exception occurred that was caught by the error-handling controller");
                Log.Error($"Stutus Code: {HttpContext.Response.StatusCode}");
                // Handle different status codes or exceptions
                switch (statusCode.Value)
                {
                    // User Errors
                    // Bad Request
                    case 400:
                        return View("Error400");

                    // Unauthorized
                    case 401:
                        return View("Error401");

                    // Not Found
                    case 404:
                        return View("Error404");

                    // Request Timeout
                    case 408:
                        return View("Error408");

                    // Server Errors
                    // Internal Server Error
                    case 500:
                        return View("Error500");

                    // Bad Gateway
                    case 502:
                        return View("Error502");

                    // Gateway Timeout
                    case 504:
                        return View("Error504");
                    // If there's no exception return a generic error 
                    default:
                        return View("Error");
                }
            }
            

            // If there's no exception return a generic error 
            return View("Error");
        }
    }
}
