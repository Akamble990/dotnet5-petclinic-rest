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

namespace PetTypeRestControllerTests
{
    [TestFixture]
    public class PetTypeRestControllerTest
    {
        private Mock<IPetTypeService> _petTypeServiceObj;
        private Mock<IUnitOfWork> _unityOfWorkObj;
        private Mock<PetTypeRestController> _petTypeRestControllerObj;
        private PetTypeRestController _petTypeRestController;

        [SetUp]
        public void SetUp()
        {
            _petTypeServiceObj = new Mock<IPetTypeService>();
            _unityOfWorkObj = new Mock<IUnitOfWork>();
            _petTypeRestControllerObj = new Mock<PetTypeRestController>();
            _petTypeRestController = new PetTypeRestController(_petTypeServiceObj.Object, _unityOfWorkObj.Object);
        }


        [Test]
        [Category("getAllPetTypes")]
        public void getAllPetTypes_WhenCalled_ReturnNotNull()
        {
            var result = _petTypeRestController.getAllPetTypes(default);

            Assert.IsNotNull(result);
        }

        [Test]
        [Category("getAllPetTypes")]
        public void getAllPetTypes_WhenCalled_GetThePetTypeFromDb()
        {

            var result = _petTypeRestController.getAllPetTypes(default);

            _petTypeServiceObj.Verify(s => s.GetAllPetTypes());
        }

        [Test]
        [Category("getPetType")]
        public void getPetType_WhenCalled_ReturnNotNull()
        {

            var result = _petTypeRestController.getPetType(1, default);

            Assert.IsNotNull(result);
        }

        [Test]
        [Category("getPetType")]
        public void getPetType_WhenCalled_GetThePetTypeFromDb()
        {

            var result = _petTypeRestController.getPetType(1, default);

            _petTypeServiceObj.Verify(s => s.GetPetType(1));
        }

        //[Test]
        //[Category("addPetType")]
        //public void addPetType_addPet_ReturnCreatedResultStatus()
        //{
        //    _petTypeRestControllerObj.Setup(x => x.addPetType(new PetTypeDTO() {Id=1, Name = "Tomy"}, default)).ReturnsAsync(new CreatedResult(String.Empty, default(int)));

        //    var result = _petTypeRestController.addPetType(new PetTypeDTO() { Id = 1, Name = "Tomy" }, default);
        //    result.Wait();
        //    var ActualResult = result.Result;
        //    var Result = ActualResult.GetType();

        //    Assert.AreEqual(typeof(CreatedResult), Result);
        //}

        //[Test]
        //[Category("addPetType")]
        //public void addPetType_addPetType_AddPetTypeToDb()
        //{
        //    _petTypeRestControllerObj.Setup(x => x.addPetType(new PetTypeDTO() { Id = 1, Name = "Tomy" }, default));

        //    _petTypeServiceObj.Verify(s => s.AddPetType(It.IsAny<PetTypeDTO>()), Times.Once);
        //}

        [Test]
        [Category("updatePetType")]
        public void updatePetType_UpdatePetType_ReturnNocontent()
        {
            _petTypeRestControllerObj.Setup(x => x.updatePetType(1, new PetTypeDTO() { Id = 1, Name = "Tomy" }, default)).ReturnsAsync(new NoContentResult());

            var result = _petTypeRestController.updatePetType(1, new PetTypeDTO() { Id = 1, Name = "Tomy" }, default);
            result.Wait();
            var ActualResult = result.Result;
            var Result = ActualResult.GetType();

            Assert.AreEqual(typeof(NoContentResult), Result);
        }

        [Test]
        [Category("updatePetType")]
        public void updatePetType_UpdatePetType_UpdateThePetTypeToDb()
        {

            var result = _petTypeRestController.updatePetType(1, new PetTypeDTO() { Id = 1, Name = "Tomy" }, default);

            _petTypeServiceObj.Verify(s => s.UpdatePetType(1, It.IsAny<PetTypeDTO>()), Times.Once);

        }

        [Test]
        [Category("deletePetType")]
        public void deletePetType_WhenCalled_ReturnOkStatus()
        {
            _petTypeRestControllerObj.Setup(x => x.deletePetType(1, default)).ReturnsAsync(new OkResult());

            var result = _petTypeRestController.deletePetType(1, default);
            result.Wait();
            var ActualResult = result.Result;
            var Result = ActualResult.GetType();

            Assert.AreEqual(typeof(OkResult), Result);

        }

        [Test]
        [Category("deletePetType")]
        public void deletePetType_WhenCalled_DeleteThePetTypeFromDb()
        {
            var result = _petTypeRestController.deletePetType(1, default);

            _petTypeServiceObj.Verify(s => s.DeletePetType(1));
        }

    }
}

