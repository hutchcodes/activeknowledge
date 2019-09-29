using System;
using System.Collections.Generic;
using System.Text;
using Ents = AKS.AppCore.Entities;

namespace AKS.Infrastructure
{
    static class TopicCleaner
    {
        internal static void CleanTopicContent(Ents.Topic topic)
        {
            if (string.IsNullOrWhiteSpace(topic.Content))
            {
                return;
            }

            ReplaceProjectId(topic);
            ReplaceFragmentContents(topic);
        }

        private static Ents.Topic ReplaceProjectId(Ents.Topic topic)
        {
            topic.Content = topic.Content?.Replace("{{projectId}}", topic.ProjectId.ToString());
            return topic;
        }

        private static void ReplaceFragmentContents(Ents.Topic topic)
        {
            if (string.IsNullOrEmpty(topic.Content))
            {
                return;
            }
            var contentHtml = new HtmlAgilityPack.HtmlDocument();
            contentHtml.LoadHtml(topic.Content);

            foreach (var frag in topic.TopicFragmentChildren)
            {
                var fragmentNodes = contentHtml.DocumentNode.SelectNodes($"//fragment[@topicid='{frag.ChildTopicId}']");
                if (fragmentNodes == null)
                {
                    continue;
                }
                foreach (var fragNode in fragmentNodes)
                {
                    fragNode.InnerHtml = frag.ChildTopic?.Content;
                }
            }

            topic.Content = contentHtml.DocumentNode.OuterHtml;
        }
    }
}
