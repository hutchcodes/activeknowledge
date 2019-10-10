using System;
using System.Threading.Tasks;
using AKS.Common.Models;
using AKS.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AKS.App.Build.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerEditController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        public CustomerEditController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        // GET: api/Customer/Header/1234
        [HttpGet("{customerId:Guid}", Name = "GetCustomerEdit")]
        public async Task<CustomerEdit> GetCustomerEdit(Guid customerId)
        {
            return await _customerService.GetGetCustomerForEdit(customerId);
        }

        // GET: api/Customer/Header/1234
        [HttpPost(Name = "UpdateCustomer")]
        public async Task<CustomerEdit> UpdateCustomerEdit(CustomerEdit customer)
        {
            return await _customerService.UpdateCustomer(customer);
        }
    }
}
