using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using System.Text;

namespace AKS.Infrastructure.DTO
{
    public class Document
    {
        public Guid? CustomerId { get; set; }
        public Guid? ProjectId { get; set; }
        public Guid? TopicId { get; set; }
        public string? Name { get; set; }
        public Stream? Content { get; set; }
        public string? ContentType { get; set; }
        public string? ETag { get; set; }
        public DateTimeOffset? LastModified { get; set; }
    }
}
