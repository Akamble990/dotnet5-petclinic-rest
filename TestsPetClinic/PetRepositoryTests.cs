using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using PetClinic.Domain.Common.Interfaces;
using PetClinic.Domain.Entities;
using PetClinic.Domain.Repositories;
using PetClinic.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TestsPetClinic
{
    public class PetRepositoryTest
    {
        public void Setup()
        {

        }

        [Test]
        public void FindPetByID()
        {

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "EmployeeDataBase")
            .Options;

            var dbcontext = new Mock<ApplicationDbContext>(options);
            dbcontext.Setup(x => x.FindAsync(typeof(int), default)).ReturnsAsync(new Pet() {  });
            var petRepo = new PetRepositoryTests();
            var task = petRepo.FindByIdAsync(10);
            task.Wait();
            var result = task.Result;

            Assert.IsNotNull(result);
        }

        [Test]
        public void FindAllPetByIDs_ReturnNotNull()
        {
            var petRepo = new PetRepositoryTests();
            var task = petRepo.FindByIdsAsync(new int[] { 10, 20 }, default);
            task.Wait();
            var result = task.Result;

            Assert.IsNotNull(result);
           
           
        }

        [Test]
        public void FindAllPetByIDs_ReturnCountGreatorthanZero()
        {
            var petRepo = new PetRepositoryTests();
            var task = petRepo.FindByIdsAsync(new int[] { 10, 20 }, default);
            task.Wait();
            var result = task.Result;

            
            Assert.That(result.Count > 0);

        }
        //[Test]
        //public void AddPet_ReturnShouldSuccess()
        //{

        //    var options = new DbContextOptionsBuilder<ApplicationDbContext>()
        //    .UseInMemoryDatabase(databaseName: "EmployeeDataBase")
        //    .Options;

        //    var dbcontext = new Mock<ApplicationDbContext>(options);
        //    var mockPetRepo = new Mock<PetRepositoryTests>();
        //    mockPetRepo.SetupAdd(x => x.Add(new Pet() {  })).Verifiable();
        //    var petRepo = new PetRepositoryTests();
        //    petRepo.Add(new Pet() { });
        //    mockPetRepo.Verify(x => x.Add(It.IsAny<Pet>()), Times.Once);
        //}

        [Test]
        public void FindAllPetAsynch_ReturnNotNull()
        {
            var petRepo = new PetRepositoryTests();
            var task = petRepo.FindAllAsync();
            task.Wait();
            var result = task.Result;

            Assert.IsNotNull(result);
          
            
        }

        [Test]
        public void FindAllPetAsynch_ReturnCountGreatorThanZero()
        {
            var petRepo = new PetRepositoryTests();
            var task = petRepo.FindAllAsync();
            task.Wait();
            var result = task.Result;

           
            Assert.That(result.Count > 0);

        }


      
    }
    public class PetRepositoryTests : IPetRepository
    {
        public IUnitOfWork UnitOfWork => throw new NotImplementedException();

        public void Add(IPet entity)
        {
           
        }

        public Task<bool> AnyAsync(Expression<Func<Pet, bool>> filterExpression, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<int> CountAsync(Expression<Func<Pet, bool>> filterExpression, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<List<IPet>> FindAllAsync(CancellationToken cancellationToken = default)
        {
            List<IPet> pet = new List<IPet>()
            {
                new Pet(){},
                new Pet(){ }
            };

            return Task.FromResult(pet);
        }

        public Task<List<IPet>> FindAllAsync(Expression<Func<Pet, bool>> filterExpression, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<List<IPet>> FindAllAsync(Expression<Func<Pet, bool>> filterExpression, Func<IQueryable<Pet>, IQueryable<Pet>> linq, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IPagedResult<IPet>> FindAllAsync(int pageNo, int pageSize, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IPagedResult<IPet>> FindAllAsync(Expression<Func<Pet, bool>> filterExpression, int pageNo, int pageSize, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IPagedResult<IPet>> FindAllAsync(Expression<Func<Pet, bool>> filterExpression, int pageIndex, int pageSize, Func<IQueryable<Pet>, IQueryable<Pet>> linq, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IPet> FindAsync(Expression<Func<Pet, bool>> filterExpression, CancellationToken cancellationToken = default)
        {
            List<IPet> pet = new List<IPet>()
            {
                new Pet(){ },
                new Pet(){ }
            };

            IPet p = new Pet() {  };

            if (filterExpression.Compile().Invoke((Pet)pet.FirstOrDefault()))
            {
                return Task.FromResult(p);
            }
            else
            {
                return null;
            }
        }

        public Task<IPet> FindByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            IPet pet = new Pet() {  };
            if (id > 0)
            {
                return Task.FromResult(pet);
            }
            else
            {
                return null;
            }
        }

        public Task<List<IPet>> FindByIdsAsync(int[] ids, CancellationToken cancellationToken = default)
        {
            List<IPet> pet = new List<IPet>()
            {
                new Pet(){ },
                new Pet(){}
            };

            if (ids != null && ids.Length > 0)
            {
                return Task.FromResult(pet);
            }
            else
            {
                return null;
            }
        }

        public void Remove(IPet entity)
        {
            throw new NotImplementedException();
        }
    }
}
