using System;
using System.Collections.Generic;
using AutoMapper;
using NUnit.Framework;
using Ents = AKS.AppCore.Entities;
using Mods = AKS.Common.Models;

namespace AKS.Infrastructure.Tests.Mappers
{
    [TestFixture]
    public class TopicMapperTests
    {
        Ents.Topic _entity1;
        Ents.Topic _entity2;
        Mods.TopicEdit _model;
        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            var projectId = Guid.NewGuid();
            var topicId1 = Guid.NewGuid();
            var topicId2 = Guid.NewGuid();
            _entity1 = new Ents.Topic
            {
                ProjectId = projectId,
                TopicId = topicId1,
                TopicTypeId = 2,
                Title = "Test Title",
                Description = "Test description",
                Content = "Test content",
            };
            _entity2 = new Ents.Topic
            {
                ProjectId = projectId,
                TopicId = topicId2,
                TopicTypeId = 3,
                Title = "Test Title",
                Description = "Test description",
                Content = "Test content",
            };
            _entity1.Tags.Add(new Ents.Tag { ProjectId = projectId, TagId = Guid.NewGuid(), Name = "Tag1" });
            var entCollectionElement = new Ents.CollectionElement { Topic = _entity1, ProjectId = projectId, CollectionElementId = Guid.NewGuid(), Name = "TestCollectionElement", TopicId = topicId1 };
            _entity1.CollectionElements.Add(entCollectionElement);
            entCollectionElement.ElementTopics.Add(new Ents.CollectionElementTopic { ProjectId = projectId, CollectionElementId = entCollectionElement.CollectionElementId, TopicId = topicId2, Order = 1 });

            _model = new Mods.TopicEdit
            {
                ProjectId = Guid.NewGuid(),
                TopicId = topicId1,
                TopicTypeId = 2,
                Title = "Test Title",
                Description = "Test description",
                Content = "Test content",
                DocumentName = "DocName",
            };
            _model.Tags.Add(new Mods.Tag { ProjectId = projectId, TagId = Guid.NewGuid(), Name = "Tag1" });
            var modCollectionElement = new Mods.CollectionElement { ProjectId = projectId, CollectionElementId = Guid.NewGuid(), Name = "TestCollectionElement", TopicId = topicId1 };
            _model.CollectionElements.Add(modCollectionElement);
            modCollectionElement.ElementTopics.Add(new Mods.CollectionElementTopic { ProjectId = projectId, CollectionElementId = Guid.NewGuid(), TopicId = Guid.NewGuid(), Order = 2, Topic = new Mods.TopicList { TopicId = Guid.NewGuid() } });
        }

        [Test]
        public void ShouldMapToTopicLink()
        {
            var model = Mapper.Map<Mods.TopicLink>(_entity1);

            Assert.AreEqual(_entity1.ProjectId, model.ProjectId);
            Assert.AreEqual(_entity1.TopicId, model.TopicId);
            Assert.AreEqual(_entity1.Title, model.Title);
            Assert.AreEqual(_entity1.Description, model.Description);
        }

        [Test]
        public void ShouldMapToTopicList()
        {
            var topicModel = Mapper.Map<Mods.TopicList>(_entity1);

            Assert.AreEqual(_entity1.ProjectId, topicModel.ProjectId);
            Assert.AreEqual(_entity1.TopicId, topicModel.TopicId);
            Assert.AreEqual(_entity1.Title, topicModel.Title);
            Assert.AreEqual(_entity1.Description, topicModel.Description);
            Assert.AreEqual(_entity1.TopicTypeId, topicModel.TopicTypeId);
        }

        [Test]
        public void ShouldMapToTopicView()
        {
            var topicModel = Mapper.Map<Ents.Topic, Mods.TopicView>(_entity1);

            Assert.AreEqual(_entity1.ProjectId, topicModel.ProjectId);
            Assert.AreEqual(_entity1.TopicId, topicModel.TopicId);
            Assert.AreEqual(_entity1.Title, topicModel.Title);
            Assert.AreEqual(_entity1.Description, topicModel.Description);
            Assert.AreEqual(_entity1.DocumentName, topicModel.DocumentName);
            Assert.AreEqual(_entity1.TopicTypeId, topicModel.TopicTypeId);

            Assert.AreEqual(_entity1.Tags.Count, topicModel.Tags.Count);
        }

        [Test]
        public void ShouldMapToTopicEdit()
        {
            var topicModel = Mapper.Map<Ents.Topic, Mods.TopicEdit>(_entity1);

            Assert.AreEqual(_entity1.ProjectId, topicModel.ProjectId);
            Assert.AreEqual(_entity1.TopicId, topicModel.TopicId);
            Assert.AreEqual(_entity1.Title, topicModel.Title);
            Assert.AreEqual(_entity1.Description, topicModel.Description);
            Assert.AreEqual(_entity1.DocumentName, topicModel.DocumentName);
            Assert.AreEqual(_entity1.TopicTypeId, topicModel.TopicTypeId);

            //We test the actual mapping separately, 
            //here we'll just verify that something comes over
            Assert.AreEqual(1, topicModel.Tags.Count);
            Assert.AreEqual(1, topicModel.CollectionElements.Count);
            Assert.AreEqual(1, topicModel.CollectionElements[0].ElementTopics.Count);
        }

        [Test]
        public void ShouldMapFromTopicEdit()
        {
            var topicEntity = Mapper.Map<Mods.TopicEdit, Ents.Topic>(_model);

            Assert.AreEqual(_model.ProjectId, topicEntity.ProjectId);
            Assert.AreEqual(_model.TopicId, topicEntity.TopicId);
            Assert.AreEqual(_model.Title, topicEntity.Title);
            Assert.AreEqual(_model.Description, topicEntity.Description);
            Assert.AreEqual(_model.DocumentName, topicEntity.DocumentName);
            Assert.AreEqual(_model.TopicTypeId, topicEntity.TopicTypeId);

            //We test the actual mapping separately, 
            //here we'll just verify that something comes over
            Assert.AreEqual(1, topicEntity.Tags.Count);
            //Assert.AreEqual(1, topicEntity.CollectionElements.Count);
            //Assert.AreEqual(1, topicEntity.CollectionElements[0].ElementTopics.Count);
        }
    }
}
