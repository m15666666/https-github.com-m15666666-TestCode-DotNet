using System;
using System.Collections.Generic;
using System.Text;

namespace Moons.Common20.TestTools
{
    public static class ConsoleUtils
    {
        /// <summary>
        /// 回车以继续
        /// </summary>
        /// <param name="nextStep">下一个步骤名称，默认为空</param>
        /// <returns>输入的字符串</returns>
        public static string Enter2Continue(string nextStep = "" )
        {
            Console.WriteLine($"enter to continue {nextStep}");
            return Console.ReadLine();
        }
        /// <summary>
        /// 回车以继续,输入q退出程序
        /// </summary>
        /// <param name="nextStep">下一个步骤名称，默认为空</param>
        /// <returns>输入的字符串</returns>
        public static string EnterQ2Exit(string nextStep = "" )
        {
            Console.WriteLine($"enter to continue {nextStep}, enter q to exit");
            return Console.ReadLine();
        }
    }
}
