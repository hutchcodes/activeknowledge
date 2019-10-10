using System;
using System.Collections.Generic;
using System.Text;

namespace AKS.Infrastructure.Enums
{
    public enum FileMetaData
    {
        [StringValue("CustomerId")]
        CustomerId,
        [StringValue("ProjectId")]
        ProjectId,
        [StringValue("TopicId")]
        TopicId,
    }
}
