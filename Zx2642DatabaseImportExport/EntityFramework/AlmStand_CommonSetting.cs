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
    /// AlmStand_CommonSetting entity
    /// </summary>
    public class AlmStand_CommonSetting
    {
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None)]//不自动增长
        public int CommonSetting_ID { get; set; }

        public byte? AlmType_ID { get; set; }

        public double? LowLimit1_NR { get; set; }

        public double? HighLimit1_NR { get; set; }

        public double? LowLimit2_NR { get; set; }

        public double? HighLimit2_NR { get; set; }

        public double? LowLimit3_NR { get; set; }

        public double? HighLimit3_NR { get; set; }

        public double? LowLimit4_NR { get; set; }

        public double? HighLimit4_NR { get; set; }

        public string Description_TX { get; set; }
    }
}

