using System;
using System.Collections.Generic;
using System.Text;

namespace Moons.Common20.Test.LuaScript
{
    public class DetectedDemo1
    {
        public static DetectedDemo1 Instance { get; set; } = new DetectedDemo1();

        public int State3 { get; set; } = 2;

        public string State4 { get; set; } = "efg";

        public Probe Probe { get; set; } = new Probe();

        public override string ToString()
        {
            return $"DetectedDemo1,State3:{State3},State4:{State4}";
        }
    }

    public class Probe
    {
        public int State1 { get; set; } = 1;

        public string State2 { get; set; } = "abc";

        public override string ToString()
        {
            Console.WriteLine($"in Probe.tostring");
            StringBuilder builder = new StringBuilder();
            builder.AppendLine($"State1:{State1}");
            builder.AppendLine($"State2:{State2}");
            return builder.ToString();
        }
    }
}
