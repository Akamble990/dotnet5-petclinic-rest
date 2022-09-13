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
    public class PetServiceTests
    {
        [Test]
        public void GetPet_returnNotNull()
        {
            var repo = new Mock<IPetService>();
            var repository = new Mock<IPetRepository>();
            repo.Setup(x => x.GetPet(10)).ReturnsAsync(new PetDTO() { Id = 10 });
            var petService = new PetService();
            var result = petService.GetPet(10);
            Assert.IsNotNull(result);


        }

        [Test]
        public void GetPet_returnPetDTO()
        {
            var repo = new Mock<IPetService>();
            var repository = new Mock<IPetRepository>();
            repo.Setup(x => x.GetPet(10)).ReturnsAsync(new PetDTO() { Id = 10});
            var PetService = new PetService();
            var result = PetService.GetPet(10);

            Assert.AreEqual(typeof(PetDTO), result.Result.GetType());

        }


        [Test]
        public void AddPet_ShouldSuccess()
        {
            var repo = new Mock<IAddPetService>();
            var repository = new Mock<IAddPetRepository>();
            var addpetService = new PetService();
            var result = addpetService.AddPet(new PetCreateDTO { OwnerId = 10, Name = "amit" });

            Assert.That(result.IsCompletedSuccessfully);

        }

        [Test]
        public void Update_ShouldSuccess()
        {
            var repo = new Mock<IAddPetService>();
            var repository = new Mock<IAddPetRepository>();
            var addpetService = new PetService();
            var result = addpetService.UpdatePet(1,new PetUpdateDTO() {PetTypeId=1,Name="XYZ",BirthDate=DateTime.Now});

            Assert.That(result.IsCompletedSuccessfully);

        }

        [Test]
        public void Delete_ShouldSuccess()
        {
            var repo = new Mock<IAddPetService>();
            var repository = new Mock<IAddPetRepository>();
            var addpetService = new PetService();
            var result = addpetService.DeletePet(1);

            Assert.That(result.IsCompletedSuccessfully);

        }

        public class PetService : IPetService
        {
            public Task AddPet(PetCreateDTO dto)
            {
                return Task.CompletedTask;
            }

            public Task DeletePet(int petId)
            {
                return Task.CompletedTask; ;
            }

            public void Dispose()
            {
                throw new NotImplementedException();
            }

            public Task<PetDTO> GetPet(int petId)
            {
                return Task.FromResult( new PetDTO() {Id = 10 });
            }

            public Task UpdatePet(int petId, PetUpdateDTO dto)
            {
                return Task.CompletedTask; ;
            }
        }
    }
}
