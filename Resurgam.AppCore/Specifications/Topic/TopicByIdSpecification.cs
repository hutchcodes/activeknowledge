using System;
using System.Collections.Generic;
using System.Text;
using Resurgam.AppCore.Entities;

namespace Resurgam.AppCore.Specifications
{
    public class TopicByIdSpecification : BaseSpecification<Topic>
    {
        public TopicByIdSpecification(Guid projectId, Guid topicId) : base(x => x.ProjectId == projectId && x.TopicId == topicId)
        {
            this.Includes.Add(x => x.Tags);
            this.Includes.Add(x => x.RelatedTopics);
        }
    }
}
