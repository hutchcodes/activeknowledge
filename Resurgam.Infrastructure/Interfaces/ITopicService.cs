using Resurgam.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resurgam.Infrastructure.Interfaces
{
    public interface ITopicService
    {
        Task<TopicDisplayViewModel> GetTopicForDisplayAsync(Guid projectId, Guid topicId);
        Task<TopicEditViewModel> GetTopicForEditAsync(Guid projectId, Guid topicId);
        Task<List<TopicListViewModel>> GetTopicListForProject(Guid projectId);
        Task SaveTopicAsync(TopicEditViewModel topicVM);
    }
}
