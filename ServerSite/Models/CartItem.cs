using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerSite.Models
{
    public class CartItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
      
        public double Price { get; set; }
        public int Inventory { get; set; }
       
        public string ImageFirst { get; set; }
        public int Quantity { get; set; }
        public int CartId { get; set; }
        public Cart Cart { get; set; }
    }
}
