namespace SignalRChat1.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class TestSocked : DbContext
    {
        public TestSocked()
            : base("name=TestSocked")
        {
        }

        public virtual DbSet<SockedUser> SockedUser { get; set; }
        public virtual DbSet<UserLogs> UserLogs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
