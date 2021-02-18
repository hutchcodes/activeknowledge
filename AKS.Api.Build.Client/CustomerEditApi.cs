using AKS.Common;
using AKS.Common.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace AKS.Api.Build.Client
{
    public class CustomerEditApi
    {
        readonly string _aksBuildApiBaseUrl;
        private readonly HttpClient _http;

        public CustomerEditApi(HttpClient http)
        {
            _http = http;
        }        

        public async Task<CustomerEdit> GetCustomer(Guid customerId)
        {
            var customer = await _http.GetJsonAsync<CustomerEdit>($"api/customer/{customerId}");
            return customer;
        }

        public async Task<CustomerEdit> UpdateCustomer(CustomerEdit customerEdit)
        {
            var customer = await _http.PostJsonAsync<CustomerEdit>($"api/customer", customerEdit);
            return customer;
        }
    }
}
