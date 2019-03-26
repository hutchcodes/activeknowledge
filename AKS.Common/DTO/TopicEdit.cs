using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AKS.Common.DTO
{
    
    public class TopicEdit
    {
        public Guid ProjectId { get; set; }
        public Guid TopicId { get; set; }
        public int TopicTypeID { get; set; }
        public string TopicName { get; set; }
        public string TopicDescription { get; set; }
        public string TopicContent { get; set; }
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
            if (string.IsNullOrWhiteSpace(TopicContent))
            {
                return;
            }
            var topicContent = TopicContent.Replace("{{projectId}}", ProjectId.ToString());

            TopicContent = topicContent;
        }
    }
}
