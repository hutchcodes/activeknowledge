using System;
using System.Threading.Tasks;
using AKS.Common.Models;
using AKS.Infrastructure.Interfaces;

namespace AKS.Share.Web.Pages
{
    public abstract class AKSProjectPage : AKSPage 
    {
        protected readonly ICategoryService _categoryService;

        public AKSProjectPage(IHeaderService headerService, ICategoryService categoryService) : base (headerService)
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