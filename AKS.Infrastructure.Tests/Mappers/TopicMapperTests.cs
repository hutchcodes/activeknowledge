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
        Entities.Topic _entity;
        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _entity = new Entities.Topic {
                ProjectId = Guid.NewGuid(),
                TopicId = Guid.NewGuid(),
                TopicTypeId = 2,
                Title = "Test Title",
                Description = "Test description",
                Content = "Test content",
            };
            _entity.AddTag(Guid.NewGuid(), "Tag1");
        }

        [Test]
        public void ShouldMapTopicLink()
        {
            var model = Mapper.Map<Common.Models.TopicLink>(_entity);

            Assert.AreEqual(_entity.ProjectId, model.ProjectId);
            Assert.AreEqual(_entity.TopicId, model.TopicId);
            Assert.AreEqual(_entity.Title, model.Title);
            Assert.AreEqual(_entity.Description, model.Description);
        }

        [Test]
        public void ShouldMapTopicList()
        {
            var topicModel = Mapper.Map<Common.Models.TopicList>(_entity);

            Assert.AreEqual(_entity.ProjectId, topicModel.ProjectId);
            Assert.AreEqual(_entity.TopicId, topicModel.TopicId);
            Assert.AreEqual(_entity.Title, topicModel.Title);
            Assert.AreEqual(_entity.Description, topicModel.Description);
            Assert.AreEqual(_entity.TopicTypeId, topicModel.TopicTypeId);
        }

        [Test]
        public void ShouldMapTopicView()
        {
            var topicModel = Mapper.Map<Entities.Topic, Common.Models.TopicView>(_entity);

            Assert.AreEqual(_entity.ProjectId, topicModel.ProjectId);
            Assert.AreEqual(_entity.TopicId, topicModel.TopicId);
            Assert.AreEqual(_entity.Title, topicModel.Title);
            Assert.AreEqual(_entity.Description, topicModel.Description);
            Assert.AreEqual(_entity.DocumentName, topicModel.DocumentName);
            Assert.AreEqual(_entity.TopicTypeId, topicModel.TopicTypeId);

            Assert.AreEqual(_entity.Tags.Count, topicModel.Tags.Count);
        }
    }
}
