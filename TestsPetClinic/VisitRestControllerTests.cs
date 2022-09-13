
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
    public class VisitRestControllerTests
    {
        private Mock<IVisitService> _visitServiceObj;
        private Mock<IUnitOfWork> _unityOfWorkObj;
        private Mock<VisitRestController> _visitRestControllerObj;
        private VisitRestController _visitRestController;

        [SetUp]
        public void SetUp()
        {
            _visitServiceObj = new Mock<IVisitService>();
            _unityOfWorkObj = new Mock<IUnitOfWork>();
            _visitRestControllerObj = new Mock<VisitRestController>();
            _visitRestController = new VisitRestController(_visitServiceObj.Object, _unityOfWorkObj.Object);
        }


        [Test]
        [Category("getVisit")]
        public void getVisit_WhenCalled_ReturnNotNull()
        {
            var result = _visitRestController.getVisit(1, default);

            Assert.IsNotNull(result);
        }

        [Test]
        [Category("getVisit")]
        public void getVisit_WhenCalled_GetTheVisitDetailsFromDb()
        {
            var result = _visitRestController.getVisit(1, default);
            _visitServiceObj.Verify(s => s.GetVisit(1));
        }

        [Test]
        [Category("addVisit")]
        public void addVisit_addVisit_ReturnCreatedResultStatus()
        {
            _visitRestControllerObj.Setup(x => x.addVisit(new VisitCreateDTO() { PetId = 1, Description = "abc", VisitDate = DateTime.Now }, default)).ReturnsAsync(new CreatedResult(String.Empty, null));

            var result = _visitRestController.addVisit(new VisitCreateDTO() { PetId = 1, Description = "abc", VisitDate = DateTime.Now }, default);
            result.Wait();
            var ActualResult = result.Result;
            var Result = ActualResult.GetType();

            Assert.AreEqual(typeof(CreatedResult), Result);
        }

        [Test]
        [Category("addVisit")]
        public void addVisit_addVisit_AddVisitToDb()
        {
            var result = _visitRestController.addVisit(new VisitCreateDTO() { PetId = 1, Description = "abc", VisitDate = DateTime.Now }, default);

            _visitServiceObj.Verify(s => s.AddVisit(It.IsAny<VisitCreateDTO>()));
        }

        [Test]
        [Category("updateVisit")]
        public void updateVisit_UpdateVisit_ReturnNocontent()
        {
            _visitRestControllerObj.Setup(x => x.updateVisit(1, new VisitUpdateDTO() { Description = "abc", VisitDate = DateTime.Now }, default)).ReturnsAsync(new NoContentResult());

            var result = _visitRestController.updateVisit(1, new VisitUpdateDTO() { Description = "abc", VisitDate = DateTime.Now }, default);
            result.Wait();
            var ActualResult = result.Result;
            var Result = ActualResult.GetType();

            Assert.AreEqual(typeof(NoContentResult), Result);
        }

        [Test]
        [Category("updateVisit")]
        public void updateVisit_UpdateVisit_UpdateTheVisitToDb()
        {
            var result = _visitRestController.updateVisit(1, new VisitUpdateDTO() { Description = "abc", VisitDate = DateTime.Now }, default);

            _visitServiceObj.Verify(s => s.UpdateVisit(1, It.IsAny<VisitUpdateDTO>()), Times.Once);

        }

        [Test]
        [Category("deleteVisit")]
        public void deleteVisit_WhenCalled_ReturnOkStatus()
        {
            _visitRestControllerObj.Setup(x => x.deleteVisit(1, default)).ReturnsAsync(new OkResult());

            var result = _visitRestController.deleteVisit(1, default);
            result.Wait();
            var ActualResult = result.Result;
            var Result = ActualResult.GetType();

            Assert.AreEqual(typeof(OkResult), Result);

        }

        [Test]
        [Category("deleteVisit")]
        public void deleteVisit_WhenCalled_DeleteTheVisitFromDb()
        {
            var result = _visitRestController.deleteVisit(1, default);

            _visitServiceObj.Verify(s => s.DeleteVisit(1));
        }

    }
}

