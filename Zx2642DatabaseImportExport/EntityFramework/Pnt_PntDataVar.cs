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
    /// Pnt_PntDataVar entity
    /// </summary>
    public class Pnt_PntDataVar
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None)]//不自动增长
        public int Point_ID { get; set; }

        [Key, Column(Order = 1)]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None)]//不自动增长
        public int Var_ID { get; set; }
    }
}

