﻿namespace SharedVm
{
    public class OrderDetailVm
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public int UnitPrice { get; set; }
        public ProductVm Product { get; set; }
    }
}
