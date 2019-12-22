using AKS.Common.Models;
using AKS.Common;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AKS.App.Core.Components
{
    public partial class TopicEditDetailContent : ComponentBase
    {
        public TopicEditDetailContent()
        {
            InsertTopicFragmentAction = InsertTopicFragment;

        }

        private string _ckEditorCommandName = "";
        private TopicSearchModal TopicSearcher { get; set; } = null!;

        [Parameter]
        public TopicEdit Topic { get; set; } = null!;

        public CKEditor.CKEditorControl TopicContentEditor { get; set; } = null!;

        public bool IsAddingTopics { get; set; }

        public string ContentImageUploadUrl { get; set; } = "";

        private void SelectTopicFragmentAction(string commandName)
        {
            _ckEditorCommandName = commandName;
            TopicSearcher.ShowModal();
            StateHasChanged();
        }

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters);
            if (Topic != null)
            {
                ContentImageUploadUrl = $"{ConfigSettings.BuildApiBaseUrl}ContentImage/{Topic.ProjectId}/{Topic.TopicId}/";

                JSInterop.AKSInterop.SelectTopicAction = SelectTopicFragmentAction;
            }
        }

        protected void CloseModal()
        {
            TopicSearcher.CloseModal();
        }

        protected Action<List<TopicList>> InsertTopicFragmentAction;

        protected void InsertTopicFragment(List<TopicList> topics)
        {
            if (topics.Count == 1)
            {                
                var topicFragment = topics.First();
                TopicContentEditor.InsertTopicFragment(topicFragment, _ckEditorCommandName);
            }
            StateHasChanged();
        }

        public void TopicContentEditor_OnEditorChanged(string content)
        {
            Topic.Content = content;
        }
    }
}
