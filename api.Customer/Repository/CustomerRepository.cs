using api.Customer.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Customer.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private List<Models.Customer> customers = new List<Models.Customer>();
        public CustomerRepository()
        {
            customers.Add(new Models.Customer()
            {
                Id = Guid.NewGuid(),
                FirstName = "Arman",
                LastName = "Bahrkazemi",
                Email = "arman.brki3147@gmail.com"
            });
            customers.Add(new Models.Customer()
            {
                Id = Guid.NewGuid(),
                FirstName = "majid",
                LastName = "gholipour :)",
                Email = "tempEmail@gmail.com"
            });
        }
        public Task<List<Models.Customer>> GetAllCustomer()
        {
            return Task.FromResult(customers);
        }
    }
}
