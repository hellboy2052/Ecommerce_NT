﻿using System.Collections.Generic;

namespace ServerSite.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public double Price { get; set; }
        public int Inventory { get; set; }
        public int BrandId { get; set; }
        public Brand Brand { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public virtual ICollection<Image> Images { get; set; }

    }
}
