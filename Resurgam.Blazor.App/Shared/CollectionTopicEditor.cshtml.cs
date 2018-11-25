using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Blazor.Components;
using Resurgam.Infrastructure.ViewModels;

namespace Resurgam.Blazor.App.Shared
{
    public class CollectionTopicEditorModel : BlazorComponent
    {
        [Parameter]
        protected TopicEditViewModel Topic { get; set; }

        protected void RemoveTopic (CollectionElementViewModel element, TopicDisplayViewModel topic)
        {
            element.Topics.Remove(topic);
        }
    }
}
