using Moq;
using NUnit.Framework;
using System;

namespace PetClinicTests
{
    [TestFixture]
    public class OwnerRestControllerTests
    {

        [Test]
        public void GetOwner_WithOwnerId_ReturnNotNull()
        {
            var ownerObj = new Mock<IOwnerRepository>();
            var mapper = new Mock<IMapper>();

            var ownerService = new OwnerService(ownerObj.Object, mapper.Object);

            var result = ownerService.GetOwner(1);

            Assert.IsNotNull(result);

        }

        [Test]
        public void GetOwner_WithOwnerId_ReturnNullObject()
        {
            var ownerObj = new Mock<IOwnerRepository>();
            var mapper = new Mock<IMapper>();
            var owner = new Mock<IOwner>();
            var ownerServiceObj = new Mock<IOwnerService>();
            ownerServiceObj.Setup(x => x.GetOwner(1)).ReturnsAsync(new OwnerDTO() { FirstName = "Amit", LastName = "Kamble", Address = "ABC", City = "Pune", Telephone = "9921913583" });
            ownerObj.Setup(x => x.FindByIdAsync(1, default)).ReturnsAsync(new Owner() { FirstName = "Amit", LastName = "Kamble", Address = "ABC", City = "Pune", Telephone = "9921913583" });

            var ownerService = new OwnerService(ownerObj.Object, mapper.Object);

            var Result = ownerService.GetOwner(1);
            Result.Wait();
            var actualResult = Result.Result;

            Assert.IsNull(actualResult);

        }

        [Test]
        public void GetOwner_WithOwnerID_ReturnNull()
        {
            var ownerServiceObj = new Mock<IOwnerService>();
            var unityObj = new Mock<IUnitOfWork>();
            var controllerObj = new Mock<OwnerRestController>();
            controllerObj.Setup(x => x.getOwner(1, default)).ReturnsAsync(default(OwnerDTO));

            var controller = new OwnerRestController(ownerServiceObj.Object, unityObj.Object);
            var result = controller.getOwner(1, default);
            result.Wait();
            var ActualResult = result.Result;
            var Result = ActualResult.Value;

            Assert.AreEqual(null, Result);

        }

        [Test]
        [Category("GetOwners")]
        public void GetOwners_WhenCalled_ReturnOkStatus()
        {
            var ownerServiceObj = new Mock<IOwnerService>();
            var unityObj = new Mock<IUnitOfWork>();
            var controllerObj = new Mock<OwnerRestController>();
            controllerObj.Setup(x => x.getOwners(default)).ReturnsAsync(new OkObjectResult(null));

            var controller = new OwnerRestController(ownerServiceObj.Object, unityObj.Object);
            var result = controller.getOwners(default);
            //result.Wait();
            //var ActualResult = result.Result;
            var Result = result.GetType();
            Assert.AreEqual(typeof(OkObjectResult), Result);

        }

        [Test]
        [Category("GetOwners")]
        public void GetOwners_WhenCalled_ReturnNotNull()
        {
            var ownerServiceObj = new Mock<IOwnerService>();
            var unityObj = new Mock<IUnitOfWork>();
            var controllerObj = new Mock<OwnerRestController>();

            var controller = new OwnerRestController(ownerServiceObj.Object, unityObj.Object);

            var result = controller.getOwners(default);

            Assert.IsNotNull(result);

        }

        [Test]
        [Category("GetOwners")]
        public void GetOwners_WhenCalled_GetTheOwnerFromDb()
        {
            var ownerServiceObj = new Mock<IOwnerService>();
            var unityObj = new Mock<IUnitOfWork>();
            var controllerObj = new Mock<OwnerRestController>();

            var controller = new OwnerRestController(ownerServiceObj.Object, unityObj.Object);
            var result = controller.getOwners(default);

            ownerServiceObj.Verify(s => s.GetOwners());
        }
        [Test]
        [Category("addOwner")]
        public void addOwner_addOwner_ReturnCreatedResultStatus()
        {
            var ownerServiceObj = new Mock<IOwnerService>();
            var unityObj = new Mock<IUnitOfWork>();
            var controllerObj = new Mock<OwnerRestController>();
            controllerObj.Setup(x => x.addOwner(new OwnerCreateDTO() { FirstName = "Amit", LastName = "Kamble", Address = "ABC", City = "Pune", Telephone = "9921913583" }, default)).ReturnsAsync(new CreatedResult(String.Empty, null));

            var controller = new OwnerRestController(ownerServiceObj.Object, unityObj.Object);
            var result = controller.addOwner(new OwnerCreateDTO() { FirstName = "Amit", LastName = "Kamble", Address = "ABC", City = "Pune", Telephone = "9921913583" }, default);
            result.Wait();
            var ActualResult = result.Result;
            var Result = ActualResult.GetType();
            Assert.AreEqual(Result, typeof(CreatedResult));

        }
        [Test]
        [Category("addOwner")]
        public void addOwner_addOwner_CreatedTheOwnerToDb()
        {
            var ownerServiceObj = new Mock<IOwnerService>();
            var unityObj = new Mock<IUnitOfWork>();
            var controllerObj = new Mock<OwnerRestController>();
            var controller = new OwnerRestController(ownerServiceObj.Object, unityObj.Object);
            var result = controller.addOwner(new OwnerCreateDTO() { FirstName = "Amit", LastName = "Kamble", Address = "ABC", City = "Pune", Telephone = "9921913583" }, default);


            ownerServiceObj.Verify(s => s.AddOwner(It.IsAny<OwnerCreateDTO>()));

        }

        [Test]
        [Category("updateOwner")]
        public void updateOwner_UpdateModel_ReturnNocontent()
        {
            var ownerServiceObj = new Mock<IOwnerService>();
            var unityObj = new Mock<IUnitOfWork>();
            var controllerObj = new Mock<OwnerRestController>();
            controllerObj.Setup(x => x.updateOwner(1, new OwnerUpdateDTO() { FirstName = "Amit", LastName = "Kamble", Address = "ABC", City = "Pune", Telephone = "9921913583" }, default)).ReturnsAsync(new NoContentResult());

            var controller = new OwnerRestController(ownerServiceObj.Object, unityObj.Object);
            var result = controller.updateOwner(1, new OwnerUpdateDTO() { FirstName = "Amit", LastName = "Kamble", Address = "ABC", City = "Pune", Telephone = "9921913583" }, default);
            result.Wait();
            var ActualResult = result.Result;
            var Result = ActualResult.GetType();
            Assert.AreEqual(Result, typeof(NoContentResult));

        }

        [Test]
        [Category("updateOwner")]
        public void updateOwner_UpdateModel_UpdateTheOwnerFromDb()
        {
            var ownerServiceObj = new Mock<IOwnerService>();
            var unityObj = new Mock<IUnitOfWork>();
            var controllerObj = new Mock<OwnerRestController>();
            //  controllerObj.Setup(x => x.updateOwner(1, new OwnerUpdateDTO() { FirstName = "Amit", LastName = "Kamble", Address = "ABC", City = "Pune", Telephone = "9921913583" }, default)).ReturnsAsync(new NoContentResult());

            var controller = new OwnerRestController(ownerServiceObj.Object, unityObj.Object);
            var result = controller.updateOwner(1, new OwnerUpdateDTO() { FirstName = "Amit", LastName = "Kamble", Address = "ABC", City = "Pune", Telephone = "9921913583" }, default);
            //result.Wait();
            //var ActualResult = result.Result;
            //var Result = ActualResult.GetType();
            //Assert.AreEqual(Result, typeof(NoContentResult));
            ownerServiceObj.Verify(s => s.UpdateOwner(1, It.IsAny<OwnerUpdateDTO>()), Times.Once);

        }

        [Test]
        [Category("DeleteOwner")]
        public void DeleteOwner_WhenCalled_ReturnOkStatus()
        {
            var ownerServiceObj = new Mock<IOwnerService>();
            var unityObj = new Mock<IUnitOfWork>();
            var controllerObj = new Mock<OwnerRestController>();
            controllerObj.Setup(x => x.deleteOwner(1, default)).ReturnsAsync(new OkResult());

            var controller = new OwnerRestController(ownerServiceObj.Object, unityObj.Object);
            var result = controller.deleteOwner(1, default);
            result.Wait();
            var ActualResult = result.Result;
            var Result = ActualResult.GetType();
            Assert.AreEqual(typeof(OkResult), Result);

        }

        [Test]
        [Category("DeleteOwner")]
        public void DeleteOwner_WhenCalled_DeleteTheOwnerFromDb()
        {
            var ownerServiceObj = new Mock<IOwnerService>();
            var unityObj = new Mock<IUnitOfWork>();
            var controllerObj = new Mock<OwnerRestController>();


            var controller = new OwnerRestController(ownerServiceObj.Object, unityObj.Object);
            var result = controller.deleteOwner(1, default);

            ownerServiceObj.Verify(s => s.DeleteOwner(1));
        }

        [Test]
        [Category("GetOwnerList")]
        public void GetOwnerList_WithOwnerLastName_ReturnNotNull()
        {
            var ownerServiceObj = new Mock<IOwnerService>();
            var unityObj = new Mock<IUnitOfWork>();
            var controllerObj = new Mock<OwnerRestController>();
            var controller = new OwnerRestController(ownerServiceObj.Object, unityObj.Object);

            var result = controller.getOwnersList("Amit", default);

            Assert.IsNotNull(result);

        }

        [Test]
        [Category("GetOwnerList")]
        public void GetOwnerList_WithOwnerId_ReturnOkStatus()
        {
            var ownerServiceObj = new Mock<IOwnerService>();
            var unityObj = new Mock<IUnitOfWork>();
            var controllerObj = new Mock<OwnerRestController>();
            controllerObj.Setup(x => x.getOwnersList("Amit", default)).ReturnsAsync(new OkObjectResult(" "));

            var controller = new OwnerRestController(ownerServiceObj.Object, unityObj.Object);
            var result = controller.getOwnersList("Amit", default);
            result.Wait();
            var ActualResult = result.Result;
            var Result = ActualResult.GetType();
            Assert.AreEqual(typeof(OkObjectResult), Result);

        }
        [Test]

        [Category("GetOwnerList")]
        public void getOwnerList_WhenCalled_GetOwnerFromDb()
        {
            var ownerServiceObj = new Mock<IOwnerService>();
            var unityObj = new Mock<IUnitOfWork>();
            var controllerObj = new Mock<OwnerRestController>();

            var controller = new OwnerRestController(ownerServiceObj.Object, unityObj.Object);
            var result = controller.getOwnersList("Amit", default);

            ownerServiceObj.Verify(s => s.GetOwnersList("Amit"));
        }



    }
}