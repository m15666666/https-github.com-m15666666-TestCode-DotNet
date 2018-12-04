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
    /// Sample_PntChannel entity
    /// </summary>
    public class Sample_PntChannel
    {
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None)]//不自动增长
        public int StationChannel_ID { get; set; }

        public int Point_ID { get; set; }

        public byte? ChnNo_NR { get; set; }
    }
}

