using System;
using System.Collections.Generic;
using System.Text;

namespace CKEditor.Blazor
{
    public class CKEditorCommandParams
    {
        public string CKEditorId { get; set; }
        public string CommandName { get; set; }

        public dynamic Data { get; set; }
    }
}
