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
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryViewController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryViewController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("project/{projectId:Guid}")]
        public async Task<List<CategoryTree>> GetHeaderForProject(Guid projectId)
        {
            var categoryTree = await _categoryService.GetCategoryTreeAsync(projectId);
            return categoryTree;
        }
    }
}