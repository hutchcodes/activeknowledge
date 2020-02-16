using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AKS.Common.Models;

namespace AKS.Infrastructure.Interfaces
{
    public interface ICategoryService
    {
        Task<List<CategoryTree>> GetCategoryTreeAsync(Guid projectId, Guid? categoryId = null, Guid? topicId = null);
        Task<List<CategoryTree>> SaveCategoryTreeAsync(Guid projectId, List<CategoryTree> categoryTree);
    }
}
