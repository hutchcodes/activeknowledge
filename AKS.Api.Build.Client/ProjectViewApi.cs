using AKS.Common;
using AKS.Common.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace AKS.Api.Build.Client
{
    public class ProjectViewApi
    {
        private readonly HttpClient _http;

        public ProjectViewApi(HttpClient http)
        {
            _http = http;
        }        

        public async Task<List<ProjectList>> GetProjectListByCustomer(Guid customerId)
        {
            var projectList = await _http.GetJsonAsync<List<ProjectList>>($"project/list/{customerId}");
            return projectList;
        }
    }
}
