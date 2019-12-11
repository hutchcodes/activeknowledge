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
    public class CategoryViewApi
    {
        private readonly HttpClient _http;

        public CategoryViewApi(HttpClient http) 
        {
            _http = http;
        }
        public async Task<CategoryTreeView> GetCategoryTreeForProject(Guid projectId)
        {
            var categoryTree = await _http.GetJsonAsync<CategoryTreeView>($"categoryview/project/{projectId}");

            return categoryTree;
        }
    }
}
