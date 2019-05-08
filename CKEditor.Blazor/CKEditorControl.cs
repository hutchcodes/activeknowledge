using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace CKEditor.Blazor
{
    public class CKEditorControlModel : ComponentBase
    {
        [Parameter]
        protected string EditorContent { get; set; }

        [Parameter]
        protected Action<string> EditorContentChanged { get; set; }

        protected string CKEditorId { get; set; } = $"ck{Guid.NewGuid().ToString().Replace("-", "")}";

        [Parameter]
        protected Action<string> OnEditorChanged { get; set; }

        [Inject]
        protected IJSRuntime jsRuntime { get; set; }

        protected override void OnAfterRender()
        {
            CKEditorJsInterop.InitializeEditor(jsRuntime, CKEditorId, EditorContent);
        }

        private void ThisEditorUpdate(object sender, string editorText)
        {
            EditorContent = editorText;
            OnEditorChanged?.Invoke(editorText);
        }

        public async Task<string> GetEditorText()
        {
            return await CKEditorJsInterop.GetData(jsRuntime, CKEditorId);
        }
        public async Task<string> InsertTopicFragment(dynamic topicFragmentInfo)
        {
            var parameters = new CKEditorCommandParams
            {
                CKEditorId = CKEditorId,
                CommandName = "topicfragment",
                Data = topicFragmentInfo
            };

            return await CKEditorJsInterop.ExecuteCKCommand(jsRuntime, parameters);
        }

        public async void Dispose()
        {
            await CKEditorJsInterop.DestroyCKEditor(jsRuntime, CKEditorId);
        }
    }
}
