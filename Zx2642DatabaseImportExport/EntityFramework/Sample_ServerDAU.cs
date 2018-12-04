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
    /// Sample_ServerDAU entity
    /// </summary>
    public class Sample_ServerDAU
    {
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None)]//不自动增长
        public int ServerDAU_ID { get; set; }

        public int Server_ID { get; set; }

        public string Name_TX { get; set; }

        public string URL_TX { get; set; }

        public string ServerDAUParam_TX { get; set; }

        public string IP_TX { get; set; }
    }
}

