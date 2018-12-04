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
    /// AlmStand_SixFreqBnd entity
    /// </summary>
    public class AlmStand_SixFreqBnd
    {
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None)]//不自动增长
        public int Point_ID { get; set; }

        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None)]//不自动增长
        public byte SixFreqBndType_ID { get; set; }

        public byte? LowAlmLevel_ID { get; set; }

        public byte? HighAlmLevel_ID { get; set; }

        public int? FreqBnd_NR { get; set; }

        public double? LowFreq1_NR { get; set; }

        public double? HighFreq1_NR { get; set; }

        public double? LowLimit1_NR { get; set; }

        public double? HighLimit1_NR { get; set; }

        public double? LowFreq2_NR { get; set; }

        public double? HighFreq2_NR { get; set; }

        public double? LowLimit2_NR { get; set; }

        public double? HighLimit2_NR { get; set; }

        public double? LowFreq3_NR { get; set; }

        public double? HighFreq3_NR { get; set; }

        public double? LowLimit3_NR { get; set; }

        public double? HighLimit3_NR { get; set; }

        public double? LowFreq4_NR { get; set; }

        public double? HighFreq4_NR { get; set; }

        public double? LowLimit4_NR { get; set; }

        public double? HighLimit4_NR { get; set; }

        public double? LowFreq5_NR { get; set; }

        public double? HighFreq5_NR { get; set; }

        public double? LowLimit5_NR { get; set; }

        public double? HighLimit5_NR { get; set; }

        public double? LowFreq6_NR { get; set; }

        public double? HighFreq6_NR { get; set; }

        public double? LowLimit6_NR { get; set; }

        public double? HighLimit6_NR { get; set; }

        public string Description_TX { get; set; }
    }
}

