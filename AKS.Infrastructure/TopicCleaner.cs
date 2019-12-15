using AKS.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ents = AKS.Infrastructure.Entities;
using Mods = AKS.Common.Models;

namespace AKS.Infrastructure
{
    static class TopicCleaner
    {
        internal static void CompileTopicForView(Ents.Topic topic)
        {
            if (string.IsNullOrWhiteSpace(topic.Content))
            {
                return;
            }

            DetokenizeTopicContent(topic);
            ReplaceFragmentTokenWithContents(topic);
        }

        internal static void TokenizeTopicContent(Mods.TopicEdit topic)
        {
            if (string.IsNullOrWhiteSpace(topic.Content))
            {
                return;
            }
            ReplaceApiUrlWithToken(topic);
            ReplaceProjectIdWithToken(topic);
            AddFragmentEntityForFragments(topic);
        }

        internal static void DetokenizeTopicContent(Ents.Topic topic)
        {
            ReplaceProjectTokenWithId(topic);
            ReplaceApiTokenWithUrl(topic);
        }

        private static Ents.Topic ReplaceProjectTokenWithId(Ents.Topic topic)
        {
            topic.Content = topic.Content?.Replace("{{ProjectId}}", topic.ProjectId.ToString());
            return topic;
        }

        private static Mods.TopicEdit ReplaceProjectIdWithToken(Mods.TopicEdit topic)
        {
            topic.Content = topic.Content?.Replace(topic.ProjectId.ToString(), "{{ProjectId}}");
            return topic;
        }

        private static Ents.Topic ReplaceApiTokenWithUrl(Ents.Topic topic)
        {
            topic.Content = topic.Content?.Replace("{{ApiBaseUrl}}", ConfigSettings.ThisApiBaseUrl);
            return topic;
        }

        private static Mods.TopicEdit ReplaceApiUrlWithToken(Mods.TopicEdit topic)
        {
            topic.Content = topic.Content?.Replace(ConfigSettings.ThisApiBaseUrl, "{{ApiBaseUrl}}");
            return topic;
        }

        private static void ReplaceFragmentTokenWithContents(Ents.Topic topic)
        {
            try
            {
                if (string.IsNullOrEmpty(topic.Content))
                {
                    return;
                }
                var contentHtml = new HtmlAgilityPack.HtmlDocument();
                contentHtml.LoadHtml(topic.Content);

                foreach (var frag in topic.TopicFragmentChildren)
                {
                    var fragmentNodes = contentHtml.DocumentNode.SelectNodes($"//topicfragment[@topicid='{frag.ChildTopicId}']");
                    if (fragmentNodes == null)
                    {
                        continue;
                    }
                    foreach (var fragNode in fragmentNodes)
                    {
                        fragNode.InnerHtml = frag.ChildTopic?.Content ?? "";
                    }
                }

                topic.Content = contentHtml.DocumentNode.OuterHtml;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
        }


        private static void AddFragmentEntityForFragments(Mods.TopicEdit topic)
        {
            topic.FragmentsUsed.Clear();
            if (string.IsNullOrEmpty(topic.Content))
            {
                return;
            }
            var contentHtml = new HtmlAgilityPack.HtmlDocument();
            contentHtml.LoadHtml(topic.Content);

            var fragmentNodes = contentHtml.DocumentNode.SelectNodes($"//topicfragment[@topicid]");
            if (fragmentNodes == null)
            {
                return;
            }
            foreach (var fragNode in fragmentNodes)
            {
                var fragmentId = Guid.Parse(fragNode.GetAttributeValue("topicid", Guid.Empty.ToString()));

                var fragment = new Mods.TopicFragmentLink
                {
                    ProjectId = topic.ProjectId,
                    ParentTopicId = topic.TopicId,
                    TopicId = fragmentId
                };

                if (!topic.FragmentsUsed.Any(x => x.TopicId == fragmentId))
                {
                    topic.FragmentsUsed.Add(fragment);
                }
            }
        }
    }
}
