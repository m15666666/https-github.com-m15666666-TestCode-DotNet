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
	/// Pnt_Point entity
	/// </summary>
	public class Pnt_Point
    {
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None)]//不自动增长
        public int Point_ID { get; set; }

        public int Mobject_ID { get; set; }

        public string PointName_TX { get; set; }

        public short DatType_ID { get; set; }

        public short SigType_ID { get; set; }

        public short PntDim_NR { get; set; }

        public short PntDirect_NR { get; set; }

        public short Rotation_NR { get; set; }

        public int EngUnit_ID { get; set; }

        public int FeatureValue_ID { get; set; }

        public short StoreType_NR { get; set; }

        public int SortNo_NR { get; set; }

        public string Desc_TX { get; set; }
    }
}
