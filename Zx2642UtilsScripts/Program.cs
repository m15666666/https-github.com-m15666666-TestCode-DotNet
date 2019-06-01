using System;
using System.Diagnostics;

namespace Zx2642UtilsScripts
{
    class Program
    {
        static void Main(string[] args)
        {
            new DaLianXiTiScript().GenerateExportSQLs();
            //new OpcAgent2642Script().GenerateXmlSegments();
            //new WineSteelScript().GenerateExportSQLs();

            Console.WriteLine("Zx2642UtilsScripts");
        }
    }
}
