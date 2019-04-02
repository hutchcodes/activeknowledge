using System;
using System.Threading.Tasks;
using AKS.Common.Models;
using AKS.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AKS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IHeaderService _headerService;
        public CustomerController(IHeaderService headerService)
        {
            _headerService = headerService;
        }
        
        // GET: api/Customer/Header/1234
        [HttpGet("header/{customerId:Guid}",  Name = "GetCustomerHeader")]
        public async Task<HeaderNavView> GetHeader(Guid customerId)
        {
            return await _headerService.GetHeaderForCustomerAsync(customerId);
        }
    }
}
