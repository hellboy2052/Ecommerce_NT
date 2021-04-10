using System.Collections.Generic;

namespace SharedVm
{
    public class CartProductVm
    {
        public int ProductID { get; set; }

        public string ProductName { get; set; }

        public double Price { get; set; }

        //public int Quantity { get; set; }

        public List<string> ImageLocation { get; set; }

    }
}
