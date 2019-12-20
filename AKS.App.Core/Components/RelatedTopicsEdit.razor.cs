using AKS.Common.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AKS.App.Core.Components
{
    public partial class RelatedTopicsEdit : ComponentBase
    {
        [Parameter]
        public TopicEdit Topic { get; set; } = new TopicEdit();

        public TopicSearchModal TopicSearcher { get; set; } = null!;

        public void ShowAddRelatedTopic()
        {
            TopicSearcher.ShowModal();
        }

        protected void AddRelatedTopic(List<TopicList> topics)
        {
            foreach (var topic in topics)
            {
                var relatedTopic = new TopicLink
                {
                    TopicId = topic.TopicId,
                    ProjectId = topic.ProjectId,
                    Title = topic.Title,
                    Description = topic.Description
                };
                Topic.RelatedTopics.Add(relatedTopic);
            }

            StateHasChanged();
        }
    }
}
