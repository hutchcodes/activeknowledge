using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AKS.Common.Models
{
    public class TopicView
    {
        public Guid ProjectId { get; set; }
        public Guid TopicId { get; set; }
        public int TopicTypeId { get; set; }
        public string Title  { get; set; }
        public string Description { get; set; }
        public string DocumentName { get; set; }
        public string Content { get; set; }

        public List<TopicLink> RelatedTopics { get; } = new List<TopicLink>();
        public List<Tag> Tags { get; set; } = new List<Tag>();
        public List<CollectionElement> CollectionElements { get; } = new List<CollectionElement>();

        private void CleanTopicContent()
        {
            if (string.IsNullOrWhiteSpace(Content))
            {
                return;
            }
            var topicContent = Content.Replace("{{projectId}}", ProjectId.ToString());

            Content = topicContent;
        }

        //private void ReplaceFragmentContents(Topic topic)
        //{
        //    if (string.IsNullOrEmpty(Content))
        //    {
        //        return;
        //    }
        //    var contentHtml = new HtmlAgilityPack.HtmlDocument();
        //    contentHtml.LoadHtml(this.Content);

        //    foreach (var frag in topic.ReferencedFragments)
        //    {
        //        var fragmentNodes = contentHtml.DocumentNode.SelectNodes($"//fragment[@topicid='{frag.ChildTopicId}']");
        //        if (fragmentNodes == null)
        //        {
        //            continue;
        //        }
        //        foreach (var fragNode in fragmentNodes)
        //        {
        //            fragNode.InnerHtml = frag.ChildTopic.TopicContent;
        //        }
        //    }

        //    this.Content = contentHtml.DocumentNode.OuterHtml;
        //}
    }
}
