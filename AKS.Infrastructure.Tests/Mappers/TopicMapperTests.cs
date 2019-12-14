using System;
using System.Collections.Generic;
using System.Linq;
using AKS.Common;
using AutoMapper;
using NUnit.Framework;
using Ents = AKS.Infrastructure.Entities;
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
                TopicType = Common.Enums.TopicType.Collection,
                Title = "Test Title",
                Description = "Test description",
                Content = "Test content",
            };
            _entity2 = new Ents.Topic
            {
                ProjectId = projectId,
                TopicId = topicId2,
                TopicType = Common.Enums.TopicType.Document,
                Title = "Test Title",
                Description = "Test description",
                Content = "Test content",
            };
            _entity1.TopicTags.Add(new Ents.TopicTag { Tag = new Ents.Tag { ProjectId = projectId, TagId = Guid.NewGuid(), Name = "Tag1" } });
            var entCollectionElement = new Ents.CollectionElement { Topic = _entity1, ProjectId = projectId, CollectionElementId = Guid.NewGuid(), Name = "TestCollectionElement", TopicId = topicId1 };
            _entity1.CollectionElements.Add(entCollectionElement);
            entCollectionElement.CollectionElementTopics.Add(new Ents.CollectionElementTopic { ProjectId = projectId, CollectionElementId = entCollectionElement.CollectionElementId, TopicId = _entity2.TopicId, Order = 1, Topic = _entity2 });

            _model = new Mods.TopicEdit
            {
                ProjectId = Guid.NewGuid(),
                TopicId = topicId1,
                TopicType = Common.Enums.TopicType.Document,
                Title = "Test Title",
                Description = "Test description",
                Content = "Test content",
                DocumentName = "DocName",
            };
            _model.Tags.Add(new Mods.Tag { ProjectId = projectId, TagId = Guid.NewGuid(), Name = "Tag1" });
            var modCollectionElement = new Mods.CollectionElementEdit { ProjectId = projectId, CollectionElementId = Guid.NewGuid(), Name = "TestCollectionElement", TopicId = topicId1 };
            _model.CollectionElements.Add(modCollectionElement);
            modCollectionElement.ElementTopics.Add(new Mods.CollectionElementTopicList { ProjectId = projectId, CollectionElementId = Guid.NewGuid(), TopicId = Guid.NewGuid(), Order = 2, Topic = new Mods.TopicList { TopicId = Guid.NewGuid() } });
        }

        [Test]
        public void ShouldMapToTopicLink()
        {
            var mapper = MapperConfig.GetMapperConfig().CreateMapper();
            var model = mapper.Map<Mods.TopicLink>(_entity1);

            Assert.AreEqual(_entity1.ProjectId, model.ProjectId);
            Assert.AreEqual(_entity1.TopicId, model.TopicId);
            Assert.AreEqual(_entity1.Title, model.Title);
            Assert.AreEqual(_entity1.Description, model.Description);
        }

        [Test]
        public void ShouldMapToTopicList()
        {
            var mapper = MapperConfig.GetMapperConfig().CreateMapper();
            var topicModel = mapper.Map<Mods.TopicList>(_entity1);

            Assert.AreEqual(_entity1.ProjectId, topicModel.ProjectId);
            Assert.AreEqual(_entity1.TopicId, topicModel.TopicId);
            Assert.AreEqual(_entity1.Title, topicModel.Title);
            Assert.AreEqual(_entity1.Description, topicModel.Description);
            Assert.AreEqual(_entity1.TopicType, topicModel.TopicType);
        }

        [Test]
        public void ShouldMapToTopicView()
        {
            var mapper = MapperConfig.GetMapperConfig().CreateMapper();
            var topicModel = mapper.Map<Ents.Topic, Mods.TopicView>(_entity1);

            Assert.AreEqual(_entity1.ProjectId, topicModel.ProjectId);
            Assert.AreEqual(_entity1.TopicId, topicModel.TopicId);
            Assert.AreEqual(_entity1.Title, topicModel.Title);
            Assert.AreEqual(_entity1.Description, topicModel.Description);
            Assert.AreEqual(_entity1.DocumentName, topicModel.DocumentName);
            Assert.AreEqual(_entity1.TopicType, topicModel.TopicType);

            Assert.AreEqual(_entity1.CollectionElements.Count, topicModel.CollectionElements.Count);
            Assert.AreEqual(_entity1.CollectionElements.First().CollectionElementTopics.First().TopicId, topicModel.CollectionElements.First().ElementTopics.First().TopicId);

            Assert.AreEqual(_entity1.TopicTags.Count, topicModel.Tags.Count);
        }

        [Test]
        public void ShouldMapToTopicEdit()
        {
            var mapper = MapperConfig.GetMapperConfig().CreateMapper();
            var topicModel = mapper.Map<Ents.Topic, Mods.TopicEdit>(_entity1);

            Assert.AreEqual(_entity1.ProjectId, topicModel.ProjectId);
            Assert.AreEqual(_entity1.TopicId, topicModel.TopicId);
            Assert.AreEqual(_entity1.Title, topicModel.Title);
            Assert.AreEqual(_entity1.Description, topicModel.Description);
            Assert.AreEqual(_entity1.DocumentName, topicModel.DocumentName);
            Assert.AreEqual(_entity1.TopicType, topicModel.TopicType);

            //We test the actual mapping separately, 
            //here we'll just verify that something comes over
            Assert.AreEqual(1, topicModel.Tags.Count);
            Assert.AreEqual(1, topicModel.CollectionElements.Count);
            Assert.AreEqual(1, topicModel.CollectionElements[0].ElementTopics.Count);
        }

        [Test]
        public void ShouldMapFromTopicEdit()
        {
            ConfigSettings.BuildApiBaseUrl = "http://foo.com";
            var mapper = MapperConfig.GetMapperConfig().CreateMapper();
            var topicEntity = mapper.Map<Mods.TopicEdit, Ents.Topic>(_model);

            Assert.AreEqual(_model.ProjectId, topicEntity.ProjectId);
            Assert.AreEqual(_model.TopicId, topicEntity.TopicId);
            Assert.AreEqual(_model.Title, topicEntity.Title);
            Assert.AreEqual(_model.Description, topicEntity.Description);
            Assert.AreEqual(_model.DocumentName, topicEntity.DocumentName);
            Assert.AreEqual(_model.TopicType, topicEntity.TopicType);

            //We test the actual mapping separately, 
            //here we'll just verify that something comes over
            Assert.AreEqual(1, topicEntity.TopicTags.Count);
            //Assert.AreEqual(1, topicEntity.CollectionElements.Count);
            //Assert.AreEqual(1, topicEntity.CollectionElements[0].ElementTopics.Count);
        }
    }
}
