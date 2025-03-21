﻿using System;

namespace AKS.Infrastructure.Entities.Interfaces
{
    public interface IReferencedTopic 
    {
        Guid ProjectId { get; set; }
        Guid ParentTopicId { get; set; }
        Topic? ParentTopic { get; set; }
        Guid ChildTopicId { get; set; }
        Topic? ChildTopic { get; set; }
    }
}
