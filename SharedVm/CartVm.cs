using System.Collections.Generic;

namespace SharedVm
{
    public class CartVm
    {
        public int Id { get; set; }
        public double TotalPrice { get; set; }
        public string UserId { get; set; }
        public List<CartItemVm> cartItemVms { get; set; }
        //public int ProductId { get; set; }
    }
}
