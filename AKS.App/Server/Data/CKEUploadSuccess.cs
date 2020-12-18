using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AKS.Api.Build.Data
{
    public class CKEUploadSuccess
    {
        public CKEUploadSuccess(string defaultImageUrl)
        {
            Urls["default"] = defaultImageUrl;
        }
        public Dictionary<string, string> Urls { get; } = new Dictionary<string, string>();
    }
}
