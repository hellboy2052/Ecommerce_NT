using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerSite.Models
{
    public class CartItem
    {
        public int Id { get; set; }
        public Product Product { get; set; }
    }
}
