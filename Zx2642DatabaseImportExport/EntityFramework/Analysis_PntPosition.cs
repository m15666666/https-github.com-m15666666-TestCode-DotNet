﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zx2642DatabaseImportExport.EntityFramework
{
    /// <summary>
    /// Analysis_PntPosition entity
    /// </summary>
    public class Analysis_PntPosition
    {
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None)]//不自动增长
        public int PntPosition_ID { get; set; }

        public int Point_ID { get; set; }

        public int Mobject_ID { get; set; }

        public double PosX_NR { get; set; }

        public double PosY_NR { get; set; }

        public double TagX_NR { get; set; }

        public double TagY_NR { get; set; }
    }
}

