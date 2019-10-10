using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AKS.AppCore.Entities;
using AKS.AppCore.Interfaces;
using AKS.AppCore.Specifications;
using AKS.Common.Models;
using AKS.Infrastructure.Interfaces;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace AKS.Infrastructure.Services
{
    public class CustomerService : ICustomerService 
    {
        private readonly ILogger<ProjectService> _logger;
        private readonly IAsyncRepository<Customer> _customerRepo;
        public CustomerService(ILoggerFactory loggerFactory, IAsyncRepository<Customer> customerRepo)
        {
            _logger = loggerFactory.CreateLogger<ProjectService>();
            _customerRepo = customerRepo;

            _logger.LogDebug($"New instance of {GetType().Name} was created");
        }
        public async Task<CustomerEdit> GetGetCustomerForEdit(Guid customerId)
        {
            var spec = new CustomerSpecification(customerId);
            var customer = await _customerRepo.GetAsync(spec);

            return Mapper.Map<CustomerEdit>(customer);
        }

        public async Task<CustomerEdit> UpdateCustomer(CustomerEdit customerEdit)
        {
            var spec = new CustomerSpecification(customerEdit.CustomerId);
            var customer = await _customerRepo.GetAsync(spec);
            
            customer = Mapper.Map(customerEdit, customer);
            await _customerRepo.UpdateAsync(customer);

            customer = await _customerRepo.GetAsync(spec);
            return Mapper.Map<CustomerEdit>(customer);
        }
    }
}
