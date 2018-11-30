namespace Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UserIP")]
    public partial class UserIP
    {
        [Key]
        [StringLength(50)]
        public string UserId { get; set; }

        [Required]
        [StringLength(50)]
        public string PassWord { get; set; }

        [Column("UserIP")]
        [StringLength(100)]
        public string UserIP1 { get; set; }
    }
}
