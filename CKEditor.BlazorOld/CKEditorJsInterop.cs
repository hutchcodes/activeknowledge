using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace CKEditor.Blazor
{
    public class CKEditorJsInterop
    {
        public static event EventHandler<string> EditorUpdate;

        private static string _editorText { get; set; }

        public static Task<string> InitializeEditor(IJSRuntime jsruntime, string ckEditorId, string initialContent)
        {
            return jsruntime.InvokeAsync<string>("ckEditorJsInterop.initializeCKEditor", new { ckEditorId });
        }

        public static Task<string> GetData(IJSRuntime jsruntime, string ckEditorId)
        {
            return jsruntime.InvokeAsync<string>("ckEditorJsInterop.getData", ckEditorId );
        }

        public static Task<string> ExecuteCKCommand(IJSRuntime jsruntime, string ckEditorId, string commandName, dynamic data)
        {
            return jsruntime.InvokeAsync<string>("ckEditorJsInterop.executeCKCommand", ckEditorId, commandName, data);
        }

        [JSInvokable]      
        public static Task<bool> UpdateText(string editorText)
        {
            _editorText = editorText;
            EditorUpdate?.Invoke(null, editorText);

            return Task.FromResult(true);
        }

        public static Task<bool> DestroyCKEditor(IJSRuntime jsruntime, string ckEditorId)
        {
            return jsruntime.InvokeAsync<bool>("ckEditorJsInterop.destroyCKEditor", ckEditorId);
        }
    }
}
