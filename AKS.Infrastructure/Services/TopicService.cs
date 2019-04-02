using AKS.AppCore.Entities;
using AKS.AppCore.Interfaces;
using AKS.AppCore.Specifications;
using AKS.Infrastructure.Data;
using AKS.Infrastructure.Interfaces;
using AKS.Infrastructure.ViewModels;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AKS.Infrastructure.Services
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

        public async Task<List<TopicListViewModel>> SearchTopics(Guid projectId, Guid? categoryId, string searchString)
        {
            var spec = new TopicSearchSpecification(projectId, categoryId, searchString);
            var topics = await _topicRepo.ListAsync(spec);

            var topicsVM = new List<TopicListViewModel>();

            topicsVM.AddRange(topics.ConvertAll(x => new TopicListViewModel(x)));

            return topicsVM;
        }
        public async Task<TopicDisplayViewModel> GetTopicForDisplay(Guid projectId, Guid topicId)
        {
            var spec = new TopicDisplaySpecification(projectId, topicId);
            var topic = await _topicRepo.GetAsync(spec);

            var topicVM = new TopicDisplayViewModel(topic);
            return topicVM;
        }

        public async Task<TopicEditViewModel> GetTopicForEdit(Guid projectId, Guid topicId)
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

        public async Task SaveTopic(TopicEditViewModel topicVM)
        {
            try
            {
                var spec = new TopicEditSpecification(topicVM.ProjectId, topicVM.TopicId);
                var topic = await _topicRepo.GetAsync(spec);

                if (topic == null)
                {
                    topic = topicVM.ToTopicEntity(topic);
                    await _topicRepo.AddAsync(topic);
                }
                else
                {
                    topic = topicVM.ToTopicEntity(topic);
                    await _topicRepo.UpdateAsync(topic);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task DeleteTopic(Guid projectId, Guid topicId)
        {
            var spec = new TopicEditSpecification(projectId, topicId);
            var topic = await _topicRepo.GetAsync(spec);

            if (topic != null)
            {
                await _topicRepo.DeleteAsync(topic);
            }
        }
    }
}
