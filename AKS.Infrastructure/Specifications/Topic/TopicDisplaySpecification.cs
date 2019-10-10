using System;
using System.Collections.Generic;
using System.Text;
using AKS.Infrastructure.Entities;
using AKS.Infrastructure.Entities.Interfaces;

namespace AKS.Infrastructure.Specifications
{
    public class TopicDisplaySpecification : BaseSpecification<Topic>
    {
        public TopicDisplaySpecification(Guid projectId, Guid topicId) : base(x => x.ProjectId == projectId && x.TopicId == topicId)
        {
            AddInclude(x => x.TopicTags);

            AddInclude(x => x.RelatedToTopics);
            AddInclude($"{nameof(Topic.RelatedToTopics)}.{nameof(IReferencedTopic.ChildTopic)}");

            AddInclude(x => x.CollectionElements);
            AddInclude($"{nameof(Topic.CollectionElements)}.{nameof(CollectionElement.CollectionElementTopics)}");
            AddInclude($"{nameof(Topic.CollectionElements)}.{nameof(CollectionElement.CollectionElementTopics)}.{nameof(CollectionElementTopic.Topic)}");

            AddInclude(x => x.TopicFragmentChildren);
            AddInclude($"{nameof(Topic.TopicFragmentChildren)}.{nameof(IReferencedTopic.ChildTopic)}");
        }
    }
}
