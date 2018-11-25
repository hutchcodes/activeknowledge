using Resurgam.AppCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resurgam.Infrastructure.ViewModels
{
    public class TopicDisplayViewModel
    {
        public TopicDisplayViewModel() { }
        public TopicDisplayViewModel(Topic topic)
        {
            ProjectId = topic.ProjectId;
            TopicId = topic.TopicId;
            TopicName = topic.Name;
            TopicDescription = topic.Description;
            Content = topic.TopicContent;
            TopicTypeID = topic.TopicTypeId;
            DocumentName = topic.DocumentName;

            ReplaceFragmentContents(topic);
            CleanTopicContent();

            foreach (var ce in topic.CollectionElements)
            {
                CollectionElements.Add(new CollectionElementViewModel(ce));
            }

            foreach(var tag in topic.Tags)
            {
                Tags.Add(new TagViewModel(tag));
            }

            foreach(var refTopic in topic.RelatedToTopics)
            {
                RelatedTopics.Add(new TopicLinkViewModel(refTopic));
            }
        }

        public Guid ProjectId { get; set; }
        public Guid TopicId { get; set; }
        public int TopicTypeID { get; set; }
        public string TopicName { get; set; }
        public string TopicDescription { get;set; }
        public string DocumentName { get; set; }
        public string Content { get; set; }

        public List<TopicLinkViewModel> RelatedTopics { get; } = new List<TopicLinkViewModel>();
        public List<TagViewModel> Tags { get; } = new List<TagViewModel>();
        public List<CollectionElementViewModel> CollectionElements { get; } = new List<CollectionElementViewModel>();

        private void CleanTopicContent()
        {
            if (string.IsNullOrWhiteSpace(Content))
            {
                return;
            }
            var topicContent = Content.Replace("{{projectId}}", ProjectId.ToString());

            Content = topicContent;
        }

        private void ReplaceFragmentContents(Topic topic)
        {
            if (string.IsNullOrEmpty(Content))
            {
                return;
            }
            var contentHtml = new HtmlAgilityPack.HtmlDocument();
            contentHtml.LoadHtml(this.Content);

            foreach (var frag in topic.ReferencedFragments)
            {
                var fragmentNodes = contentHtml.DocumentNode.SelectNodes($"//fragment[@topicid='{frag.ChildTopicId}']");
                if (fragmentNodes == null)
                {
                    continue;
                }
                foreach (var fragNode in fragmentNodes)
                {
                    fragNode.InnerHtml = frag.ChildTopic.TopicContent;
                }
            }

            this.Content = contentHtml.DocumentNode.OuterHtml;
        }

        public Topic ToTopicEntity()
        {
            var topic = new Topic();
            topic.ProjectId = ProjectId;
            topic.TopicId = TopicId;
            topic.Name = TopicName;
            topic.Description = TopicDescription;
            topic.TopicContent = Content;
            topic.TopicTypeId = TopicTypeID;
            topic.DocumentName = DocumentName;

            foreach (var ce in CollectionElements)
            {
                var collectionElement = new CollectionElement();
                topic.AddCollectionElement(collectionElement);
            }

            foreach (var tag in Tags)
            {
                topic.AddTag(tag.TagId, tag.TagName);
            }

            foreach (var refTopic in RelatedTopics)
            {
                //topic.AddRelatedTopic(refTopic.ToRelatedTopic());
            }

            return topic;
        }
    }
}
