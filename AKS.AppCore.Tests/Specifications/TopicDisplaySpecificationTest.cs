using AKS.AppCore.Entities;
using AKS.AppCore.Specifications;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AKS.AppCore.Tests.Specifications
{
    [TestClass]
    public class TopicDisplaySpecificationTest
    {


        [TestMethod]
        public void ShouldFilterByTopicAndProject()
        {
            //Test Data
            var projectId = new Guid(123, 0, 0, new byte[8]);
            var topicId = new Guid(1234, 0, 0, new byte[8]);

            var topics = new List<Topic>
            {
                new Topic {ProjectId = projectId, TopicId = topicId},
                new Topic {ProjectId = new Guid(12, 0, 0, new byte[8]), TopicId = topicId},
                new Topic {ProjectId = new Guid(13, 0, 0, new byte[8]), TopicId = topicId}
            };

            var spec = new TopicDisplaySpecification(projectId, topicId);

            var filteredTopics = topics.Where(spec.CompiledCriteria);

            Assert.AreEqual(1, filteredTopics.Count());
            var topic = filteredTopics.First();
            Assert.AreEqual(projectId, topic.ProjectId);
            Assert.AreEqual(topicId, topic.TopicId);

        }

        [TestMethod]
        public void ShouldIncludeTags()
        {
            //Test Data
            var projectId = new Guid(123, 0, 0, new byte[8]);
            var topicId = new Guid(1234, 0, 0, new byte[8]);

            var expectedTopic = new Topic { ProjectId = projectId, TopicId = topicId };
            expectedTopic.AddTag(Guid.NewGuid(), "Tag1");
            var topics = new List<Topic>
            {
                expectedTopic,
                new Topic {ProjectId = new Guid(12, 0, 0, new byte[8]), TopicId = topicId},
                new Topic {ProjectId = projectId, TopicId = topicId}
            };

            var spec = new TopicDisplaySpecification(projectId, topicId);

            var filteredTopics = topics.Where(spec.CompiledCriteria);
            var topic = filteredTopics.First();

            Assert.IsNotNull(topic.Tags);
            Assert.AreEqual(1, topic.Tags.Count());
        }
    }
}
