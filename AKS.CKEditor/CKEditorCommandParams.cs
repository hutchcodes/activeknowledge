using System;
using System.Collections.Generic;
using System.Text;

namespace AKS.CKEditor
{
    public class CKEditorCommandParams
    {
        public string CKEditorId { get; set; }
        public string CommandName { get; set; }

        public dynamic Data { get; set; }
    }
}
