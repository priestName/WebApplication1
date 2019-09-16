namespace SignalRChat1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class UserLogs
    {
        public int id { get; set; }

        public int UserId { get; set; }
        
        public string Operate { get; set; }

        public string Message { get; set; }

        public DateTime AddTime { get; set; }
    }
}
