using System;
using System.Linq;
using AKS.Infrastructure;
using AKS.Infrastructure.Data;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace AKS.AppCore.Tests
{
    [TestFixture]
    public class CollectionElementMapperTests
    {
        Entities.CollectionElement _entity;
        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _entity = new Entities.CollectionElement
            {
                ProjectId = Guid.NewGuid(),
                TopicId = Guid.NewGuid(),
                CollectionElementId = Guid.NewGuid(),
                Name = "Test Name",
            };
            _entity.AddTopic(new Entities.Topic { TopicId = Guid.NewGuid(), ProjectId = Guid.NewGuid(), Title = "Test Topic Name", Description = "Test Description" });
        }

        [Test]
        public void ShouldMapCollectionElement()
        {
            var model = Mapper.Map<Common.Models.CollectionElement>(_entity);

            Assert.AreEqual(_entity.ProjectId, model.ProjectId);
            Assert.AreEqual(_entity.CollectionElementId, model.CollectionElementId);
            Assert.AreEqual(_entity.Name, model.Name);
            Assert.AreEqual(_entity.ElementTopics.First().TopicId, model.ElementTopics.First().TopicId);
        }
    }
}
