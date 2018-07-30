using Resurgam.AppCore.Entities;
using Resurgam.AppCore.Specifications;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Resurgam.AppCore.Tests.Specifications
{
    [TestClass]
    public class TopicDisplaySpecificationTest
    {


        [TestMethod]
        public void ShouldFilterByTopicAndProject()
        {
            //Test Data
            var projectId = 123;
            var topicId = 1234;

            var topics = new List<Topic>
            {
                new Topic {ProjectId = 123, Id = 1234},
                new Topic {ProjectId = 12, Id = 1234},
                new Topic {ProjectId = 123, Id = 123}
            };

            var spec = new TopicDisplaySpecification(projectId, topicId);

            var filteredTopics = topics.Where(spec.CompiledCriteria);

            Assert.AreEqual(1, filteredTopics.Count());
            var topic = filteredTopics.First();
            Assert.AreEqual(projectId, topic.ProjectId);
            Assert.AreEqual(topicId, topic.Id);

        }

        [TestMethod]
        public void ShouldIncludeTags()
        {
            //Test Data
            var projectId = 123;
            var topicId = 1234;

            var expectedTopic = new Topic { ProjectId = 123, Id = 1234 };
            expectedTopic.AddTag(1, "Tag1");
            var topics = new List<Topic>
            {
                expectedTopic,
                new Topic {ProjectId = 12, Id = 1234},
                new Topic {ProjectId = 123, Id = 123}
            };

            var spec = new TopicDisplaySpecification(projectId, topicId);

            var filteredTopics = topics.Where(spec.CompiledCriteria);
            var topic = filteredTopics.First();

            Assert.IsNotNull(topic.Tags);
            Assert.AreEqual(1, topic.Tags.Count());
        }
    }
}
