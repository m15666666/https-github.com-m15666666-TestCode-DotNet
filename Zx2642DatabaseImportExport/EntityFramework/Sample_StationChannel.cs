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
    /// Sample_StationChannel entity
    /// </summary>
    public class Sample_StationChannel
    {
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None)]//不自动增长
        public int StationChannel_ID { get; set; }

        public int Station_ID { get; set; }

        public int? ChannelType_NR { get; set; }

        public int? ChannelNumber_NR { get; set; }

        public string StationChannelParam_TX { get; set; }
    }
}

