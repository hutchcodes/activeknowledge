using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AKS.Common.Models;

namespace AKS.Infrastructure.Interfaces
{
    public interface ICategoryTopicService
    {
        Task SaveCategoryTopicsAsync(List<CategoryTopicList> topic);
        Task DeleteCategoryTopicAsync(Guid projectId, Guid categoryId, Guid topicId);
    }
}
