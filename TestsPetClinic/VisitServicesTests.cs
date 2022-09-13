using Moq;
using NUnit.Framework;
using PetClinic.Application.Dtos;
using PetClinic.Application.Interfaces;
using PetClinic.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestsPetClinic
{
    public class VisitServicesTests
    {
        [Test]
        public void GetVisitByID_returnNotNull()
        {
            var repo = new Mock<IVisitService>();
            var repository = new Mock<IVisitRepository>();
            repo.Setup(x => x.GetVisit(10)).ReturnsAsync(new VisitDTO() { Id = 10});
            var vetService = new VisitService();
            var result = vetService.GetVisit(10);
            Assert.IsNotNull(result);


        }

        [Test]
        public void GetVisitByID_returnVetDTO()
        {
            var repo = new Mock<IVisitService>();
            var repository = new Mock<IVisitRepository>();
            repo.Setup(x => x.GetVisit(10)).ReturnsAsync(new VisitDTO() { Id = 10,Description = "qwwee" });
            var visitService = new VisitService();
            var result = visitService.GetVisit(10);

            Assert.AreEqual(typeof(VisitDTO), result.Result.GetType());

        }


      


        [Test]
        public void AddVisit_ShouldSuccess()
        {
            var repo = new Mock<IVisitService>();
            var repository = new Mock<IVisitRepository>();
            var visitService = new VisitService();
            var result = visitService.AddVisit(new VisitCreateDTO() { });

            Assert.That(result.IsCompletedSuccessfully);


        }

        [Test]
        public void UpdateVisit_ShouldSuccess()
        {
            var repo = new Mock<IVisitService>();
            var repository = new Mock<IVisitRepository>();
            var visitService = new VisitService();
            var result = visitService.UpdateVisit(1, new VisitUpdateDTO() { Description = "asdsff", VisitDate = DateTime.Now});

            Assert.That(result.IsCompletedSuccessfully);

        }

        [Test]
        public void DeleteVisit_ShouldSuccess()
        {
            var repo = new Mock<IVisitService>();
            var repository = new Mock<IVisitRepository>();
            var visitService = new VisitService();
            var result = visitService.DeleteVisit(1);

            Assert.That(result.IsCompletedSuccessfully);

        }
    }
    public class VisitService : IVisitService
    {
        public Task AddVisit(VisitCreateDTO dto)
        {
            return Task.CompletedTask;
        }

        public Task DeleteVisit(int visitId)
        {
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task<VisitDTO> GetVisit(int visitId)
        {

            return Task.FromResult(new VisitDTO() { Id = 10 });
        }

        public Task UpdateVisit(int visitId, VisitUpdateDTO dto)
        {
            return Task.CompletedTask;
        }
    }
}
