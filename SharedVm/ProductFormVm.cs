using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedVm
{
    public class ProductFormVm
    {
        public List<IFormFile> Images { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public double Price { get; set; }
        public int Inventory { get; set; }

        public int CategoryId { get; set; }

    }
}
