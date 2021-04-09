using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedVm
{
    public class OrderVm
    {
        public string UserId { get; set; }  
        public int Id { get; set; }
        public double totalPrice { get; set; }
        public Boolean Status { get; set; }
        public DateTime CraeteDate { get; set; }
        public string Address { get; set; }
        public string UserPhone { get; set; }
    }
}
