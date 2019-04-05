using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AKS.Common.Models
{
    
    public class TopicEdit
    {
        public Guid ProjectId { get; set; }
        public Guid TopicId { get; set; }
        public int TopicTypeID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public string DocumentName { get; set; }
        public bool IsSelected { get; set; }

        public List<TopicLink> RelatedTopics { get; } = new List<TopicLink>();
        public List<Tag> Tags { get; } = new List<Tag>();
        public List<CollectionElement> CollectionElements { get; } = new List<CollectionElement>();

        //private void ReplaceFragmentContents(Topic topic)
        //{
        //    if (string.IsNullOrEmpty(TopicContent))
        //    {
        //        return;
        //    }
        //    var contentHtml = new HtmlAgilityPack.HtmlDocument();
        //    contentHtml.LoadHtml(this.TopicContent);

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

        //    this.TopicContent = contentHtml.DocumentNode.OuterHtml;
        //}

        private void CleanTopicContent()
        {
            if (string.IsNullOrWhiteSpace(Content))
            {
                return;
            }
            var topicContent = Content.Replace("{{projectId}}", ProjectId.ToString());

            Content = topicContent;
        }
    }
}
