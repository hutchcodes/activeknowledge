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
    public class HeaderApi
    {
        private readonly HttpClient _http;

        public HeaderApi(HttpClient http) 
        {
            _http = http;
        }
        public async Task<HeaderNavView> GetHeaderForProject(Guid projectId)
        {
            var headerNav = await _http.GetJsonAsync<HeaderNavView>($"header/project/{projectId}");
            return headerNav;
        }

        public async Task<HeaderNavView> GetHeaderForCustomer(Guid customerId)
        {
            var headerNav = await _http.GetJsonAsync<HeaderNavView>($"header/customer/{customerId}");
            return headerNav;
        }
    }
}
