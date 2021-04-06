using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerSite.Models
{
    public class Cart
    {
        public string UserId { get; set; }
        public User User { get; set; }
        public int Id { get; set; }
        public IList<int> Products { get; set; }
        
    }
}
