using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using System.Text;

namespace Resurgam.AppCore.DTO
{
    public class Document
    {
        public int? CustomerId { get; set; }
        public int? ProjectId { get; set; }
        public string Name { get; set; }
        public Stream Content { get; set; }
        public string ContentType { get; set; }
        public string ETag { get; set; }
        public DateTimeOffset? LastModified { get; set; }
    }
}
