using System;
using System.Collections.Generic;
using System.Text;

namespace Resurgam.AppCore.Enums
{
    public enum FileStorageType
    {
        [StringValue("projectlogos")]
        ProjectLogos,
        [StringValue("customerlogos")]
        CustomerLogos,
        [StringValue("contentimages")]
        ContentImages,
        [StringValue("contentdocuments")]
        ContentDocuments
    }
}
