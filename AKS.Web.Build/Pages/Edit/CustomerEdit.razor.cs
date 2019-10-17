using AKS.App.Build.Api.Client;
using AKS.App.Core.Data;
using AKS.Common.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AKS.App.Core
{
    public class CustomerEditBase : ComponentBase
    {

        [Parameter]
        public Guid CustomerId { get; set; }

        [Inject]
        public CustomerEditApi CustomerEditApi { get; set; } = null!;

        [CascadingParameter] protected IAppState AppState { get; set; } = null!;

        public CustomerEdit Customer { get; set; } = new CustomerEdit();

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters);

            var headerTask = AppState.UpdateCustomerAndProject(CustomerId, null);
            var customerTask = GetCustomer();
            await headerTask;
            await customerTask;

        }

        public async Task GetCustomer()
        {
            Customer = await CustomerEditApi.GetCustomer(CustomerId);
            StateHasChanged();
        }

        public async Task Save()
        {
            await CustomerEditApi.UpdateCustomer(Customer);
        }

        public async Task Cancel()
        {
            await GetCustomer();
        }
    }
}
