using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebCore.Services
{
    public class TestService : ITestService
    {
        void ITestService.Log(string message)
        {
            string m = message;

            Console.WriteLine(m);
        }
    }
}
