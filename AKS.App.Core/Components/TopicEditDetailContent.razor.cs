using AKS.Common.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AKS.App.Core.Components
{
    public class TopicEditDetailContentBase : ComponentBase
    {
        public TopicEditDetailContentBase()
        {
            InsertTopicFragmentAction = InsertTopicFragment;

        }

        private string _ckEditorCommandName = "";

        [Parameter]
        public TopicEdit Topic { get; set; } = null!;

        public CKEditor.CKEditorControl TopicContentEditor { get; set; } = null!;

        public bool IsAddingTopics { get; set; }

        private void SelectTopicFragmentAction(string commandName)
        {
            _ckEditorCommandName = commandName;
            IsAddingTopics = true;
            StateHasChanged();
        }

        public override Task SetParametersAsync(ParameterView parameters)
        {
            JSInterop.AKSInterop.SelectTopicAction = SelectTopicFragmentAction;
            return base.SetParametersAsync(parameters);
        }

        protected void CloseModal()
        {
            IsAddingTopics = false;
        }

        protected Action<List<TopicList>> InsertTopicFragmentAction;

        protected void InsertTopicFragment(List<TopicList> topics)
        {
            if (topics.Count == 1)
            {
                var topicFragment = topics.First();
                TopicContentEditor.InsertTopicFragment(topicFragment, _ckEditorCommandName);

                IsAddingTopics = false;
            }
            StateHasChanged();
        }
    }
}
