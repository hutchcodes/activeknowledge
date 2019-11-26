using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace AKS.CKEditor
{
    public static class CKEditorJsInterop
    {
        public class EditorUpdateEventData { public string ckEditorId; public string editorText; }
        public static event EventHandler<EditorUpdateEventData> EditorUpdate;

        private static string EditorText { get; set; }

        public static ValueTask<string> InitializeEditor(IJSRuntime jsruntime, string ckEditorId)
        {
            return jsruntime.InvokeAsync<string>("ckEditorJsInterop.initializeCKEditor", new { ckEditorId });
        }

        public static ValueTask<string> GetData(IJSRuntime jsruntime, string ckEditorId)
        {
            return jsruntime.InvokeAsync<string>("ckEditorJsInterop.getData", ckEditorId );
        }

        public static ValueTask<string> ExecuteCKCommand(IJSRuntime jsruntime, CKEditorCommandParams parameters)
        {
            return jsruntime.InvokeAsync<string>("ckEditorJsInterop.executeCKCommand", parameters);
        }

        [JSInvokable("CKEditorUpdateText")]      
        public static Task<bool> UpdateText(string ckEditorId, string editorText)
        {
            EditorText = editorText;
            var editorUpdateEventData = new EditorUpdateEventData
            {
                ckEditorId = ckEditorId,
                editorText = editorText
            };
            EditorUpdate?.Invoke(null, editorUpdateEventData);

            return Task.FromResult(true);
        }

        public static ValueTask<bool> DestroyCKEditor(IJSRuntime jsruntime, string ckEditorId)
        {
            return jsruntime.InvokeAsync<bool>("ckEditorJsInterop.destroyCKEditor", ckEditorId);
        }
    }
}
