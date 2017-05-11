namespace testORM.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class BS_Person
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id_NR { get; set; }

        [Required]
        [StringLength(150)]
        public string Name_TX { get; set; }

        [StringLength(500)]
        public string Description_TX { get; set; }
    }
}
