using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class HomeViewModel
    {
        public IEnumerable<User> user { get; set; }
    }
}