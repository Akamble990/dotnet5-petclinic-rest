
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

namespace PetClinicTests
{
    [TestFixture]
    public class PetRestControllerTest
    {
        private Mock<IPetService> _petServiceObj;
        private Mock<IUnitOfWork> _unityOfWorkObj;
        private Mock<PetRestController> _petRestControllerObj;
        private PetRestController _petRestController;

        [SetUp]
        public void SetUp()
        {
            _petServiceObj = new Mock<IPetService>();
            _unityOfWorkObj = new Mock<IUnitOfWork>();
            _petRestControllerObj = new Mock<PetRestController>();
            _petRestController = new PetRestController(_petServiceObj.Object, _unityOfWorkObj.Object);
        }


        [Test]
        [Category("getPet")]
        public void getPet_WhenCalled_ReturnNotNull()
        {
            var result = _petRestController.getPet(1, default);

            Assert.IsNotNull(result);
        }

        [Test]
        [Category("getPet")]
        public void getPet_WhenCalled_GetThePetFromDb()
        {

            var result = _petRestController.getPet(1, default);

            _petServiceObj.Verify(s => s.GetPet(1));
        }

        [Test]
        [Category("addPet")]
        public void addPet_addPet_ReturnCreatedResultStatus()
        {
            _petRestControllerObj.Setup(x => x.addPet(new PetCreateDTO() { Name = "Tomy", OwnerId = 1, PetTypeId = 1, BirthDate = DateTime.Now }, default)).ReturnsAsync(new CreatedResult(String.Empty, null));

            var result = _petRestController.addPet(new PetCreateDTO() { Name = "Tomy", OwnerId = 1, PetTypeId = 1, BirthDate = DateTime.Now }, default);
            result.Wait();
            var ActualResult = result.Result;
            var Result = ActualResult.GetType();

            Assert.AreEqual(typeof(CreatedResult), Result);
        }

        [Test]
        [Category("addPet")]
        public void addPet_addOwner_AddPetToDb()
        {
            var result = _petRestController.addPet(new PetCreateDTO() { Name = "Tomy", OwnerId = 1, PetTypeId = 1, BirthDate = DateTime.Now }, default);

            _petServiceObj.Verify(s => s.AddPet(It.IsAny<PetCreateDTO>()));
        }

        [Test]
        [Category("updatePet")]
        public void updatePet_UpdatePet_ReturnNocontent()
        {
            _petRestControllerObj.Setup(x => x.updatePet(1, new PetUpdateDTO() { Name = "Tomy", PetTypeId = 1, BirthDate = DateTime.Now }, default)).ReturnsAsync(new NoContentResult());

            var result = _petRestController.updatePet(1, new PetUpdateDTO() { Name = "Tomy", PetTypeId = 1, BirthDate = DateTime.Now }, default);
            result.Wait();
            var ActualResult = result.Result;
            var Result = ActualResult.GetType();

            Assert.AreEqual(typeof(NoContentResult), Result);
        }

        [Test]
        [Category("updatePet")]
        public void updatePet_UpdatePet_UpdateThePetToDb()
        {

            var result = _petRestController.updatePet(1, new PetUpdateDTO() { Name = "Tomy", PetTypeId = 1, BirthDate = DateTime.Now }, default);

            _petServiceObj.Verify(s => s.UpdatePet(1, It.IsAny<PetUpdateDTO>()), Times.Once);

        }

        [Test]
        [Category("deletePet")]
        public void deletePet_WhenCalled_ReturnOkStatus()
        {
            _petRestControllerObj.Setup(x => x.deletePet(1, default)).ReturnsAsync(new OkResult());

            var result = _petRestController.deletePet(1, default);
            result.Wait();
            var ActualResult = result.Result;
            var Result = ActualResult.GetType();

            Assert.AreEqual(typeof(OkResult), Result);

        }

        [Test]
        [Category("deletePet")]
        public void deletePet_WhenCalled_DeleteThePetFromDb()
        {
            var result = _petRestController.deletePet(1, default);

            _petServiceObj.Verify(s => s.DeletePet(1));
        }

    }
}
