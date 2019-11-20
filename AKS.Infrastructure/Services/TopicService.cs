using AKS.Infrastructure.Entities;
using AKS.Infrastructure.Interfaces;
using AKS.Infrastructure.Specifications;
using AKS.Infrastructure.Data;
using AKS.Common.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.EntityFrameworkCore;

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

            _logger.LogDebug($"New instance of {GetType().Name} was created");
        }

        public async Task<List<TopicList>> SearchTopics(Guid projectId, Guid? categoryId, string searchString)
        {
            var spec = new TopicSearchSpecification(projectId, categoryId, searchString);
            var topics = await _topicRepo.ListAsync(spec);

            return Mapper.Map<List<TopicList>>(topics);
        }
        public async Task<TopicView> GetTopicForDisplay(Guid projectId, Guid topicId)
        {
            var spec = new TopicDisplaySpecification(projectId, topicId);
            var topic = await _topicRepo.GetAsync(spec);

            return Mapper.Map<TopicView>(topic);
        }

        public async Task<TopicEdit> GetTopicForEdit(Guid projectId, Guid topicId)
        {
            var spec = new TopicEditSpecification(projectId, topicId);
            var topic = await _topicRepo.GetAsync(spec);

            return Mapper.Map<TopicEdit>(topic);
        }

        public async Task<List<TopicList>> GetTopicListForProject(Guid projectId)
        {
            var spec = new TopicListSpecification(projectId);
            var topics = await _topicRepo.ListAsync(spec);

            return Mapper.Map<List<TopicList>>(topics);
        }

        public async Task<TopicEdit> SaveTopic(TopicEdit topicVM)
        {
            var spec = new TopicEditSpecification(topicVM.ProjectId, topicVM.TopicId);
            Topic topic;
            if (topicVM.TopicStatus == Common.Enums.TopicStatus.New)
            {
                topic = Mapper.Instance.Map< Topic>(topicVM);
                await _topicRepo.AddAsync(topic);
            }
            else
            {
                topic = await _topicRepo.GetAsync(spec);
                topic = Mapper.Instance.Map(topicVM, topic);
                await _topicRepo.UpdateAsync(topic);
            }

            topic = await _topicRepo.GetAsync(spec);
            return Mapper.Map<TopicEdit>(topic);
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
