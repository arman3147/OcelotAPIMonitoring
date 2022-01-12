using api.Products.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Products.Repository
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly List<Product> products = new List<Product>();
        public ProductsRepository()
        {
            products.Add(new Product()
            {
                Id = Guid.NewGuid(),
                Code = "005001",
                Name = "Benzin"
            });
            products.Add(new Product()
            {
                Id = Guid.NewGuid(),
                Code = "003001",
                Name = "naftgaz"
            });
            products.Add(new Product()
            {
                Id = Guid.NewGuid(),
                Code = "003401",
                Name = "naftgazU4"
            });
        }

        public Task<List<Product>> GetAllProducts()
        {
            return Task.FromResult(products);
        }
    }
}
