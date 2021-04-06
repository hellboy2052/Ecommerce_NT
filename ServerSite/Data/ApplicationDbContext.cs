﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ServerSite.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServerSite.Data
{
    public class ApplicationDbContext : IdentityUserContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Rate> Rates { get; set; }
        public DbSet<Cart> Carts { get; set; }
    }
}
