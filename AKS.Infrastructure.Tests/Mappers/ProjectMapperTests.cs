using System;
using AKS.Infrastructure;
using AKS.Infrastructure.Data;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace AKS.AppCore.Tests
{
    [TestFixture]
    public class ProjectMapperTests
    {
        Entities.Project _entity;
        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            var customerId = Guid.NewGuid();
            _entity = new Entities.Project {
                ProjectId = Guid.NewGuid(),
                CustomerId = customerId,
                Name = "Test Name",
                LogoFileName = "LogoFile",
                Customer = new Entities.Customer { CustomerId = customerId, Name = "Test Customer Name", LogoFileName = "Customer Logo File" }
            };
        }

        [Test]
        public void ShouldMapProjectList()
        {
            var model = Mapper.Map<Common.Models.ProjectList>(_entity);

            Assert.AreEqual(_entity.ProjectId, model.ProjectId);
            Assert.AreEqual(_entity.Name, model.Name);
        }

        [Test]
        public void ShouldMapProjectHeaderNav()
        {
            var model = Mapper.Map<Common.Models.HeaderNavView>(_entity);

            Assert.AreEqual(_entity.ProjectId, model.ProjectId);
            Assert.AreEqual(_entity.Name, model.ProjectName);
            Assert.AreEqual(_entity.LogoFileName, model.ProjectLogo);
            Assert.AreEqual(_entity.CustomerId, model.CustomerId);
            Assert.AreEqual(_entity.Customer.Name, model.CustomerName);
            Assert.AreEqual(_entity.Customer.LogoFileName, model.CustomerLogo);
        }
    }
}
