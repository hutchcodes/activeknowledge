using AKS.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AKS.Infrastructure.Entities
{
    [Table("Topic")]
    public partial class Topic : BaseEntity
    {
        public Topic()
        {
            CategoryTopics = new HashSet<CategoryTopic>();
            CollectionElements = new HashSet<CollectionElement>();
            CollectionElementTopics = new HashSet<CollectionElementTopic>();
            RelatedFromTopics = new HashSet<RelatedTopic>();
            RelatedToTopics = new HashSet<RelatedTopic>();
            TopicFragmentsParents = new HashSet<TopicFragment>();
            TopicFragmentChildren = new HashSet<TopicFragment>();
            TopicTags = new HashSet<TopicTag>();
        }

        public Guid TopicId { get; set; }
        public Guid ProjectId { get; set; }
        public TopicType TopicType { get; set; }
        public string Title { get; set; } = "";
        public string? Description { get; set; }
        public TopicStatus TopicStatus { get; set; }
        public Guid? ImageResourceId { get; set; }
        public string? Content { get; set; }
        public Guid? FileResourceId { get; set; }
        public string? DocumentName { get; set; }
        public Guid? DefaultCategoryId { get; set; }

        public virtual ICollection<CategoryTopic> CategoryTopics { get; set; }
        public virtual ICollection<CollectionElement> CollectionElements { get; set; }
        public virtual ICollection<CollectionElementTopic> CollectionElementTopics { get; set; }
        public virtual ICollection<RelatedTopic> RelatedFromTopics { get; set; }
        public virtual ICollection<RelatedTopic> RelatedToTopics { get; set; }
        public virtual ICollection<TopicFragment> TopicFragmentsParents { get; set; }
        public virtual ICollection<TopicFragment> TopicFragmentChildren { get; set; }
        public virtual ICollection<TopicTag> TopicTags { get; set; }
    }
}
