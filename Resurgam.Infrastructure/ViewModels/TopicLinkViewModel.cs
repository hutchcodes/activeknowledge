using Resurgam.AppCore.Entities;
using Resurgam.AppCore.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resurgam.Infrastructure.ViewModels
{
    public class TopicLinkViewModel
    {
        public TopicLinkViewModel() { }
        public TopicLinkViewModel(IReferencedTopic referencedTopic)
        {
            ProjectId = referencedTopic.ProjectId;
            TopicId = referencedTopic.ChildTopicId;
            TopicName = referencedTopic.ChildTopic.Name;
            TopicDescription = referencedTopic.ChildTopic.Description;
        }

        public TopicLinkViewModel(Topic referencedTopic)
        {
            ProjectId = referencedTopic.ProjectId;
            TopicId = referencedTopic.TopicId;
            TopicName = referencedTopic.Name;
            TopicDescription = referencedTopic.Description;
        }

        public Guid ProjectId { get; set; }
        public Guid TopicId { get; set; }
        public string TopicName { get; set; }
        public string TopicDescription { get; set; }

        public RelatedTopic ToRelatedTopic()
        {
            var relatedTopic = new RelatedTopic();
            relatedTopic.ProjectId = ProjectId;
            relatedTopic.ChildTopicId = TopicId;
            return relatedTopic;
        }
    }
}
