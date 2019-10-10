using System;
using System.Linq;
using AutoMapper;
using NUnit.Framework;
using Ents = AKS.Infrastructure.Entities;
using Mods = AKS.Common.Models;

namespace AKS.Infrastructure.Tests.Mappers
{
    [TestFixture]
    public class CollectionElementMapperTests
    {
        Ents.CollectionElement _entity;
        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _entity = new Ents.CollectionElement
            {
                ProjectId = Guid.NewGuid(),
                TopicId = Guid.NewGuid(),
                CollectionElementId = Guid.NewGuid(),
                Name = "Test Name",
            };
            _entity.CollectionElementTopics.Add(new Ents.CollectionElementTopic { ProjectId = Guid.NewGuid(), CollectionElementId = Guid.NewGuid(), TopicId = Guid.NewGuid(), Order = 2, Topic = new Ents.Topic { TopicId = Guid.NewGuid() } });
        }

        [Test]
        public void ShouldMapCollectionElement()
        {
            var model = Mapper.Map<Mods.CollectionElement>(_entity);

            Assert.AreEqual(_entity.ProjectId, model.ProjectId);
            Assert.AreEqual(_entity.CollectionElementId, model.CollectionElementId);
            Assert.AreEqual(_entity.Name, model.Name);
            Assert.AreEqual(_entity.CollectionElementTopics.First().TopicId, model.ElementTopics.First().TopicId);
        }
    }
}
