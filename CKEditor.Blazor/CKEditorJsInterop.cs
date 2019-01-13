using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace CKEditor.Blazor
{
    public class CKEditorJsInterop
    {
        public static event EventHandler<string> EditorUpdate;

        private static string _editorText { get; set; }

        public static Task<string> InitializeEditor(string ckEditorId, string initialContent)
        {
            return JSRuntime.Current.InvokeAsync<string>("ckEditorJsInterop.initializeCKEditor", new { ckEditorId });
        }

        [JSInvokable]
        public static Task<bool> UpdateText(string editorText)
        {
            _editorText = editorText;
            EditorUpdate?.Invoke(null, editorText);

            return Task.FromResult(true);
        }

        public static Task<bool> DestroyCKEditor(string ckEditorId)
        {
            return JSRuntime.Current.InvokeAsync<bool>("ckEditorJsInterop.destroyCKEditor", ckEditorId);
        }
    }
}
