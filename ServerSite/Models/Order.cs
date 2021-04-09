using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerSite.Models
{
    public class Order
    {
        public string UserId { get; set; }
        public User User { get; set; }
        public int Id { get; set; }
        public int ProductId { get; set; }
        public ICollection<Product> Products { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public double totalPrice { get; set; }
        public Boolean Status { get; set; }
        public DateTime CraeteDate { get; set; }
        public string Address { get; set; }
        public string UserPhone { get; set; }
    }
}
