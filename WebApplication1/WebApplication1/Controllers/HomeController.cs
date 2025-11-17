using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // اهم 3 طرق في الكونترولر
        // Method Public   2) Can't be static   3) cnat' be overloading

        public ContentResult ShowMsg()
        {
            // 1)declare
            ContentResult result = new ContentResult();
            //2)initialize
            result.Content = "Hello from ShowMsg";
            // 3) Return
            return result;
        }

        public ViewResult ShowView()
        {
            // 1)declare
            ViewResult result2 = new ViewResult();
            //2)initialize
            result2.ViewName = "View1";
            // 3) Return
            return result2;
        }

        // URL : /Home/ShowMix?id=5
        public IActionResult ShowMix(int id)
        {
            if (id% 2 == 0)
            {
                return ShowMsg();
            }
            else
            {
                return ShowView();
            }
        }
        // Action Return Type

        // Srting ---> ContentResult
        // View --->ViewResult
        // JSON ----> JsonResult
        // Redirect--->RedirectResult
        // File---> FileResult
        //NotFound ---> NotFoundResult



        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Manage ()
           
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
