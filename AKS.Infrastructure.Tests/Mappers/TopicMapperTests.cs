using System;
using AKS.Infrastructure;
using AKS.Infrastructure.Data;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace AKS.AppCore.Tests
{
    [TestFixture]
    public class TopicMapperTests
    {
        Entities.Topic _topicEntity;
        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _topicEntity = new Entities.Topic {
                ProjectId = Guid.NewGuid(),
                TopicId = Guid.NewGuid(),
                TopicTypeId = 2,
                Title = "Test Title",
                Description = "Test description",
                Content = "Test content",
            };
            _topicEntity.AddTag(Guid.NewGuid(), "Tag1");
        }

        [Test]
        public void ShouldMapTopicLink()
        {
            var topicModel = Mapper.Map<Common.Models.TopicLink>(_topicEntity);

            Assert.AreEqual(_topicEntity.ProjectId, topicModel.ProjectId);
            Assert.AreEqual(_topicEntity.TopicId, topicModel.TopicId);
            Assert.AreEqual(_topicEntity.Title, topicModel.Title);
            Assert.AreEqual(_topicEntity.Description, topicModel.Description);
        }

        [Test]
        public void ShouldMapTopicList()
        {
            var topicModel = Mapper.Map<Common.Models.TopicList>(_topicEntity);

            Assert.AreEqual(_topicEntity.ProjectId, topicModel.ProjectId);
            Assert.AreEqual(_topicEntity.TopicId, topicModel.TopicId);
            Assert.AreEqual(_topicEntity.Title, topicModel.Title);
            Assert.AreEqual(_topicEntity.Description, topicModel.Description);
            Assert.AreEqual(_topicEntity.TopicTypeId, topicModel.TopicTypeId);
        }

        [Test]
        public void ShouldMapTopicView()
        {
            var topicModel = Mapper.Map<Entities.Topic, Common.Models.TopicView>(_topicEntity);

            Assert.AreEqual(_topicEntity.ProjectId, topicModel.ProjectId);
            Assert.AreEqual(_topicEntity.TopicId, topicModel.TopicId);
            Assert.AreEqual(_topicEntity.Title, topicModel.Title);
            Assert.AreEqual(_topicEntity.Description, topicModel.Description);
            Assert.AreEqual(_topicEntity.DocumentName, topicModel.DocumentName);
            Assert.AreEqual(_topicEntity.TopicTypeId, topicModel.TopicTypeId);

            Assert.AreEqual(_topicEntity.Tags.Count, topicModel.Tags.Count);
        }
    }
}
