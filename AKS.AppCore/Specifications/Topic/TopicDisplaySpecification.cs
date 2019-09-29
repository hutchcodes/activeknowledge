using System;
using System.Collections.Generic;
using System.Text;
using AKS.AppCore.Entities;
using AKS.AppCore.Entities.Interfaces;

namespace AKS.AppCore.Specifications
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
