using AKS.Common;
using AKS.Common.Enums;
using AKS.Infrastructure.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AKS.Api.Build.Helpers
{
    class UserUpdateHelper
    {
        private readonly ServiceProvider _serviceProvider;

        public UserUpdateHelper(ServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public async Task CreateUpdateUser( ClaimsPrincipal principal)
        {
            var customerId = Guid.Parse(UserClaimHelper.GetClaimValue(principal, UserClaimType.CustomerId));
            var userId = Guid.Parse(UserClaimHelper.GetClaimValue(principal, UserClaimType.UserId));
            var isNewCustomer = await CreateCustomerIfNeeded(customerId);
            var userService = _serviceProvider.GetService<IUserService>();
            await userService.CreateUpdateUser(principal);

            if (isNewCustomer)
            {
                await MakeUserCustomerAdmin(customerId, userId);
            }
        }

        private async Task<bool> CreateCustomerIfNeeded(Guid customerId)
        {
            var isNew = false;
            var customerService = _serviceProvider.GetService<ICustomerService>();

            var cust = await customerService.GetCustomerForEdit(customerId);

            if (cust == null)
            {
                isNew = true;
                var customer = new AKS.Common.Models.CustomerEdit()
                {
                    CustomerId = customerId,
                    Name = "New Customer"
                };
                await customerService.CreateCustomer(customer);
            }
            return isNew;
        }

        private async Task MakeUserCustomerAdmin(Guid customerId, Guid userId)
        {
            var groupService = _serviceProvider.GetService<IGroupService>();
            await groupService.AddCustomerAdminWithUser(customerId, userId);
        } 
    }
}
