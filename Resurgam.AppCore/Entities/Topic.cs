using Resurgam.AppCore.Specifications;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Resurgam.AppCore.Entities
{
    public class Topic : BaseEntity
    {
        public Guid TopicId { get; set; }
        public Guid ProjectId { get; set; }
        public int TopicTypeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid? ImageResourceId { get; set; }
        public string TopicContent { get; set; }
        public Guid? FileResourceId { get; set; }
        public string DocumentName { get; set; }

        public Guid? DefaultCategoryId { get; set; }

        private readonly List<Tag> _tags = new List<Tag>();
        public IReadOnlyCollection<Tag> Tags => _tags.AsReadOnly();
        public void AddTag(Guid tagId, string tagName)
        {
            if (!_tags.Any(new TagFilterSpecification(this.ProjectId, tagId).CompiledCriteria))
            {
                _tags.Add(new Tag()
                {
                    TagId = tagId,
                    ProjectId = this.ProjectId,
                    Name = tagName,
                });
                return;
            }
        }
        public void RemoveTag(Guid tagId)
        {
            var tag = _tags.FirstOrDefault(new TagFilterSpecification(this.ProjectId, tagId).CompiledCriteria);
            if (tag != null)
            {
                _tags.Remove(tag);
            }
        }

        private readonly List<RelatedTopic> _relatedToTopics = new List<RelatedTopic>();
        public IReadOnlyCollection<RelatedTopic> RelatedToTopics => _relatedToTopics.AsReadOnly();
        public void AddRelatedToTopic(RelatedTopic relatedTopic)
        { 
            if (!_relatedToTopics.Contains(relatedTopic))
            {
                _relatedToTopics.Add(relatedTopic);
                return;
            }
        }
        public void RemoveRelatedToTopic(RelatedTopic relatedTopic)
        {
            _relatedToTopics.Any(x => x.ChildTopicId == relatedTopic.ChildTopicId);
            _relatedToTopics.Remove(relatedTopic);
        }

        private readonly List<RelatedTopic> _relatedFromTopics = new List<RelatedTopic>();
        public IReadOnlyCollection<RelatedTopic> RelatedFromTopics => _relatedFromTopics.AsReadOnly();

        public void AddRelatedFromTopic(RelatedTopic relatedTopic)
        {
            if (!_relatedToTopics.Contains(relatedTopic))
            {
                _relatedToTopics.Add(relatedTopic);
                return;
            }
        }
        public void RemoveRelatedFromTopic(RelatedTopic relatedTopic)
        {
            _relatedToTopics.Any(x => x.ChildTopicId == relatedTopic.ChildTopicId);
            _relatedToTopics.Remove(relatedTopic);
        }

        private readonly List<ReferencedFragment> _referencedFragments = new List<ReferencedFragment>();
        public IReadOnlyCollection<ReferencedFragment> ReferencedFragments => _referencedFragments.AsReadOnly();
        public void AddReferencedFragments(ReferencedFragment referencedFragment)
        {
            if (!_referencedFragments.Any(x => x.ChildTopicId == referencedFragment.ChildTopicId))
            {
                _referencedFragments.Add(referencedFragment);
                return;
            }
        }
        public void RemoveReferencedFragments(ReferencedFragment referencedFragment)
        {
            if (_referencedFragments.Any(x => x.ChildTopicId == referencedFragment.ChildTopicId))
            {
                _referencedFragments.Remove(referencedFragment);
            }
        }

        private readonly List<ReferencedFragment> _fragmentReferencedBy = new List<ReferencedFragment>();
        public IReadOnlyCollection<ReferencedFragment> FragmentReferencedBy => _fragmentReferencedBy.AsReadOnly();
        public void AddFragmentReferencedBy(ReferencedFragment referencedFragment)
        {
            if (!_fragmentReferencedBy.Any(x => x.ParentTopicId == referencedFragment.ParentTopicId))
            {
                _fragmentReferencedBy.Add(referencedFragment);
                return;
            }
        }
        public void RemoveFragmentReferencedBy(ReferencedFragment referencedFragment)
        {
            if (_fragmentReferencedBy.Any(x => x.ParentTopicId == referencedFragment.ParentTopicId))
            {
                _fragmentReferencedBy.Remove(referencedFragment);
            }
        }

        private readonly List<CollectionElement> _collectionElements = new List<CollectionElement>();
        public IReadOnlyCollection<CollectionElement> CollectionElements => _collectionElements.AsReadOnly();
        public void AddCollectionElement(CollectionElement collectionElement)
        {
            if (!_collectionElements.Any(x => x.ProjectId == collectionElement.ProjectId && x.CollectionElementId == collectionElement.CollectionElementId))
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
