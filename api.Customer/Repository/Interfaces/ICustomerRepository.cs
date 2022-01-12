using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Customer.Repository.Interfaces
{
    public interface ICustomerRepository
    {
        Task<List<Models.Customer>> GetAllCustomer();
    }
}
