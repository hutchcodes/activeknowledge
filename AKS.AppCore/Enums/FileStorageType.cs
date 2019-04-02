using System;
using System.Collections.Generic;
using System.Text;

namespace AKS.AppCore.Enums
{
    public enum FileStorageType
    {
        [StringValue("projectlogos")]
        ProjectLogos,

        [StringValue("customerlogos")]
        CustomerLogos,

        [StringValue("projectimages")]
        ProjectImages,

        [StringValue("contentimages")]
        ContentImages,

        [StringValue("contentdocuments")]
        ContentDocuments
    }
}
