namespace testORM.EF
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model2 : DbContext
    {
        public Model2()
            : base("name=Model2")
        {
        }

        public Model2(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
        }

        // #1
        //public virtual DbSet<BS_Person> BS_Person { get; set; }

        protected override void OnModelCreating( DbModelBuilder modelBuilder )
        {
            // #2
            //modelBuilder.Entity<BS_Person>();
            
            // #3
            modelBuilder.RegisterEntityType(typeof(BS_Person));

            // #1 和 #2 和 #3 可以互相替代， #2 和 #3 用于动态添加实体（表）


        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            string message = $"disposing:{disposing}";
            Console.WriteLine(message);
        }
    }
}
