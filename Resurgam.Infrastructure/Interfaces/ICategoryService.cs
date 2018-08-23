using Resurgam.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resurgam.Infrastructure.Interfaces
{
    public interface ICategoryService
    {
        Task<CategoryTreeViewModel> GetCategoryTreeAsync(int projectId, int? categoryId = null, int? topicId = null);
    }
}
