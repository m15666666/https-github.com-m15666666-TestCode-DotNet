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
	/// BS_Org entity
	/// </summary>
	public class BS_Org
    {
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None)]//not autogenerate primary key value
        public int Org_ID { get; set; }

        public string Org_CD { get; set; }

        public string Memo_TX { get; set; }

        public string OrgName_TX { get; set; }

        public int Property_ID { get; set; }

        public int? SortNo_NR { get; set; }

        public string AddUser_TX { get; set; }

        public DateTime? Add_DT { get; set; }

        public string EditUser_TX { get; set; }

        public DateTime? Edit_DT { get; set; }

        public string Active_YN { get; set; }

        public string WorkDateVersion_TX { get; set; }

        public string InterFaceKey_CD { get; set; }
    }
}
