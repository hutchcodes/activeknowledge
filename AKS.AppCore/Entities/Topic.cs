using AKS.AppCore.Specifications;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace AKS.AppCore.Entities
{
    public class Topic : BaseEntity
    {
        public Topic() { }

        public Guid TopicId { get; set; }
        public Guid ProjectId { get; set; }
        public int TopicTypeId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid? ImageResourceId { get; set; }
        public string Content { get; set; }
        public Guid? FileResourceId { get; set; }
        public string DocumentName { get; set; }

        public Guid? DefaultCategoryId { get; set; }

        public List<Tag> Tags { get; set; } = new List<Tag>();

        public List<RelatedTopic> RelatedToTopics { get; set; } = new List<RelatedTopic>();

        public List<RelatedTopic> RelatedFromTopics { get; set; } = new List<RelatedTopic>();

        public List<ReferencedFragment> ReferencedFragments { get; set; } = new List<ReferencedFragment>();

        public List<ReferencedFragment> FragmentReferencedBy { get; set; } = new List<ReferencedFragment>();

        public List<CollectionElement> CollectionElements { get; set; } = new List<CollectionElement>();
    }
}
