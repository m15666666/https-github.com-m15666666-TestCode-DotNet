using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zx2642DatabaseImportExport.EntityFramework
{
    /// <summary>
    /// Sample_Station entity
    /// </summary>
    public class Sample_Station
    {
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None)]//不自动增长
        public int Station_ID { get; set; }

        public int? Org_ID { get; set; }

        public string Name_TX { get; set; }

        public string StationSN_TX { get; set; }

        public string StationType_TX { get; set; }

        public string IP_TX { get; set; }

        public string StationParam_TX { get; set; }
    }
}

