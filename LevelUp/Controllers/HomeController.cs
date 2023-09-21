using LevelUp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LevelUp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            List<ChartData> chartData = new List<ChartData>
            {
                new ChartData{ x=2000, y1= 0.03, y2= 0.48},
                new ChartData{ x=2001, y1= 0.05, y2= 0.53 },
                new ChartData{ x=2002, y1= 0.06, y2= 0.57 },
                new ChartData{ x=2003, y1= 0.09, y2= 0.61 },
                new ChartData{ x=2004, y1= 0.14, y2= 0.63 },
                new ChartData{ x=2005, y1= 0.20, y2= 0.64 },
                new ChartData{ x=2006, y1= 0.29, y2= 0.66 },
                new ChartData{ x=2007, y1= 0.46, y2= 0.76 },
                new ChartData{ x=2008, y1= 0.64, y2= 0.77 },
                new ChartData{ x=2009,  y1= 0.75, y2= 0.55 }
            };
            ViewBag.dataSource = chartData;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

    public class ChartData
    {
        public double x;
        public double y1;
        public double y2;
    }
}