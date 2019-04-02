using System;
using System.Threading.Tasks;
using AKS.Common.Models;
using AKS.Infrastructure.Interfaces;

namespace Resurgam.Admin.Web.Pages
{
    public abstract class ResurgamProjectPage : ResurgamPage 
    {
        protected readonly ICategoryService _categoryService;

        public ResurgamProjectPage(IHeaderService headerService, ICategoryService categoryService) : base (headerService)
        {
            _categoryService = categoryService;
        }
        public CategoryTreeView CategoryTree { get; set; }

        public async Task GetCategoryTree(Guid projectId)
        {
            CategoryTree = await _categoryService.GetCategoryTreeAsync(projectId);
        }
    }
}