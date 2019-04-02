using AKS.AppCore.Specifications;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace AKS.AppCore.Entities
{
    public class CollectionElement 
    {
        public CollectionElement() { }

        public Guid CollectionElementId { get; set; }

        public Guid ProjectId { get; set; }
        public Guid TopicId { get; set; }
        public Topic Topic { get; set; }
        public string Name { get; set; }

        private readonly List<Topic> _elementTopics = new List<Topic>();
        public List<Topic> ElementTopics => _elementTopics;
        public void AddTopic(Topic topic)
        {
            if (!_elementTopics.Any(new TopicByIdSpecification(topic.ProjectId, topic.TopicId).CompiledCriteria))
            {
                _elementTopics.Add(topic);
            }
        }
        public void RemoveTopic(Guid projectId, Guid topicId)
        {
            var tag = _elementTopics.FirstOrDefault(new TopicByIdSpecification(projectId, topicId).CompiledCriteria);
            if (tag != null)
            {
                _elementTopics.Remove(tag);
            }
        }
    }
}
