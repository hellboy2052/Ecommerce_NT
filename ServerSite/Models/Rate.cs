﻿namespace ServerSite.Models
{
    public class Rate
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int Star { get; set; }
       
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public User User { get; set; }
    }
}
