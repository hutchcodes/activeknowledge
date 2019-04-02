using System;
using System.Collections.Generic;
using System.Text;
using AKS.AppCore.Entities;

namespace AKS.AppCore.Specifications
{
    public class TopicByIdSpecification : BaseSpecification<Topic>
    {
        public TopicByIdSpecification(Guid projectId, Guid topicId) : base(x => x.ProjectId == projectId && x.TopicId == topicId)
        {
            this.Includes.Add(x => x.Tags);
            this.Includes.Add(x => x.RelatedToTopics);
        }
    }
}
