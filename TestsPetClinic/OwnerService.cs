using Moq;
using NUnit.Framework;
using PetClinic.Application.Dtos;
using PetClinic.Application.Implementation;
using PetClinic.Application.Interfaces;
using PetClinic.Domain.Entities;
using PetClinic.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TestsPetClinic
{
    [TestFixture]

    public class OwnerTestServiceTests
    {
        [Test]
        public void GetOwnerByID_returnNotNull()
        {
            var repo = new Mock<IAddPetService>();
            var repository = new Mock<IAddPetRepository>();
            repo.Setup(x => x.GetOwner(10)).ReturnsAsync(new OwnerDTO() { Id= 10, FirstName="Amit" }) ;
            var ownerService = new OwnerService(repository.Object);
            var result=ownerService.GetOwner(10);
            Assert.IsNotNull(result);  
            

        }

        [Test]
        public void GetOwnerByID_returnOwnerDTO()
        {
            var repo = new Mock<IAddPetService>();
            var repository = new Mock<IAddPetRepository>();
            repo.Setup(x => x.GetOwner(10)).ReturnsAsync(new OwnerDTO() { Id = 10, FirstName = "Amit" });
            var ownerService = new OwnerService(repository.Object);
            var result = ownerService.GetOwner(10);
            
            Assert.AreEqual(typeof(OwnerDTO), result.Result.GetType());

        }


        [Test]
        public void GetOwnerByID_returnOwnerDTOList()
        {
            var repo = new Mock<IAddPetService>();
            var repository = new Mock<IAddPetRepository>();

            var ownerService = new OwnerService(repository.Object);
            var result = ownerService.GetOwners();

            Assert.AreEqual(typeof(List<OwnerDTO>), result.Result.GetType());

        }



        [Test]
        public void AddOwner_ShouldSuccess()
        {
            var repo = new Mock<IAddPetService>();
            var repository = new Mock<IAddPetRepository>();
            var ownerService = new OwnerService(repository.Object);
            var result = ownerService.AddOwner(new OwnerCreateDTO() { });

            Assert.That(result.IsCompletedSuccessfully);


        }

        [Test]
        public void UpdateOwner_ShouldSuccess()
        {
            var repo = new Mock<IAddPetService>();
            var repository = new Mock<IAddPetRepository>();
            var addOwnerService = new OwnerService();
            var result = addOwnerService.UpdateOwner(1, new OwnerUpdateDTO() { FirstName = "Amit", LastName = "Kamble" });

            Assert.That(result.IsCompletedSuccessfully);

        }

        [Test]
        public void DeleteOwner_ShouldSuccess()
        {
            var repo = new Mock<IAddPetService>();
            var repository = new Mock<IAddPetRepository>();
            var addpetService = new OwnerService();
            var result = addpetService.DeleteOwner(1);

            Assert.That(result.IsCompletedSuccessfully);

        }




    }
    public class OwnerService : IAddPetService
    {
        private readonly IAddPetRepository _ownerRepository;

        public OwnerService()
        {
        }

        public OwnerService(IAddPetRepository ownerRepository)
        {
            _ownerRepository = ownerRepository;
        }
        public Task AddOwner(OwnerCreateDTO dto)
        {
            return Task.CompletedTask;
        }

        public Task DeleteOwner(int ownerId)
        {
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public async Task<OwnerDTO> GetOwner(int ownerId)
        {
           
            return new OwnerDTO() {Id=10,FirstName="Amit" };
        }

        public Task<List<OwnerDTO>> GetOwners()
        {

            List<OwnerDTO> list = new List<OwnerDTO>()
            {
                new OwnerDTO(){Id=1 },
                new OwnerDTO(){Id=2 },
                new OwnerDTO(){Id=3 }
            };

            return Task.FromResult(list);

        }

        public Task<List<OwnerDTO>> GetOwnersList(string lastName)
        {
            List<OwnerDTO> list = new List<OwnerDTO>()
            {
                new OwnerDTO(){Id=1 },
                new OwnerDTO(){Id=2 },
                new OwnerDTO(){Id=3 }
            };

            return Task.FromResult(list);
            
        }

        public Task UpdateOwner(int ownerId, OwnerUpdateDTO dto)
        {
            return Task.CompletedTask;
        }
    }
}


