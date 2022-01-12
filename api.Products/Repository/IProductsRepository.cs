using api.Products.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace api.Products.Repository
{
    public interface IProductsRepository
    {
        Task<List<Product>> GetAllProducts();
    }
}