using System;
using System.Collections.Generic;
using System.Text;

namespace DotNettyServer
{
    public static class ServerSettings
    {
        public static bool IsSsl { get; internal set; }
        public static int Port { get; internal set; }
    }
}
