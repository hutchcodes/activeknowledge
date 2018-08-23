using Resurgam.AppCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resurgam.Infrastructure.ViewModels
{
    public class CollectionElementViewModel
    {
        public CollectionElementViewModel() { }
        public CollectionElementViewModel(CollectionElement collectionElement)
        {
            ProjectId = collectionElement.ProjectId;
            CollectionElementId = collectionElement.Id;
            CollectionElementName = collectionElement.Name;

            foreach(var t in collectionElement.ElementTopics)
            {
                Topics.Add(new TopicDisplayViewModel(t));
            }
        }
        public int ProjectId { get; set; }
        public int CollectionElementId { get; set; }
        public string CollectionElementName { get; set; }
        public List<TopicDisplayViewModel> Topics { get; } = new List<TopicDisplayViewModel>();

        public CollectionElement ToCollectionElement()
        {
            var collectionElement = new CollectionElement();

            collectionElement.Id = CollectionElementId;
            collectionElement.ProjectId = ProjectId;
            collectionElement.Name = CollectionElementName;

            foreach(var topic in Topics)
            {
                collectionElement.AddTopic(topic.ToTopicEntity());
            }

            return collectionElement;
        }
    }
}
