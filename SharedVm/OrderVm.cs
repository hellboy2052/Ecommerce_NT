using System;
using System.Collections.Generic;

namespace SharedVm
{
    public class OrderVm
    {
        public string UserId { get; set; }
        public int Id { get; set; }
        public double TotalPrice { get; set; }
        public Boolean Status { get; set; }
        public DateTime CraeteDate { get; set; }
        public List<OrderDetailVm> orderDetailVms { get; set; }
    }
}
