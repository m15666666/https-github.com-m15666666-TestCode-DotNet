using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Moons.Common20.Test
{
    public class Config
    {
        public static void WriteLine(string message)
        {
            Console.WriteLine(message);
            Debug.WriteLine(message);
        }
    }
}
