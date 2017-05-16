using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebCore.Services;

namespace WebCore.Controllers
{
    public class HomeController : Controller
    {
        public ITestService TestService { get; set; }

        public HomeController(ITestService testService)
        {
            TestService = testService;
        }

        public IActionResult Index()
        {
            TestService.Log( "abc" );
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
