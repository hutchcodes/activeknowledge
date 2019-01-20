using Resurgam.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resurgam.Infrastructure.Interfaces
{
    public interface ITopicService
    {
        Task<TopicDisplayViewModel> GetTopicForDisplay(Guid projectId, Guid topicId);
        Task<TopicEditViewModel> GetTopicForEdit(Guid projectId, Guid topicId);
        Task<List<TopicListViewModel>> GetTopicListForProject(Guid projectId);
        Task<List<TopicListViewModel>> SearchTopics(Guid projectId, Guid? categoryId, string searchString);
        Task SaveTopic(TopicEditViewModel topicVM);

        Task DeleteTopic(Guid projectId, Guid topicId);
    }
}
