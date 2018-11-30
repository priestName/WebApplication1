namespace WebApplication2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Msg")]
    public partial class Msg
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(50)]
        public string uid { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string yid { get; set; }

        [Key]
        [Column("msg", Order = 2)]
        public string msg1 { get; set; }
    }
}
