using Resurgam.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resurgam.Infrastructure.Interfaces
{
    public interface ITopicService
    {
        Task<TopicDisplayViewModel> GetTopicForDisplayAsync(int projectId, int topicId);
        Task<TopicEditViewModel> GetTopicForEditAsync(int projectId, int topicId);
        Task<List<TopicListViewModel>> GetTopicListForProject(int projectId);
        Task SaveTopicAsync(TopicEditViewModel topicVM);
    }
}
