using System;
using System.Threading.Tasks;
using AKS.Common.Models;

namespace AKS.Infrastructure.Interfaces
{
    public interface ICategoryService
    {
        Task<CategoryTreeView> GetCategoryTreeAsync(Guid projectId, Guid? categoryId = null, Guid? topicId = null);
    }
}
