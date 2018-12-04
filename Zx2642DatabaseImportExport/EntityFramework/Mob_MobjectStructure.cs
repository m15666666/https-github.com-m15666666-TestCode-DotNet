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
    /// Mob_MobjectStructure entity
    /// </summary>
    public class Mob_MobjectStructure
    {
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None)]//不自动增长
        public int MobjectStructure_ID { get; set; }

        public int Mobject_ID { get; set; }

        public int Org_ID { get; set; }

        public int Parent_ID { get; set; }

        public int Lever_NR { get; set; }

        public string ParentList_TX { get; set; }

        public string Memo_TX { get; set; }
    }
}

