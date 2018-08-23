using Resurgam.AppCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resurgam.Infrastructure.ViewModels
{
    public class TopicListViewModel
    {
        public TopicListViewModel(Topic topic)
        {
            ProjectId = topic.ProjectId;
            TopicID = topic.Id;
            TopicName = topic.Name;
            TopicDesription = topic.Description;
        }

        public int ProjectId { get; set; }
        public int TopicID { get; set; }
        public string TopicName { get; set; }
        public string TopicDesription { get; set; }

    }
}
