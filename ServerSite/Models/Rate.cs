﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerSite.Models
{
    public class Rate
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int totalStar { get; set; }
        public string Comment { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public User User { get; set; }
    }
}
