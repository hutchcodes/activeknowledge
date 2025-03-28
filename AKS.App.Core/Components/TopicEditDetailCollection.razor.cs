﻿using AKS.Common.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AKS.App.Core.Components
{
    public partial class TopicEditDetailCollection : ComponentBase
    {
        [Parameter] public TopicEdit Topic { get; set; } = null!;

        protected bool IsAddingElements { get; set; }
        protected CollectionElementEdit? NewCollectionElement { get; set; }

        private CollectionElementEdit? _currentElement;
        private TopicSearchModal TopicSearcher { get; set; } = null!;
        protected void AddElement()
        {
            IsAddingElements = true;
            NewCollectionElement = new CollectionElementEdit() { ProjectId = Topic.ProjectId, CollectionElementId = Guid.NewGuid() };
        }
        protected void AddElementToTopic()
        {
            if (NewCollectionElement == null) return;
            IsAddingElements = false;
            Topic.CollectionElements.Add(NewCollectionElement);
            NewCollectionElement = null;
        }

        protected void DeleteElement(CollectionElementEdit element)
        {
            if (Topic.CollectionElements.Contains(element))
            {
                Topic.CollectionElements.Remove(element);
            }
            StateHasChanged();
        }

        protected void AddTopic(CollectionElementEdit currentElement)
        {
            _currentElement = currentElement;
            TopicSearcher.ShowModal();
        }

        protected void AddTopicToElement(List<TopicList> topics)
        {
            if (_currentElement == null) return;
            foreach (var topic in topics)
            {
                var collectionElementTopic = new CollectionElementTopicList
                {
                    CollectionElementId = _currentElement.CollectionElementId,
                    ProjectId = _currentElement.ProjectId,
                    TopicId = topic.TopicId,
                    Topic = topic
                };
                _currentElement.ElementTopics.Add(collectionElementTopic);
            }
            StateHasChanged();
        }

        protected void RemoveTopic(CollectionElementEdit element, CollectionElementTopicList topic)
        {
            element.ElementTopics.Remove(topic);
        }
    }
}
