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
    /// Analysis_MObjPicture entity
    /// </summary>
    public class Analysis_MObjPicture
    {
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None)]//不自动增长
        public int MObject_ID { get; set; }

        public byte[] Picture_GR { get; set; }

        public string FileName_TX { get; set; }

        public string FileSuffix_TX { get; set; }
    }
}

