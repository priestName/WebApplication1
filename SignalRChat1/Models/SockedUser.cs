namespace SignalRChat1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SockedUser")]
    public partial class SockedUser
    {
        public int ID { get; set; }
        
        public string Name { get; set; }
        
        public string Password { get; set; }
        
        public string ClientId { get; set; }
    }
}
