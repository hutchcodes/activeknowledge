using System;
using System.Collections.Generic;
using System.Text;
using Resurgam.AppCore.Entities;
using Resurgam.AppCore.Entities.Interfaces;

namespace Resurgam.AppCore.Specifications
{
    public class TopicDisplaySpecification : BaseSpecification<Topic>
    {
        public TopicDisplaySpecification(Guid projectId, Guid topicId) : base(x => x.ProjectId == projectId && x.TopicId == topicId)
        {
            //AddInclude(x => x.Tags);

            //AddInclude(x => x.RelatedTopics);
            //AddInclude($"{nameof(Topic.RelatedTopics)}.{nameof(IReferencedTopic.ChildTopic)}");

            AddInclude(x => x.CollectionElements);
            AddInclude($"{nameof(Topic.CollectionElements)}.{nameof(CollectionElement.ElementTopics)}");

            AddInclude(x => x.ReferencedFragments);
            AddInclude($"{nameof(Topic.ReferencedFragments)}.{nameof(IReferencedTopic.ChildTopic)}");
        }
    }
}
