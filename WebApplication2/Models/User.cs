namespace WebApplication2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class User
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(50)]
        public string uid { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string psd { get; set; }
    }
}
