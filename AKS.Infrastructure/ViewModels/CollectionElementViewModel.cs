using AKS.AppCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AKS.Infrastructure.ViewModels
{
    public class CollectionElementViewModel
    {
        public CollectionElementViewModel() { }
        public CollectionElementViewModel(CollectionElement collectionElement)
        {
            ProjectId = collectionElement.ProjectId;
            CollectionElementId = collectionElement.CollectionElementId;
            CollectionElementName = collectionElement.Name;

            foreach(var t in collectionElement.ElementTopics)
            {
                Topics.Add(new TopicDisplayViewModel(t));
            }
        }
        public Guid ProjectId { get; set; }
        public Guid CollectionElementId { get; set; }
        public string CollectionElementName { get; set; }
        public List<TopicDisplayViewModel> Topics { get; } = new List<TopicDisplayViewModel>();

        public CollectionElement ToCollectionElement()
        {
            var collectionElement = new CollectionElement();

            collectionElement.CollectionElementId = CollectionElementId;
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
