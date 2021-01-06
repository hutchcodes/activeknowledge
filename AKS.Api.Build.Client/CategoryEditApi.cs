using AKS.Common;
using AKS.Common.Models;
using Microsoft.AspNetCore.Components;
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
            var categoryTree = await _http.GetJsonAsync<List<CategoryTree>>($"api/categoryedit/project/{projectId}");

            return categoryTree;
        }

        public async Task<List<CategoryTree>> SaveCategoryTree(Guid projectId, List<CategoryTree> categoryTrees)
        {
            categoryTrees = await _http.PostJsonAsync<List<CategoryTree>>($"api/categoryedit/project/{projectId}", categoryTrees);

            return categoryTrees;
        }

        public async Task SaveCategory(CategoryTree categoryTrees)
        {
            await _http.PostJsonAsync($"api/categoryedit/", categoryTrees);
            return;
        }

        public async Task DeleteCategory(Guid projectId, Guid categoryId)
        {
            await _http.DeleteAsync($"api/categoryedit/{projectId}/{categoryId}");
        }

        public async Task SaveCategoryTopics(List<CategoryTopicList> topics)
        {
            await _http.PostJsonAsync($"api/categoryedit/topic/", topics);
        }
        public async Task DeleteCategoryTopic(Guid projectId, Guid categoryId, Guid topicId)
        {
            await _http.DeleteAsync($"api/categoryedit/{projectId}/{categoryId}/topic/{topicId}");
        }
    }
}
