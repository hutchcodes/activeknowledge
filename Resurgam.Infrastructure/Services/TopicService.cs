using Resurgam.AppCore.Entities;
using Resurgam.AppCore.Interfaces;
using Resurgam.AppCore.Specifications;
using Resurgam.Infrastructure.Data;
using Resurgam.Infrastructure.Interfaces;
using Resurgam.Infrastructure.ViewModels;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Resurgam.Infrastructure.Services
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
        public async Task<TopicDisplayViewModel> GetTopicForDisplayAsync(Guid projectId, Guid topicId)
        {
            var spec = new TopicDisplaySpecification(projectId, topicId);
            var topic = await _topicRepo.GetAsync(spec);

            var topicVM = new TopicDisplayViewModel(topic);
            return topicVM;
        }

        public async Task<TopicEditViewModel> GetTopicForEditAsync(Guid projectId, Guid topicId)
        {
            var spec = new TopicEditSpecification(projectId, topicId);
            var topic = await _topicRepo.GetAsync(spec);

            var topicVM = new TopicEditViewModel(topic);
            return topicVM;
        }

        public async Task<List<TopicListViewModel>> GetTopicListForProject(Guid projectId)
        {
            var spec = new TopicListSpecification(projectId);
            var topics = await _topicRepo.ListAsync(spec);

            var topicsVM = new List<TopicListViewModel>();

            topicsVM.AddRange(topics.ConvertAll(x => new TopicListViewModel(x)));

            return topicsVM;
        }

        public async Task SaveTopicAsync(TopicEditViewModel topicVM)
        {
            try
            {
                var spec = new TopicEditSpecification(topicVM.ProjectId, topicVM.TopicId);
                var topic = await _topicRepo.GetAsync(spec);

                topic = topicVM.ToTopicEntity(topic);
                await _topicRepo.UpdateAsync(topic);
            }
            catch (Exception ex)
            {

            }
        }
    }
}
