using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Migrations;

namespace Zx2642DatabaseImportExport.EntityFramework
{
    

    public partial class MyDbContext : DbContext
    {
        public MyDbContext()
            : base("name=Database1")
        {
        }

        public MyDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
        }


        
        public virtual DbSet<Pnt_Point> Pnt_Point { get; set; }

        public virtual DbSet<Mob_MObject> Mob_MObject { get; set; }

        public virtual DbSet<Mob_MobjectStructure> Mob_MobjectStructure { get; set; }

        public virtual DbSet<Sample_DAUStation> Sample_DAUStation { get; set; }
        public virtual DbSet<Sample_PntChannel> Sample_PntChannel { get; set; }
        public virtual DbSet<Sample_Server> Sample_Server { get; set; }
        public virtual DbSet<Sample_ServerDAU> Sample_ServerDAU { get; set; }
        public virtual DbSet<Sample_Station> Sample_Station { get; set; }
        public virtual DbSet<Sample_StationChannel> Sample_StationChannel { get; set; }

        public virtual DbSet<Analysis_PntPosition> Analysis_PntPosition { get; set; }
        public virtual DbSet<Analysis_MObjPosition> Analysis_MObjPosition { get; set; }
        public virtual DbSet<Analysis_MObjPicture> Analysis_MObjPicture { get; set; }
        //public virtual DbSet<> { get; set; }

        public virtual DbSet<Pnt_DataVar> Pnt_DataVar { get; set; }
        public virtual DbSet<Pnt_PntDataVar> Pnt_PntDataVar { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            
        }
    }


}
