using AKS.AppCore.Entities;
using AKS.AppCore.Interfaces;
using AKS.AppCore.Specifications;
using AKS.Infrastructure.Data;
using AKS.Infrastructure.Interfaces;
using AKS.Common.Models;
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

            _logger.LogDebug($"New instance of {GetType().Name} was created");
        }

        public async Task<List<TopicList>> SearchTopics(Guid projectId, Guid? categoryId, string searchString)
        {
            var spec = new TopicSearchSpecification(projectId, categoryId, searchString);
            var topics = await _topicRepo.ListAsync(spec);

            var topicsVM = new List<TopicList>();

            topicsVM.AddRange(topics.ConvertAll(x => new TopicList { ProjectId = x.ProjectId, TopicId = x.TopicId, TopicName = x.Name, TopicDesription = x.Description } ));

            return topicsVM;
        }
        public async Task<TopicView> GetTopicForDisplay(Guid projectId, Guid topicId)
        {
            var spec = new TopicDisplaySpecification(projectId, topicId);
            var topic = await _topicRepo.GetAsync(spec);
            var topicVM = CreateTopicView(topic);

            foreach (var tag in topic.Tags)
            {
                topicVM.Tags.Add(new Common.Models.Tag { ProjectId = tag.ProjectId, TagId = tag.TagId, TagName = tag.Name });
            }

            foreach (var rt in topic.RelatedToTopics)
            {
                topicVM.RelatedTopics.Add(new TopicLink { ProjectId = rt.ProjectId, TopicId = rt.ChildTopicId, TopicName = rt.ChildTopic.Name, TopicDescription = rt.ChildTopic.Description });
            }

            foreach (var elm in topic.CollectionElements)
            {
                var elemant = new Common.Models.CollectionElement { ProjectId = elm.ProjectId, CollectionElementId = elm.CollectionElementId, CollectionElementName = elm.Name };
                topicVM.CollectionElements.Add(elemant);

                foreach (var top in elm.ElementTopics)
                {
                    elemant.Topics.Add(CreateTopicView(top));
                }
            }

            return topicVM;
        }

        private static TopicView CreateTopicView(Topic topic)
        {
            return new TopicView
            {
                TopicId = topic.TopicId,
                TopicName = topic.Name,
                TopicDescription = topic.Description,
                TopicTypeID = topic.TopicTypeId,
                ProjectId = topic.ProjectId,
                Content = topic.TopicContent,
                DocumentName = topic.DocumentName,
            };
        }

        public async Task<TopicEdit> GetTopicForEdit(Guid projectId, Guid topicId)
        {
            var spec = new TopicEditSpecification(projectId, topicId);
            var topic = await _topicRepo.GetAsync(spec);

            var topicVM = new TopicEdit
            {
                TopicId = topic.TopicId,
                TopicName = topic.Name,
                TopicDescription = topic.Description,
                TopicTypeID = topic.TopicTypeId,
                ProjectId = topic.ProjectId,
                TopicContent = topic.TopicContent,
                DocumentName = topic.DocumentName,
            };

            foreach (var tag in topic.Tags)
            {
                topicVM.Tags.Add(new Common.Models.Tag { ProjectId = tag.ProjectId, TagId = tag.TagId, TagName = tag.Name });
            }

            foreach (var rt in topic.RelatedToTopics)
            {
                topicVM.RelatedTopics.Add(new TopicLink { ProjectId = rt.ProjectId, TopicId = rt.ChildTopicId, TopicName = rt.ChildTopic.Name, TopicDescription = rt.ChildTopic.Description });
            }

            foreach (var elm in topic.CollectionElements)
            {
                var elemant = new Common.Models.CollectionElement { ProjectId = elm.ProjectId, CollectionElementId = elm.CollectionElementId, CollectionElementName = elm.Name };
                topicVM.CollectionElements.Add(elemant);

                foreach (var top in elm.ElementTopics)
                {
                    elemant.Topics.Add(CreateTopicView(top));
                }
            }

            return topicVM;
        }

        public async Task<List<TopicList>> GetTopicListForProject(Guid projectId)
        {
            var spec = new TopicListSpecification(projectId);
            var topics = await _topicRepo.ListAsync(spec);

            var topicsVM = new List<TopicList>();

            topicsVM.AddRange(topics.ConvertAll(x => new TopicList { ProjectId = x.ProjectId, TopicId = x.TopicId, TopicName = x.Name, TopicDesription = x.Description }));

            return topicsVM;
        }

        public async Task SaveTopic(TopicEdit topicVM)
        {
            try
            {
                var spec = new TopicEditSpecification(topicVM.ProjectId, topicVM.TopicId);
                var topic = await _topicRepo.GetAsync(spec);

                //if (topic == null)
                //{
                //    topic = topicVM.ToTopicEntity(topic);
                //    await _topicRepo.AddAsync(topic);
                //}
                //else
                //{
                //    topic = topicVM.ToTopicEntity(topic);
                //    await _topicRepo.UpdateAsync(topic);
                //}
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
