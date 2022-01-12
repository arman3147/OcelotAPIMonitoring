using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Products.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public double Price_Unit { get; set; }
    }
}
