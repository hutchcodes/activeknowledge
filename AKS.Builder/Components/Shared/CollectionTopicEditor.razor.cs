using System;
using System.Collections.Generic;
using System.Linq;
using AKS.Common.Models;
using Microsoft.AspNetCore.Components;

namespace AKS.Builder.Shared
{
    public class CollectionTopicEditorModel : ComponentBase
    {
        [Parameter]
        protected TopicEdit Topic { get; set; }

        protected bool IsAddingElements { get; set; }
        protected bool IsAddingTopics { get; set; }
        protected CollectionElement NewCollectionElement { get; set; }

        private CollectionElement _currentElement;

        protected void AddElement()
        {
            IsAddingElements = true;
            NewCollectionElement = new CollectionElement() { ProjectId = Topic.ProjectId, CollectionElementId = Guid.NewGuid() };
        }
        protected void AddElementToTopic()
        {
            IsAddingElements = false;
            Topic.CollectionElements.Add(NewCollectionElement);
            NewCollectionElement = null;
        }

        protected void DeleteElement(CollectionElement element)
        {
            if (Topic.CollectionElements.Contains(element))
            {
                Topic.CollectionElements.Remove(element);
            }
            StateHasChanged();
        }

        protected void AddTopic(CollectionElement currentElement)
        {
            _currentElement = currentElement;
            IsAddingTopics = true;
        }

        protected void CloseModal()
        {
            IsAddingElements = false;
            IsAddingTopics = false;            
        }
        protected void AddTopicToElement(List<TopicList> topics)
        {
            foreach (var topic in topics) {
                var collectionElementTopic = new CollectionElementTopic
                {
                    CollectionElementId = _currentElement.CollectionElementId,
                    ProjectId = _currentElement.ProjectId,
                    TopicId = topic.TopicId,
                    Topic = topic
                };
                _currentElement.ElementTopics.Add(collectionElementTopic);
            }

            IsAddingTopics = false;
            StateHasChanged();
        }

        protected void RemoveTopic (CollectionElement element, CollectionElementTopic topic)
        {
            element.ElementTopics.Remove(topic);
        }
    }
}
