
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
    public class SpecialtyRestControllerTests
    {
        private Mock<ISpecialtyService> _specialtyServiceObj;
        private Mock<IUnitOfWork> _unityOfWorkObj;
        private Mock<SpecialtyRestController> _specialtyRestControllerObj;
        private SpecialtyRestController _specialtyRestController;

        [SetUp]
        public void SetUp()
        {
            _specialtyServiceObj = new Mock<ISpecialtyService>();
            _unityOfWorkObj = new Mock<IUnitOfWork>();
            _specialtyRestControllerObj = new Mock<SpecialtyRestController>();
            _specialtyRestController = new SpecialtyRestController(_specialtyServiceObj.Object, _unityOfWorkObj.Object);
        }


        [Test]
        [Category("getAllSpecialties")]
        public void getAllSpecialties_WhenCalled_ReturnNotNull()
        {
            var result = _specialtyRestController.getAllSpecialties(default);

            Assert.IsNotNull(result);
        }

        [Test]
        [Category("getAllSpecialties")]
        public void getAllSpecialties_WhenCalled_GetTheAllSpecialtyFromDb()
        {
            var result = _specialtyRestController.getAllSpecialties(default);
            _specialtyServiceObj.Verify(s => s.GetAllSpecialties());
        }

        [Test]
        [Category("getSpecialty")]
        public void getSpecialty_WhenCalled_ReturnNotNull()
        {
            var result = _specialtyRestController.getSpecialty(1, default);

            Assert.IsNotNull(result);
        }
        [Test]
        [Category("getSpecialty")]
        public void getSpecialty_WhenCalled_GetTheSpecialtyFromDb()
        {
            var result = _specialtyRestController.getSpecialty(1, default);
            _specialtyServiceObj.Verify(s => s.GetSpecialty(1));
        }

        //[Test]
        //[Category("addSpecialty")]
        //public void addSpecialty_addVet_ReturnCreatedResultStatus()
        //{
        //    _specialtyRestControllerObj.Setup(x => x.addSpecialty(new SpecialtyDTO() { Id = 1, Name = "AMIT" }, default)).ReturnsAsync(new CreatedResult(String.Empty, null));

        //    var result = _specialtyRestController.addSpecialty(new SpecialtyDTO() { Id = 1, Name = "AMIT" }, default);
        //    result.Wait();
        //    var ActualResult = result.Result;
        //    var Result = ActualResult.GetType();

        //    Assert.AreEqual(typeof(CreatedResult), Result);
        //}

        [Test]
        [Category("addSpecialty")]
        public void addSpecialty_addSpecialty_AddSpecialtyToDb()
        {
            var result = _specialtyRestController.addSpecialty(new SpecialtyCreateDTO() { Id = 1, Name = "AMIT" }, default);
            _specialtyServiceObj.Verify(s => s.AddSpecialty(It.IsAny<SpecialtyCreateDTO>()), Times.Once);
        }

        [Test]
        [Category("updateSpecialty")]
        public void updateSpecialty_UpdateSpecialty_ReturnNocontent()
        {
            _specialtyRestControllerObj.Setup(x => x.updateSpecialty(1, new SpecialtyCreateDTO() { Id = 1, Name = "amit" }, default)).ReturnsAsync(new NoContentResult());

            var result = _specialtyRestController.updateSpecialty(1, new SpecialtyCreateDTO() { Id = 1, Name = "amit" }, default);
            result.Wait();
            var ActualResult = result.Result;
            var Result = ActualResult.GetType();

            Assert.AreEqual(typeof(NoContentResult), Result);
        }

        [Test]
        [Category("updateSpecialty")]
        public void updateSpecialty_updateSpecialty_UpdateTheSpecialtyToDb()
        {
            var result = _specialtyRestController.updateSpecialty(1, new SpecialtyCreateDTO() { Id = 1, Name = "amit" }, default);

            _specialtyServiceObj.Verify(s => s.UpdateSpecialty(1, It.IsAny<SpecialtyCreateDTO>()), Times.Once);

        }

        [Test]
        [Category("deleteSpecialty")]
        public void deleteSpecialty_WhenCalled_ReturnOkStatus()
        {
            _specialtyRestControllerObj.Setup(x => x.deleteSpecialty(1, default)).ReturnsAsync(new OkResult());

            var result = _specialtyRestController.deleteSpecialty(1, default);
            result.Wait();
            var ActualResult = result.Result;
            var Result = ActualResult.GetType();

            Assert.AreEqual(typeof(OkResult), Result);

        }

        [Test]
        [Category("deleteSpecialty")]
        public void deleteSpecialty_WhenCalled_DeleteTheSpecialtyFromDb()
        {
            var result = _specialtyRestController.deleteSpecialty(1, default);

            _specialtyServiceObj.Verify(s => s.DeleteSpecialty(1));
        }

    }
}



