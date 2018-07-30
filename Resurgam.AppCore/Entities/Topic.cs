using Resurgam.AppCore.Specifications;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Resurgam.AppCore.Entities
{
    public class Topic : BaseEntity
    {
        public int ProjectId { get; set; }
        public int TopicTypeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid? ImageResourceId { get; set; }
        public string TopicContent { get; set; }
        public Guid? FileResourceId { get; set; }
        public string Url { get; set; }

        public int? DefaultCategoryId { get; set; }

        private readonly List<Tag> _tags = new List<Tag>();
        public IReadOnlyCollection<Tag> Tags => _tags.AsReadOnly();
        public void AddTag(int tagId, string tagName)
        {
            if (!_tags.Any(new TagFilterSpecification(this.ProjectId, tagId).CompiledCriteria))
            {
                _tags.Add(new Tag()
                {
                    Id = tagId,
                    ProjectId = this.ProjectId,
                    Name = tagName,
                });
                return;
            }
        }
        public void RemoveTag(int tagId)
        {
            var tag = _tags.FirstOrDefault(new TagFilterSpecification(this.ProjectId, tagId).CompiledCriteria);
            if (tag != null)
            {
                _tags.Remove(tag);
            }
        }

        private readonly List<RelatedTopic> _relatedTopics = new List<RelatedTopic>();
        public IReadOnlyCollection<RelatedTopic> RelatedTopics => _relatedTopics.AsReadOnly();
        public void AddRelatedTopic(RelatedTopic relatedTopic)
        {
            if (!_relatedTopics.Contains(relatedTopic))
            {
                _relatedTopics.Add(relatedTopic);
                return;
            }
        }
        public void RemoveRelatedTopic(RelatedTopic referencedTopic)
        {
            _relatedTopics.Any(x => x.ChildTopicId == referencedTopic.Id);
            _relatedTopics.Remove(referencedTopic);
        }

        private readonly List<ReferencedFragment> _referencedFragments = new List<ReferencedFragment>();
        public IReadOnlyCollection<ReferencedFragment> ReferencedFragments => _referencedFragments.AsReadOnly();
        public void AddReferencedFragments(ReferencedFragment referencedTopic)
        {
            if (!_referencedFragments.Any(x => x.ChildTopicId == referencedTopic.Id))
            {
                _referencedFragments.Add(referencedTopic);
                return;
            }
        }
        public void RemoveReferencedFragments(ReferencedFragment referencedTopic)
        {
            _referencedFragments.Any(x => x.ChildTopicId == referencedTopic.Id);
            _referencedFragments.Remove(referencedTopic);
        }

        private readonly List<CollectionElement> _collectionElements = new List<CollectionElement>();
        public IReadOnlyCollection<CollectionElement> CollectionElements => _collectionElements.AsReadOnly();
        public void AddCollectionElement(CollectionElement collectionElement)
        {
            if (!_collectionElements.Any(x => x.ProjectId == collectionElement.ProjectId && x.Id == collectionElement.Id))
            {
                _collectionElements.Add(collectionElement);
                return;
            }
        }
        public void RemoveCollectionElement(CollectionElement collectionElement)
        {
            if (_collectionElements.Contains(collectionElement))
            {
                _collectionElements.Remove(collectionElement);
            }
        }
    }
}
