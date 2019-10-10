using System;
using System.Collections.Generic;
using System.Text;
using AKS.Infrastructure.Entities;

namespace AKS.Infrastructure.Specifications
{
    public class TopicByIdSpecification : BaseSpecification<Topic>
    {
        public TopicByIdSpecification(Guid projectId, Guid topicId) : base(x => x.ProjectId == projectId && x.TopicId == topicId)
        {
            this.Includes.Add(x => x.TopicTags);
            this.Includes.Add(x => x.RelatedToTopics);
        }
    }
}
