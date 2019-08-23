namespace AdminCMD
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class KeyValue : DbContext
    {
        public KeyValue()
            : base("name=KeyValue")
        {
        }

        public virtual DbSet<Md5Test> Md5Test { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
