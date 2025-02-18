namespace BuildSchedule.ExcelAddIn.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using SigmaCape.Business.Syspro;
    
    public partial class SysprodbContext : DbContext
    {
        static SysprodbContext()
        {
            Database.SetInitializer<SysprodbContext>(null);
        }

        public SysprodbContext()
            : base("Sysprodb")
        {
        }

        public virtual DbSet<SysproAdmin> SysproAdmin { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

        }
    }
}
