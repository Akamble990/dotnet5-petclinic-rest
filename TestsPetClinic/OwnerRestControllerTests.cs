
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using PetClinic.Api.Controllers;
using PetClinic.Application.Common.Mappings;
using PetClinic.Application.Dtos;
using PetClinic.Application.Implementation;
using PetClinic.Application.Interfaces;
using PetClinic.Domain.Common.Interfaces;
using PetClinic.Domain.Entities;
using PetClinic.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PetClinicTests1
{
    [TestFixture]
    public class OwnerRestControllerTests
    {
        private Mock<IAddPetService> _ownerServiceObj;
        private Mock<IUnitOfWork> _unityObj;
        private Mock<OwnerRestController> _controllerObj;
        private OwnerRestController _controller;

        [SetUp]
        public void SetUp()
        {
            _ownerServiceObj = new Mock<IAddPetService>();
            _unityObj = new Mock<IUnitOfWork>();
            _controllerObj = new Mock<OwnerRestController>();
            _controller = new OwnerRestController(_ownerServiceObj.Object, _unityObj.Object);
        }

        [Test]
        [Category("getOwner")]
        public void getOwner_WithOwnerID_ReturnNull()
        {
            _controllerObj.Setup(x => x.getOwner(1, default)).ReturnsAsync(default(OwnerDTO));

            var result = _controller.getOwner(1, default);
            result.Wait();
            var ActualResult = result.Result;
            var Result = ActualResult.Value;

            Assert.AreEqual(null, Result);
        }



        [Test]
        [Category("getOwners")]
        public void getOwners_WhenCalled_ReturnNotNull()
        {
            var result = _controller.getOwners(default);

            Assert.IsNotNull(result);
        }

        [Test]
        [Category("getOwners")]
        public void getOwners_WhenCalled_GetTheOwnersFromDb()
        {

            var result = _controller.getOwners(default);

            _ownerServiceObj.Verify(s => s.GetOwners());
        }
        [Test]
        [Category("addOwner")]
        public void addOwner_addOwner_ReturnCreatedResultStatus()
        {
            _controllerObj.Setup(x => x.addOwner(new OwnerCreateDTO() { FirstName = "Amit", LastName = "Kamble", Address = "ABC", City = "Pune", Telephone = "9921913583" }, default)).ReturnsAsync(new CreatedResult(String.Empty, null));

            var result = _controller.addOwner(new OwnerCreateDTO() { FirstName = "Amit", LastName = "Kamble", Address = "ABC", City = "Pune", Telephone = "9921913583" }, default);
            result.Wait();
            var ActualResult = result.Result;
            var Result = ActualResult.GetType();

            Assert.AreEqual(typeof(CreatedResult), Result);
        }

        [Test]
        [Category("addOwner")]
        public void addOwner_addOwner_CreatedTheOwnerToDb()
        {
            var result = _controller.addOwner(new OwnerCreateDTO() { FirstName = "Amit", LastName = "Kamble", Address = "ABC", City = "Pune", Telephone = "9921913583" }, default);

            _ownerServiceObj.Verify(s => s.AddOwner(It.IsAny<OwnerCreateDTO>()));
        }

        [Test]
        [Category("updateOwner")]
        public void updateOwner_UpdateOwner_ReturnNocontent()
        {
            _controllerObj.Setup(x => x.updateOwner(1, new OwnerUpdateDTO() { FirstName = "Amit", LastName = "Kamble", Address = "ABC", City = "Pune", Telephone = "9921913583" }, default)).ReturnsAsync(new NoContentResult());

            var result = _controller.updateOwner(1, new OwnerUpdateDTO() { FirstName = "Amit", LastName = "Kamble", Address = "ABC", City = "Pune", Telephone = "9921913583" }, default);
            result.Wait();
            var ActualResult = result.Result;
            var Result = ActualResult.GetType();

            Assert.AreEqual(typeof(NoContentResult), Result);
        }

        [Test]
        [Category("updateOwner")]
        public void updateOwner_UpdateOwner_UpdateTheOwnerFromDb()
        {

            var result = _controller.updateOwner(1, new OwnerUpdateDTO() { FirstName = "Amit", LastName = "Kamble", Address = "ABC", City = "Pune", Telephone = "9921913583" }, default);

            _ownerServiceObj.Verify(s => s.UpdateOwner(1, It.IsAny<OwnerUpdateDTO>()), Times.Once);

        }

        [Test]
        [Category("deleteOwner")]
        public void deleteOwner_WhenCalled_ReturnOkStatus()
        {
            _controllerObj.Setup(x => x.deleteOwner(1, default)).ReturnsAsync(new OkResult());

            var result = _controller.deleteOwner(1, default);
            result.Wait();
            var ActualResult = result.Result;
            var Result = ActualResult.GetType();

            Assert.AreEqual(typeof(OkResult), Result);

        }

        [Test]
        [Category("deleteOwner")]
        public void deleteOwner_WhenCalled_DeleteTheOwnerFromDb()
        {
            var result = _controller.deleteOwner(1, default);

            _ownerServiceObj.Verify(s => s.DeleteOwner(1));
        }

        [Test]
        [Category("getOwnersList")]
        public void getOwnersList_WithOwnerLastName_ReturnNotNull()
        {

            var result = _controller.getOwnersList("Amit", default);

            Assert.IsNotNull(result);

        }

        //[Test]
        //[Category("getOwnersList")]
        //public void getOwnersList_WithOwnerId_ReturnOkStatus()
        //{

        //    _controllerObj.Setup(x => x.getOwnersList("Amit", default)).ReturnsAsync(new OkObjectResult(default));  

        //    var result = _controller.getOwnersList("Amit", default);
        //    result.Wait();
        //    var ActualResult = result.Result;
        //    var Result = ActualResult.GetType();

        //    Assert.AreEqual(typeof(OkObjectResult), Result);

        //}

        [Test]
        [Category("getOwnersList")]
        public void getOwnersList_WhenCalled_GetOwnerFromDb()
        {
            var result = _controller.getOwnersList("Amit", default);

            _ownerServiceObj.Verify(s => s.GetOwnersList("Amit"));
        }

    }
}