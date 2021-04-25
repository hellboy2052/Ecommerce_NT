using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedVm
{
    public class CartItemVm
    {
        public int Id { get; set; }
        public ProductVm productVm { get; set; }
        public int Quantity { get; set; }
    }
}
