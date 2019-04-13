using System;
using Mods = AKS.Common.Models;
using AKS.Infrastructure;
using AKS.Infrastructure.Data;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Collections.Generic;
using Ents = AKS.AppCore.Entities;

namespace AKS.Infrastructure.Tests.Mappers
{
    [TestFixture]
    public class TagMapperTests
    {
        Ents.Tag _entity;
        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _entity = new Ents.Tag {
                ProjectId = Guid.NewGuid(),
                TagId = Guid.NewGuid(),
                Name = "Test Name",
            };
        }

        [Test]
        public void ShouldMapTag()
        {
            var model = Mapper.Map<Mods.Tag>(_entity);

            Assert.AreEqual(_entity.ProjectId, model.ProjectId);
            Assert.AreEqual(_entity.TagId, model.TagId);
            Assert.AreEqual(_entity.Name, model.Name);
        }
    }
}
