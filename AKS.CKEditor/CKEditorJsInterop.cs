using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace AKS.CKEditor
{
    public static class CKEditorJsInterop
    {
        public static event EventHandler<string> EditorUpdate;

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

        [JSInvokable]      
        public static Task<bool> UpdateText(string editorText)
        {
            EditorText = editorText;
            EditorUpdate?.Invoke(null, editorText);

            return Task.FromResult(true);
        }

        public static ValueTask<bool> DestroyCKEditor(IJSRuntime jsruntime, string ckEditorId)
        {
            return jsruntime.InvokeAsync<bool>("ckEditorJsInterop.destroyCKEditor", ckEditorId);
        }
    }
}
