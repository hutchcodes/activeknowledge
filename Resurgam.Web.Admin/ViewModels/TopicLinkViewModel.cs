using Resurgam.AppCore.Entities;
using Resurgam.AppCore.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resurgam.Web.Admin.ViewModels
{
    public class TopicLinkViewModel
    {
        public TopicLinkViewModel(IReferencedTopic referencedTopic)
        {
            ProjectId = referencedTopic.ProjectId;
            TopicId = referencedTopic.ChildTopicId;
            TopicName = referencedTopic.ChildTopic.Name;
        }

        public TopicLinkViewModel(Topic referencedTopic)
        {
            ProjectId = referencedTopic.ProjectId;
            TopicId = referencedTopic.Id;
            TopicName = referencedTopic.Name;
        }

        public int ProjectId { get; set; }
        public int TopicId { get; set; }
        public string TopicName { get; set; }
    }
}
