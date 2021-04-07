﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedVm
{
    public class ProductVm
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<string> ImageLocation { get; set; }
        public double Price { get; set; }
        public int Inventory { get; set; }
        public int BrandId { get; set; }
        public int CategoryId { get; set; }
    }
}