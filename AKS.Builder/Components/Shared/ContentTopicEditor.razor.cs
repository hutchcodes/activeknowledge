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

        public override Task SetParametersAsync(ParameterCollection parameters)
        {
            var foo = new TopicListViewModel { TopicId = Guid.NewGuid(), TopicName = "Test Topic Title" };
            JSInterop.AKSInterop.SelectTopicAction = (async x => await TopicContentEditor.InsertTopicFragment(foo));
            return base.SetParametersAsync(parameters);
        }
        //new TopicListViewModel { TopicId = Guid.NewGuid(), TopicName = "Test Topic Title" };
    }
}
