using System;
using System.Collections.Generic;
using System.Text;

namespace AKS.Common.Models
{
    public class CategoryTopicList
    {
        public CategoryTopicList() { }
        public CategoryTopicList(TopicList topic)
        {
            if (topic != null)
            {
                Topic = topic;
                TopicId = topic.TopicId;
                ProjectId = topic.ProjectId;
            }
        }
        public Guid CategoryId { get; set; }
        public Guid ProjectId { get; set; }
        public Guid TopicId { get; set; }
        public TopicList? Topic { get; set; }
        public int Order { get; set; }
    }
}
