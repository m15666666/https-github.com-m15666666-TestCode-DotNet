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
    /// Pnt_DataVar entity
    /// </summary>
    public class Pnt_DataVar
    {
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None)]//不自动增长
        public int Var_ID { get; set; }

        public string VarName_TX { get; set; }

        public string VarDesc_TX { get; set; }

        public double VarScale_NR { get; set; }

        public double VarOffset_NR { get; set; }
    }
}

