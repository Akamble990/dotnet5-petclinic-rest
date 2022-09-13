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
    public class VetServicesTests
    {
        [Test]
        public void GetVetByID_returnNotNull()
        {
            var repo = new Mock<IVetService>();
            var repository = new Mock<IVetRepository>();
            repo.Setup(x => x.GetVet(10)).ReturnsAsync(new VetDTO() { Id = 10, FirstName = "Amit" });
            var vetService = new VetService();
            var result = vetService.GetVet(10);
            Assert.IsNotNull(result);


        }

        [Test]
        public void GetVetByID_returnVetDTO()
        {
            var repo = new Mock<IVetService>();
            var repository = new Mock<IVetRepository>();
            repo.Setup(x => x.GetVet(10)).ReturnsAsync(new VetDTO() { Id = 10, FirstName = "Amit" });
            var vetService = new VetService();
            var result = vetService.GetVet(10);

            Assert.AreEqual(typeof(VetDTO), result.Result.GetType());

        }


        [Test]
        public void GetVetByID_returnVetDTOList()
        {
            var repo = new Mock<IVetService>();
            var repository = new Mock<IVetRepository>();

            var vetService = new VetService();
            var result = vetService.GetAllVets();

            Assert.AreEqual(typeof(List<VetDTO>), result.Result.GetType());

        }



        [Test]
        public void AddVet_ShouldSuccess()
        {
            var repo = new Mock<IVetService>();
            var repository = new Mock<IVetRepository>();
            var vetService = new VetService();
            var result = vetService.AddVet(new VetCreateDTO() { });

            Assert.That(result.IsCompletedSuccessfully);


        }

        [Test]
        public void UpdateVet_ShouldSuccess()
        {
            var repo = new Mock<IVetService>();
            var repository = new Mock<IVetRepository>();
            var vetService = new VetService();
            var result = vetService.UpdateVet(1, new VetUpdateDTO() { FirstName = "Amit", LastName = "Kamble" });

            Assert.That(result.IsCompletedSuccessfully);

        }

        [Test]
        public void DeleteVet_ShouldSuccess()
        {
            var repo = new Mock<IVetService>();
            var repository = new Mock<IVetRepository>();
            var vetService = new VetService();
            var result = vetService.DeleteVet(1);

            Assert.That(result.IsCompletedSuccessfully);

        }
    }
    public class VetService : IVetService
    {
        public Task AddVet(VetCreateDTO dto)
        {
            return Task.CompletedTask;
        }

        public Task DeleteVet(int vetId)
        {
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task<List<VetDTO>> GetAllVets()
        {
            List<VetDTO> list = new List<VetDTO>()
            {
                new VetDTO(){Id=1 },
                new VetDTO(){Id=2 },
                new VetDTO(){Id=3 }
            };

            return Task.FromResult(list);
        }

        public Task<VetDTO> GetVet(int vetId)
        {

            return Task.FromResult(new VetDTO() { Id = 10 });
        }

        public Task UpdateVet(int vetId, VetUpdateDTO dto)
        {
            return Task.CompletedTask;
        }
    }
}
