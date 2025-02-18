namespace BuildSchedule.ExcelAddIn.Models
{
    using BuildSchedule.ExcelAddIn.Controllers;
    using SigmaCape.Business.Syspro;
    using System.Data.Entity;

    /// <summary>
    /// The syspro context.
    /// </summary>
    public partial class SysproContext : DbContext
    {
        #region Constructors and Destructors

        static SysproContext()
        {
            Database.SetInitializer<SysproContext>(null);
        }

        public SysproContext(SysproIdentity sysIdentity)
            : base(new SqlRepository().GetCompanyDB(sysIdentity.Profile.Company))
        {
        }

        #endregion

        #region Public Properties

        public DbSet<InvWarehouse> InvWarehouse { get; set; }
        public DbSet<InvMaster> InvMaster { get; set; }
        public virtual DbSet<MrpBuildSchedule> MrpBuildSchedule { get; set; }

        #endregion

        #region Methods

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }

        #endregion
    }
}