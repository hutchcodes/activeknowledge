using System;
using AKS.Infrastructure;
using AKS.Infrastructure.Data;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace AKS.AppCore.Tests
{
    [TestFixture]
    public class CustomerMapperTests
    {
        Entities.Customer _entity;
        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            var customerId = Guid.NewGuid();
            _entity = new Entities.Customer
            {
                CustomerId = customerId,
                Name = "Test Name",
                LogoFileName = "LogoFile",                
            };
        }

        [Test]
        public void ShouldMapCustomerHeaderNav()
        {
            var model = Mapper.Map<Common.Models.HeaderNavView>(_entity);

            Assert.AreEqual(_entity.CustomerId, model.CustomerId);
            Assert.AreEqual(_entity.Name, model.CustomerName);
            Assert.AreEqual(_entity.LogoFileName, model.CustomerLogo);
        }
    }
}
