using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerSite.Models
{
    public class OrderDetail
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public double totalPrice { get; set; }
        public DateTime CraeteDate { get; set; }
        public Boolean Status { get; set; }
        public string Address { get; set; }
        public string UserPhone { get; set; }
    }
}
