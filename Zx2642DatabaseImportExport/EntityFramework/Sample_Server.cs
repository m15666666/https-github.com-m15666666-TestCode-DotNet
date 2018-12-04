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
    /// Sample_Server entity
    /// </summary>
    public class Sample_Server
    {
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None)]//不自动增长
        public int Server_ID { get; set; }

        public string Name_TX { get; set; }

        public string URL_TX { get; set; }

        public string ServerParam_TX { get; set; }

        public string IP_TX { get; set; }
    }
}

