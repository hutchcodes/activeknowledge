using Resurgam.AppCore.Entities;
using Resurgam.AppCore.Interfaces;
using Resurgam.AppCore.Specifications;
using Resurgam.Infrastructure.Data;
using Resurgam.Web.Admin.Interfaces;
using Resurgam.Web.Admin.ViewModels;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Resurgam.Web.Admin.Services
{
    public class TopicService : ITopicService
    {
        private readonly ILogger<TopicService> _logger;
        private readonly IAsyncRepository<Topic> _topicRepo;
        public TopicService(ILoggerFactory loggerFactory, IAsyncRepository<Topic> topicRepo)
        {
            _logger = loggerFactory.CreateLogger<TopicService>();
            _topicRepo = topicRepo;
        }
        public async Task<TopicDisplayViewModel> GetTopicForDisplayAsync(int projectId, int topicId)
        {
            var spec = new TopicDisplaySpecification(projectId, topicId);
            var topic = await _topicRepo.GetAsync(spec);



            var topicVM = new TopicDisplayViewModel(topic);
            return topicVM;
        }

        public async Task<List<TopicListViewModel>> GetTopicListForProject(int projectId)
        {
            var spec = new TopicListSpecification(projectId);
            var topics = await _topicRepo.ListAsync(spec);

            var topicsVM = new List<TopicListViewModel>();

            topicsVM.AddRange(topics.ConvertAll(x => new TopicListViewModel(x)));

            return topicsVM;
        }
    }
}
