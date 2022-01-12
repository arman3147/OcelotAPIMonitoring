using api.Customer.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Customer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {

        private readonly ILogger<CustomersController> _logger;
        private readonly ICustomerRepository _customerRepository;
        

        public CustomersController(ILogger<CustomersController> logger,ICustomerRepository customerRepository)
        {
            _logger = logger;
            _customerRepository = customerRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<Models.Customer>>> Get()
        {
            return await _customerRepository.GetAllCustomer();
        }
    }
}
