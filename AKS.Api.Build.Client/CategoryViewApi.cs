using AKS.Common;
using AKS.Common.Models;
using Microsoft.AspNetCore.Components;
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
        public async Task<List<CategoryTree>> GetCategoryTreeForProject(Guid projectId)
        {
            var categoryTrees = await _http.GetJsonAsync<List<CategoryTree>>($"categoryview/project/{projectId}");

            return categoryTrees;
        }
    }
}
