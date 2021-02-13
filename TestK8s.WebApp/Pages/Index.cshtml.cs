using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestK8s.WebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IConfiguration _configuration;

        public IndexModel(ILogger<IndexModel> logger, IConfiguration configuration)
        {
            EnvironmentInfo.PrintEnv();
            _logger = logger;
            _configuration = configuration;
            PrintMsg();

        }

        public void OnGet()
        {

        }
        private void PrintMsg()
        {
            const string Section = "ConfigDto";
            const string NotInEnvVariables = "NotInEnvVariables";
            var section = _configuration.GetSection(Section);
            foreach( var s in new string[] { "A","B","C", NotInEnvVariables })
            {
                Print(section[s]);
            }
            Print(_configuration[$"{Section}:A"]);
            Print(_configuration[$"{Section}:B"]);
            Print(_configuration[$"{Section}:C"]);
            Print(_configuration[$"{Section}:{NotInEnvVariables}"]);

        }
        private void Print(string msg)
        {
            Console.WriteLine(msg);
            _logger.LogWarning(msg);
        }
    }
}
