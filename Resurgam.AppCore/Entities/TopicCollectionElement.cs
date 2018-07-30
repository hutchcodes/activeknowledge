using Resurgam.AppCore.Specifications;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Resurgam.AppCore.Entities
{
    public class CollectionElement : BaseEntity
    {
        public CollectionElement() { }

        public int ProjectId { get; set; }
        public int TopicId { get; set; }
        public Topic Topic { get; set; }
        public string Name { get; set; }

        private readonly List<Topic> _elementTopics = new List<Topic>();
        public List<Topic> ElementTopics => _elementTopics;
        public void AddTopic(Topic topic)
        {
            if (!_elementTopics.Any(new TopicByIdSpecification(topic.ProjectId, topic.Id).CompiledCriteria))
            {
                _elementTopics.Add(topic);
            }
        }
        public void RemoveTopic(int projectId, int topicId)
        {
            var tag = _elementTopics.FirstOrDefault(new TopicByIdSpecification(projectId, topicId).CompiledCriteria);
            if (tag != null)
            {
                _elementTopics.Remove(tag);
            }
        }
    }
}
