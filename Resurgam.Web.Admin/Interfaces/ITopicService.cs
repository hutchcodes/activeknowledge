using Resurgam.Web.Admin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resurgam.Web.Admin.Interfaces
{
    public interface ITopicService
    {
        Task<TopicDisplayViewModel> GetTopicForDisplayAsync(int projectId, int topicId);
        Task<List<TopicListViewModel>> GetTopicListForProject(int projectId);
    }
}
