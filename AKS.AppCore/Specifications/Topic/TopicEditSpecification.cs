using System;
using System.Collections.Generic;
using System.Text;
using AKS.AppCore.Entities;
using AKS.AppCore.Entities.Interfaces;

namespace AKS.AppCore.Specifications
{
    public class TopicEditSpecification : BaseSpecification<Topic>
    {
        public TopicEditSpecification(Guid projectId, Guid topicId) : base(x => x.ProjectId == projectId && x.TopicId == topicId)
        {
            AddInclude(x => x.TopicTags);

            //AddInclude(x => x.RelatedTopics);
            //AddInclude($"{nameof(Topic.RelatedTopics)}.{nameof(IReferencedTopic.ChildTopic)}");

            AddInclude(x => x.CollectionElements);
            AddInclude($"{nameof(Topic.CollectionElements)}.{nameof(CollectionElement.CollectionElementTopics)}.{nameof(CollectionElementTopic.Topic)}");

            //AddInclude(x => x.ReferencedFragments);
            //AddInclude($"{nameof(Topic.ReferencedFragments)}.{nameof(IReferencedTopic.ChildTopic)}");
        }
    }
}
