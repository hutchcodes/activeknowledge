using System;
using System.Collections.Generic;
using System.Text;

namespace Resurgam.AppCore.Lookups
{
    public class TopicTypes
    {
        public static IReadOnlyList<TopicType> TopicTypesList { get; } = new List<TopicType> { ContentTopic, CollectionTopic, DocumentTopic, FragmentTopic };

        public static TopicType ContentTopic { get; } = new TopicType { Id = 1, Name = "Content" };
        public static TopicType CollectionTopic { get; } = new TopicType { Id = 2, Name = "Collection" };
        public static TopicType DocumentTopic { get; } = new TopicType { Id = 3, Name = "Document" };
        public static TopicType FragmentTopic { get; } = new TopicType { Id = 4, Name = "Fragment" };
    }
}
