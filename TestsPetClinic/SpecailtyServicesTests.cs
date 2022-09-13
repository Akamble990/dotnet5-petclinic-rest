using Microsoft.AspNetCore.Mvc;
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
    public class SpecailtyServicesTests
    {
        [Test]
        public void GetSpecialtyByID_returnNotNull()
        {
            var repo = new Mock<ISpecialtyService>();
            var repository = new Mock<ISpecialtyRepository>();
            repo.Setup(x => x.GetSpecialty(1)).ReturnsAsync(new SpecialtyCreateDTO() { Id = 1,Name = "amit"});
            var specialtyService = new SpecialtyService();
            var result = specialtyService.GetSpecialty(1);
            Assert.IsNotNull(result);


        }

        [Test]
        public void GetSpecialtyByID_returnSpecialtyDTO()
        {
            var repo = new Mock<ISpecialtyService>();
            var repository = new Mock<ISpecialtyRepository>();
            repo.Setup(x => x.GetSpecialty(10)).ReturnsAsync(new SpecialtyCreateDTO() { Id = 10});
            var specialtyService = new SpecialtyService();
            var result = specialtyService.GetSpecialty(10);

            Assert.AreEqual(typeof(SpecialtyCreateDTO), result.Result.GetType());

        }


        [Test]
        public void GetSpecialtyByID_returnSpecialtyDTOList()
        {
            var repo = new Mock<ISpecialtyService>();
            var repository = new Mock<ISpecialtyRepository>();

            var SpecialtyService = new SpecialtyService();
            var result = SpecialtyService.GetAllSpecialties();

            Assert.AreEqual(typeof(List<SpecialtyCreateDTO>), result.Result.GetType());

        }



        [Test]
        public void AddSpecialty_ShouldSuccess()
        {
            var repo = new Mock<ISpecialtyService>();
            var repository = new Mock<ISpecialtyRepository>();
            var specialtyService = new SpecialtyService();
            var result = specialtyService.AddSpecialty(new SpecialtyCreateDTO() { });

            Assert.That(result.IsCompletedSuccessfully);


        }

        [Test]
        public void UpdateSpecialty_ShouldSuccess()
        {
            var repo = new Mock<ISpecialtyService>();
            var repository = new Mock<ISpecialtyRepository>();
            var updateSpecialtyService = new SpecialtyService();
            var result = updateSpecialtyService.UpdateSpecialty(1, new SpecialtyCreateDTO() { Id = 1, Name = "amit"});

            Assert.That(result.IsCompletedSuccessfully);

        }

        [Test]
        public void DeleteSpecailty_ShouldSuccess()
        {
            var repo = new Mock<ISpecialtyService>();
            var repository = new Mock<ISpecialtyRepository>();
            var SpecialtyService = new SpecialtyService();
            var result = SpecialtyService.DeleteSpecialty(1);

            Assert.That(result.IsCompletedSuccessfully);

        }

    }
    public class SpecialtyService : ISpecialtyService
    {
        public Task<int> AddSpecialty(SpecialtyCreateDTO dto)
        {
            return Task.FromResult(1);
        }

        public Task DeleteSpecialty(int specialtyId)
        {
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task<List<SpecialtyCreateDTO>> GetAllSpecialties()
        {
            List<SpecialtyCreateDTO> list = new List<SpecialtyCreateDTO>()
            {
                new SpecialtyCreateDTO(){Id=1 },
                new SpecialtyCreateDTO(){Id=2 },
                new SpecialtyCreateDTO(){Id=3 }
            };

            return Task.FromResult(list);
        }

        public Task<SpecialtyCreateDTO> GetSpecialty(int specialtyId)
        {
            return Task.FromResult(new SpecialtyCreateDTO() { Id = 10 });
        }

        public Task UpdateSpecialty(int specialtyId, SpecialtyCreateDTO dto)
        {
            return Task.CompletedTask;
        }
    }
}
