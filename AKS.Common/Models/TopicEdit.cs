using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AKS.Common.Models
{
    
    public class TopicEdit
    {
        public Guid ProjectId { get; set; }
        public Guid TopicId { get; set; }
        public int TopicTypeId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public string DocumentName { get; set; }

        public List<TopicLink> RelatedTopics { get; set; } = new List<TopicLink>();
        public List<Tag> Tags { get; set; } = new List<Tag>();
        public List<CollectionElement> CollectionElements { get; set; } = new List<CollectionElement>();

        private void CleanTopicContent()
        {
            if (string.IsNullOrWhiteSpace(Content))
            {
                return;
            }
            var topicContent = Content.Replace("{{projectId}}", ProjectId.ToString());

            Content = topicContent;
        }
    }
}
