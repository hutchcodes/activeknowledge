using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace AKS.CKEditor
{
    public class CKEditorControlBase : ComponentBase
    {
        [Parameter]
        public string EditorContent { get; set; }

        [Parameter]
        public Action<string> OnEditorChanged { get; set; }

        [Parameter]
        public string ContentImageUploadUrl { get; set; }

        [Inject]
        protected IJSRuntime JsRuntime { get; set; }

        public string CKEditorId { get; } = $"ck{Guid.NewGuid().ToString().Replace("-", "")}";

        protected override void OnAfterRender(bool firstRender)
        {
            base.OnAfterRender(firstRender);
            if (firstRender)
            {
                CKEditorJsInterop.InitializeEditor(JsRuntime, CKEditorId, ContentImageUploadUrl);
                CKEditorJsInterop.EditorUpdate += CKEditorJsInterop_EditorUpdate;
            }
        }

        private void CKEditorJsInterop_EditorUpdate(object sender, CKEditorJsInterop.EditorUpdateEventData e)
        {
            if (e.ckEditorId == CKEditorId)
            {
                OnEditorChanged?.Invoke(e.editorText);
            }
        }

        public async Task<string> GetEditorText()
        {
            return await CKEditorJsInterop.GetData(JsRuntime, CKEditorId);
        }
        public async Task<string> InsertTopicFragment(dynamic topicFragmentInfo, string ckEditorCommandName)
        {
            var parameters = new CKEditorCommandParams
            {
                CKEditorId = CKEditorId,
                CommandName = ckEditorCommandName,
                Data = topicFragmentInfo
            };

            return await CKEditorJsInterop.ExecuteCKCommand(JsRuntime, parameters);
        }

        public async void Dispose()
        {
            await CKEditorJsInterop.DestroyCKEditor(JsRuntime, CKEditorId);
        }
    }
}
