using System;
using System.Collections.Generic;
using System.Text;

namespace Moons.ExportData2Center.Core
{
    public class ServerContext
    {
        public static ServerContext Instance { get; set; } = new ServerContext();

        public List<string> TableNames { get; set; } = new List<string>();

        public Dictionary<string, TableMetaInfo> TableName2TableMetaInfos { get; set; } = new Dictionary<string, TableMetaInfo>();

        public void AddTableMetaInfo( TableMetaInfo info )
        {
            TableNames.Add( info.TableName );
            TableName2TableMetaInfos.Add( info.TableName, info );
        }
    }
}
