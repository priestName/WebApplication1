namespace SignalRChat1
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Md5TestUser
    {
        public int Id { get; set; }
        
        public string Key { get; set; }
        
        public string Value { get; set; }
    }
}
