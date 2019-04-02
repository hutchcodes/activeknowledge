using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using AKS.Infrastructure.Interfaces;
using AKS.Infrastructure.ViewModels;

namespace AKS.Builder.Shared
{
    public class CollectionTopicEditorModel : ComponentBase
    {
        [Parameter]
        protected TopicEditViewModel Topic { get; set; }

        protected bool IsAddingElements { get; set; }
        protected bool IsAddingTopics { get; set; }
        protected CollectionElementViewModel NewCollectionElement { get; set; }

        private CollectionElementViewModel _currentElement;

        protected void AddElement()
        {
            IsAddingElements = true;
            NewCollectionElement = new CollectionElementViewModel() { ProjectId = Topic.ProjectId, CollectionElementId = Guid.NewGuid() };
        }
        protected void AddElementToTopic()
        {
            IsAddingElements = false;
            Topic.CollectionElements.Add(NewCollectionElement);
            NewCollectionElement = null;
        }

        protected void DeleteElement(CollectionElementViewModel element)
        {
            if (Topic.CollectionElements.Contains(element))
            {
                Topic.CollectionElements.Remove(element);
            }
            StateHasChanged();
        }

        protected void AddTopic(CollectionElementViewModel currentElement)
        {
            _currentElement = currentElement;
            IsAddingTopics = true;
        }

        protected void CloseModal()
        {
            IsAddingElements = false;
            IsAddingTopics = false;            
        }
        protected void AddTopicToElement(List<TopicListViewModel> topics)
        {
            _currentElement.Topics.AddRange(topics.Select(x => new TopicDisplayViewModel { ProjectId = x.ProjectId, TopicId = x.TopicId, TopicName = x.TopicName, TopicDescription = x.TopicDesription }));
            IsAddingTopics = false;
            StateHasChanged();
        }

        protected void RemoveTopic (CollectionElementViewModel element, TopicDisplayViewModel topic)
        {
            element.Topics.Remove(topic);
        }
    }
}
