using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AKS.Common.Models;

namespace AKS.Infrastructure.Interfaces
{
    public interface ITopicService
    {
        Task<TopicView> GetTopicForDisplay(Guid projectId, Guid topicId);
        Task<TopicEdit> GetTopicForEdit(Guid projectId, Guid topicId);
        Task<List<TopicList>> GetTopicListForProject(Guid projectId);
        Task<List<TopicList>> SearchTopics(Guid projectId, Guid? categoryId, string searchString);
        Task<TopicEdit> SaveTopic(TopicEdit topicVM);

        Task DeleteTopic(Guid projectId, Guid topicId);
    }
}
