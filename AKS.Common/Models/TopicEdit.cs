using AKS.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AKS.Common.Models
{
    
    public class TopicEdit
    {
        public Guid ProjectId { get; set; }
        public Guid TopicId { get; set; }
        public TopicType TopicType { get; set; }
        public TopicStatus TopicStatus { get; set; }
        [Required]
        [StringLength(50)] 
        public string? Title { get; set; } = "";
        [StringLength(200)] 
        public string? Description { get; set; }
        public string? Content { get; set; }
        public string? DocumentName { get; set; }

        public List<TopicLink> RelatedTopics { get; set; } = new List<TopicLink>();
        public List<Tag> Tags { get; set; } = new List<Tag>();
        public List<CollectionElementEdit> CollectionElements { get; set; } = new List<CollectionElementEdit>();

        public List<TopicFragmentLink> FragmentsUsed { get; set; } = new List<TopicFragmentLink>();
    }
}
