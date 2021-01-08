using System;
using System.Threading.Tasks;
using AKS.Common.Models;
using AKS.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AKS.Api.Build.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IHeaderService _headerService;
        private readonly ICustomerService _customerService;

        public CustomerController(IHeaderService headerService, ICustomerService customerService)
        {
            _headerService = headerService;
            _customerService = customerService;
        }

        // GET: api/Customer/Header/1234
        [HttpGet("header/{customerId:Guid}", Name = "GetCustomerHeader")]
        public async Task<HeaderNavView> GetHeader(Guid customerId)
        {
            return await _headerService.GetHeaderForCustomerAsync(customerId);
        }

        [HttpGet("{customerId:Guid}", Name = "GetCustomerEdit")]
        public async Task<CustomerEdit> GetCustomerEdit(Guid customerId)
        {
            return await _customerService.GetCustomerForEdit(customerId);
        }

        // GET: api/Customer/Header/1234
        [HttpPost(Name = "UpdateCustomer")]
        public async Task<CustomerEdit> UpdateCustomerEdit(CustomerEdit customer)
        {
            return await _customerService.UpdateCustomer(customer);
        }
    }
}
