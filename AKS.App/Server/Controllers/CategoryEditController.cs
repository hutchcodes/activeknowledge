using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AKS.Common.Models;
using AKS.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AKS.Api.Build.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryEditController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly ICategoryTopicService _categoryTopicService;

        public CategoryEditController(ICategoryService categoryService, ICategoryTopicService categoryTopicService)
        {
            _categoryService = categoryService;
            _categoryTopicService = categoryTopicService;
        }

        [HttpGet("project/{projectId:Guid}")]
        public async Task<List<CategoryTree>> GetCategoryTreeForProject(Guid projectId)
        {
            var categoryTrees = await _categoryService.GetCategoryTreeAsync(projectId);
            return categoryTrees;
        }

        [HttpPost("project/{projectId:Guid}")]
        public async Task<List<CategoryTree>> SaveCategoryTreeForProject(Guid projectId, List<CategoryTree> categoryTrees)
        {
            categoryTrees = await _categoryService.SaveCategoryTreeAsync(projectId, categoryTrees);
            return categoryTrees;
        }

        [HttpPost]
        public async Task SaveCategoryTreeForProject(CategoryTree categoryTree)
        {
            await _categoryService.SaveCategoryTreeAsync(categoryTree);
        }

        [HttpDelete("{projectId:Guid}/{categoryId:Guid}")]
        public async Task DeleteCategoryTreeForProject(Guid projectId, Guid categoryId)
        {
            await _categoryService.DeleteCategoryTreeAsync(projectId, categoryId);
        }

        [HttpPost("topic")]
        public async Task SaveCategoryTopic(List<CategoryTopicList> topics)
        {
            await _categoryTopicService.SaveCategoryTopicsAsync(topics);
        }
        
        [HttpDelete("{projectId:Guid}/{categoryId:Guid}/topic/{topicId}")]
        public async Task DeleteCategoryTopic(Guid projectId, Guid categoryId, Guid topicId)
        {
            await _categoryTopicService.DeleteCategoryTopicAsync(projectId, categoryId, topicId);
        }
    }
}