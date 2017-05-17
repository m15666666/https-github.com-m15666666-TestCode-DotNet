using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebCore.Services;
using Microsoft.AspNetCore.Http;

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
            #region 测试依赖注入

            TestService.Log( "abc" );

            #endregion


            #region 测试session访问

            const string SessionKeyName = "_Name";
            const string SessionKeyYearsMember = "_YearsMember";
            const string SessionKeyDate = "_Date";

            // Requires using Microsoft.AspNetCore.Http;
            HttpContext.Session.SetString(SessionKeyName, "Rick");
            HttpContext.Session.SetInt32(SessionKeyYearsMember, 3);

            var name = HttpContext.Session.GetString(SessionKeyName);
            var yearsMember = HttpContext.Session.GetInt32(SessionKeyYearsMember);

            #endregion

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
