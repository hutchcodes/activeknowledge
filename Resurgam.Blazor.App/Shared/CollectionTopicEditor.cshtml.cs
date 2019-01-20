using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Blazor.Components;
using Resurgam.Infrastructure.Interfaces;
using Resurgam.Infrastructure.ViewModels;

namespace Resurgam.Blazor.App.Shared
{
    public class CollectionTopicEditorModel : BlazorComponent
    {
        [Parameter]
        protected TopicEditViewModel Topic { get; set; }

        protected bool IsAddingTopics { get; set; }

        private CollectionElementViewModel _currentElement;
        protected void AddTopic(CollectionElementViewModel currentElement)
        {
            _currentElement = currentElement;
            IsAddingTopics = true;
        }

        protected void CloseModal()
        {
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
