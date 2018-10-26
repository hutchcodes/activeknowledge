using Resurgam.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resurgam.Infrastructure.Interfaces
{
    public interface ICategoryService
    {
        Task<CategoryTreeViewModel> GetCategoryTreeAsync(Guid projectId, Guid? categoryId = null, Guid? topicId = null);
    }
}
