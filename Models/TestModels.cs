namespace Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class TestModels : DbContext
    {
        public TestModels()
            : base("name=TestModel")
        {
        }

        public virtual DbSet<UserIP> UserIP { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
