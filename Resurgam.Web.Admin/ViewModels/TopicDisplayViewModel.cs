using Resurgam.AppCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resurgam.Web.Admin.ViewModels
{
    public class TopicDisplayViewModel
    {
        public TopicDisplayViewModel(Topic topic)
        {
            ProjectId = topic.ProjectId;
            TopicId = topic.Id;
            Name = topic.Name;
            Content = topic.TopicContent;
            TopicTypeID = topic.TopicTypeId;

            foreach(var ce in topic.CollectionElements)
            {
                CollectionElements.Add(new CollectionElementViewModel(ce));
            }

            foreach(var tag in topic.Tags)
            {
                Tags.Add(new TagViewModel(tag));
            }

            foreach(var refTopic in topic.RelatedTopics)
            {
                RelatedTopics.Add(new TopicLinkViewModel(refTopic));
            }
        }
        public int ProjectId { get; set; }
        public int TopicId { get; set; }
        public int TopicTypeID { get; set; }
        public string Name { get; set; }

        public string Content { get; set; }

        public List<TopicLinkViewModel> RelatedTopics { get; } = new List<TopicLinkViewModel>();
        public List<TagViewModel> Tags { get; } = new List<TagViewModel>();
        public List<CollectionElementViewModel> CollectionElements { get; } = new List<CollectionElementViewModel>();
    }
}
