using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerSite.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public List<CartProduct> cartProducts { get; set; }
        public double Total { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
