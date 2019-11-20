using System;
using System.Collections.Generic;
using System.Text;

namespace AKS.Common.Enums
{
    public enum TopicStatus
    {
        New = 0,
        Edited = 1,
        ReadyForReview = 2,
        ReadyToPublish = 3,
        Published = 4,

        Deleted = 99
    }
}
