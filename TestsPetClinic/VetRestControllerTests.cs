
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

namespace VisitRest
{
    [TestFixture]
    public class VetRestControllerTests
    {
        private Mock<IVetService> _vetServiceObj;
        private Mock<IUnitOfWork> _unityOfWorkObj;
        private Mock<VetRestController> _vetRestControllerObj;
        private VetRestController _vetRestController;

        [SetUp]
        public void SetUp()
        {
            _vetServiceObj = new Mock<IVetService>();
            _unityOfWorkObj = new Mock<IUnitOfWork>();
            _vetRestControllerObj = new Mock<VetRestController>();
            _vetRestController = new VetRestController(_vetServiceObj.Object, _unityOfWorkObj.Object);
        }


        [Test]
        [Category("getAllVets")]
        public void getAllVets_WhenCalled_ReturnNotNull()
        {
            var result = _vetRestController.getAllVets(default);

            Assert.IsNotNull(result);
        }

        [Test]
        [Category("getAllVets")]
        public void getAllVets_WhenCalled_GetTheAllVetFromDb()
        {
            var result = _vetRestController.getAllVets(default);
            _vetServiceObj.Verify(s => s.GetAllVets());
        }

        [Test]
        [Category("getVet")]
        public void getVet_WhenCalled_ReturnNotNull()
        {
            var result = _vetRestController.getVet(1, default);

            Assert.IsNotNull(result);
        }
        [Test]
        [Category("getVet")]
        public void getVet_WhenCalled_GetTheVetFromDb()
        {
            var result = _vetRestController.getVet(1, default);
            _vetServiceObj.Verify(s => s.GetVet(1));
        }

        [Test]
        [Category("addVet")]
        public void addVet_addVet_ReturnCreatedResultStatus()
        {
            _vetRestControllerObj.Setup(x => x.addVet(new VetCreateDTO() { FirstName = "amit", LastName = "kamble" }, default)).ReturnsAsync(new CreatedResult(String.Empty, null));

            var result = _vetRestController.addVet(new VetCreateDTO() { FirstName = "amit", LastName = " kamble" }, default);
            result.Wait();
            var ActualResult = result.Result;
            var Result = ActualResult.GetType();

            Assert.AreEqual(typeof(CreatedResult), Result);
        }

        [Test]
        [Category("addVet")]
        public void addVet_addVet_AddVetToDb()
        {
            var result = _vetRestController.addVet(new VetCreateDTO() { FirstName = "amit", LastName = " kamble" }, default);
            _vetServiceObj.Verify(s => s.AddVet(It.IsAny<VetCreateDTO>()), Times.Once);
        }

        [Test]
        [Category("updateVet")]
        public void updateVet_UpdateVet_ReturnNocontent()
        {
            _vetRestControllerObj.Setup(x => x.updateVet(1, new VetUpdateDTO() { FirstName = "amit", LastName = " kamble" }, default)).ReturnsAsync(new NoContentResult());

            var result = _vetRestController.updateVet(1, new VetUpdateDTO() { FirstName = "amit", LastName = " kamble" }, default);
            result.Wait();
            var ActualResult = result.Result;
            var Result = ActualResult.GetType();

            Assert.AreEqual(typeof(NoContentResult), Result);
        }

        [Test]
        [Category("updateVet")]
        public void updateVet_UpdateVet_UpdateTheVetToDb()
        {
            var result = _vetRestController.updateVet(1, new VetUpdateDTO() { FirstName = "amit", LastName = " kamble" }, default);

            _vetServiceObj.Verify(s => s.UpdateVet(1, It.IsAny<VetUpdateDTO>()), Times.Once);

        }

        [Test]
        [Category("deleteVet")]
        public void deleteVet_WhenCalled_ReturnOkStatus()
        {
            _vetRestControllerObj.Setup(x => x.deleteVet(1, default)).ReturnsAsync(new OkResult());

            var result = _vetRestController.deleteVet(1, default);
            result.Wait();
            var ActualResult = result.Result;
            var Result = ActualResult.GetType();

            Assert.AreEqual(typeof(OkResult), Result);

        }

        [Test]
        [Category("deleteVet")]
        public void deleteVet_WhenCalled_DeleteTheVetFromDb()
        {
            var result = _vetRestController.deleteVet(1, default);

            _vetServiceObj.Verify(s => s.DeleteVet(1));
        }

    }
}


