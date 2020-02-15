using System;
using System.Threading.Tasks;
using AKS.Common.Models;

namespace AKS.Infrastructure.Interfaces
{
    public interface ICategoryService
    {
        Task<CategoryTree> GetCategoryTreeAsync(Guid projectId, Guid? categoryId = null, Guid? topicId = null);
        Task<CategoryTree> SaveCategoryTreeAsync(CategoryTree categoryTree);
    }
}
