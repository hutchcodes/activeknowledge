using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AKS.Infrastructure.Entities;
using AKS.Infrastructure.Interfaces;
using AKS.Infrastructure.Specifications;
using AKS.Common.Models;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace AKS.Infrastructure.Services
{
    public class CustomerService : ICustomerService 
    {
        private readonly ILogger<ProjectService> _logger;
        private readonly IMapper _mapper;
        private readonly IAsyncRepository<Customer> _customerRepo;
        public CustomerService(IMapper mapper, ILoggerFactory loggerFactory, IAsyncRepository<Customer> customerRepo)
        {
            _logger = loggerFactory.CreateLogger<ProjectService>();
            _mapper = mapper;
            _customerRepo = customerRepo;

            _logger.LogDebug($"New instance of {GetType().Name} was created");
        }
        public async Task<CustomerEdit> GetCustomerForEdit(Guid customerId)
        {
            var spec = new CustomerSpecification(customerId);
            var customer = await _customerRepo.GetAsync(spec);

            return _mapper.Map<CustomerEdit>(customer);
        }

        public async Task<CustomerEdit> UpdateCustomer(CustomerEdit customerEdit)
        {
            var spec = new CustomerSpecification(customerEdit.CustomerId);
            var customer = await _customerRepo.GetAsync(spec);
            
            customer = _mapper.Map(customerEdit, customer);
            await _customerRepo.UpdateAsync(customer);

            customer = await _customerRepo.GetAsync(spec);
            return _mapper.Map<CustomerEdit>(customer);
        }
    }
}
