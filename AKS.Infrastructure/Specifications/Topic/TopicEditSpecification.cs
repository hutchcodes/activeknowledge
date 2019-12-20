using System;
using System.Collections.Generic;
using System.Text;
using AKS.Infrastructure.Entities;
using AKS.Infrastructure.Entities.Interfaces;

namespace AKS.Infrastructure.Specifications
{
    public class TopicEditSpecification : BaseSpecification<Topic>
    {
        public TopicEditSpecification(Guid projectId, Guid topicId) : base(x => x.ProjectId == projectId && x.TopicId == topicId)
        {
            AddInclude(x => x.TopicTags);

            AddInclude(x => x.RelatedToTopics);
            AddInclude($"{nameof(Topic.RelatedToTopics)}.{nameof(RelatedTopic.ChildTopic)}");

            AddInclude(x => x.CollectionElements);
            AddInclude($"{nameof(Topic.CollectionElements)}.{nameof(CollectionElement.CollectionElementTopics)}.{nameof(CollectionElementTopic.Topic)}");

            //AddInclude(x => x.ReferencedFragments);
            //AddInclude($"{nameof(Topic.ReferencedFragments)}.{nameof(IReferencedTopic.ChildTopic)}");
        }
    }
}
