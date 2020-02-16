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
    public class CategoryEditApi
    {
        private readonly HttpClient _http;

        public CategoryEditApi(HttpClient http) 
        {
            _http = http;
        }
        public async Task<List<CategoryTree>> GetCategoryTreeForProject(Guid projectId)
        {
            var categoryTree = await _http.GetJsonAsync<List<CategoryTree>>($"categoryedit/project/{projectId}");

            return categoryTree;
        }

        public async Task<List<CategoryTree>> SaveCategoryTree(Guid projectId, List<CategoryTree> categoryTrees)
        {
            categoryTrees = await _http.PostJsonAsync<List<CategoryTree>>($"categoryedit/project/{projectId}", categoryTrees);

            return categoryTrees;
        }
    }
}
