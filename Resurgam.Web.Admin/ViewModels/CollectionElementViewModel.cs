using Resurgam.AppCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resurgam.Web.Admin.ViewModels
{
    public class CollectionElementViewModel
    {
        public CollectionElementViewModel(CollectionElement collectionElement)
        {
            ProjectId = collectionElement.ProjectId;
            CollectionElementId = collectionElement.Id;
            CollectionElementName = collectionElement.Name;

            foreach(var t in collectionElement.ElementTopics)
            {
                Topics.Add(new TopicLinkViewModel(t));
            }
        }
        public int ProjectId { get; set; }
        public int CollectionElementId { get; set; }
        public string CollectionElementName { get; set; }
        public List<TopicLinkViewModel> Topics { get; } = new List<TopicLinkViewModel>();
    }
}
