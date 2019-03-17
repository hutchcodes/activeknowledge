using Microsoft.AspNetCore.Components;
using Resurgam.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AKS.Builder.Components.Shared
{
    public class ContentTopicEditorModel : ComponentBase
    {
        [Parameter]
        protected TopicEditViewModel Topic { get; set; }
        public CKEditor.Blazor.CKEditorControl TopicContentEditor { get; set; }

        protected bool IsAddingTopics { get; set; }

        private void SelectTopicFragmentAction (string commandName)
        {
            IsAddingTopics = true;
            StateHasChanged();
        }

        public override Task SetParametersAsync(ParameterCollection parameters)
        {
            JSInterop.AKSInterop.SelectTopicAction = SelectTopicFragmentAction;
            return base.SetParametersAsync(parameters);
        }

        protected void CloseModal()
        {
            IsAddingTopics = false;
        }

        protected void AddTopicToElement(List<TopicListViewModel> topics)
        {
            var topicFragment = topics.First();
            TopicContentEditor.InsertTopicFragment(topicFragment);
           
            IsAddingTopics = false;
            StateHasChanged();
        }
        //new TopicListViewModel { TopicId = Guid.NewGuid(), TopicName = "Test Topic Title" };
    }
}
