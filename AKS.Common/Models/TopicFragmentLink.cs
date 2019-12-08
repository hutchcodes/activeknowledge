using System;
using System.Collections.Generic;
using System.Text;

namespace AKS.Common.Models
{
    public class TopicFragmentLink : TopicLink
    {
        public Guid ParentTopicId { get; set; }        
    }
}
