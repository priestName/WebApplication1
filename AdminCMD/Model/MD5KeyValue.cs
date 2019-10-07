namespace AdminCMD.Model
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class MD5KeyValue : DbContext
    {
        public MD5KeyValue()
            : base("name=MD5KeyValue")
        {
        }

        public virtual DbSet<Md5Test> Md5Test { get; set; }
        public virtual DbSet<Md5TestUser> Md5TestUser { get; set; }
        public virtual DbSet<V_MD5Test> V_MD5Test { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
