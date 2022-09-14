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
    public class PetTypeRepositoryTests
    {
        public void Setup()
        {

        }

        [Test]
        public void FindPetTypeByID_ReturnNotNull()
        {

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "EmployeeDataBase")
            .Options;

            var dbcontext = new Mock<ApplicationDbContext>(options);
            dbcontext.Setup(x => x.FindAsync(typeof(int), default)).ReturnsAsync(new PetType() { });
            var petTypeRepo = new PetTyperepository();
            var task = petTypeRepo.FindByIdAsync(10);
            task.Wait();
            var result = task.Result;

            Assert.IsNotNull(result);
        }

      

        [Test]
        public void FindAllPetTypeByIDs_ReturnNotNull()
        {
            var petRepo = new PetTyperepository();
            var task = petRepo.FindByIdsAsync(new int[] { 10, 20 }, default);
            task.Wait();
            var result = task.Result;

            Assert.IsNotNull(result);


        }

        [Test]
        public void FindAllPetByIDs_ReturnCountGreatorthanZero()
        {
            var petRepo = new PetTyperepository();
            var task = petRepo.FindByIdsAsync(new int[] { 10, 20 }, default);
            task.Wait();
            var result = task.Result;


            Assert.That(result.Count > 0);

        }
    

        [Test]
        public void FindAllPetTypeAsynch_ReturnNotNull()
        {
            var petRepo = new PetTyperepository();
            var task = petRepo.FindAllAsync();
            task.Wait();
            var result = task.Result;

            Assert.IsNotNull(result);


        }

        [Test]
        public void FindAllPetTypeAsynch_ReturnCountGreatorThanZero()
        {
            var petRepo = new PetTyperepository();
            var task = petRepo.FindAllAsync();
            task.Wait();
            var result = task.Result;


            Assert.That(result.Count > 0);

        }


      
    }

    public class PetTyperepository : IPetTypeRepository
    {
        public IUnitOfWork UnitOfWork => throw new NotImplementedException();

        public void Add(IPetType entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AnyAsync(Expression<Func<PetType, bool>> filterExpression, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<int> CountAsync(Expression<Func<PetType, bool>> filterExpression, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<List<IPetType>> FindAllAsync(CancellationToken cancellationToken = default)
        {
            List<IPetType> petType = new List<IPetType>()
            {
                new PetType(){},
                new PetType(){ }
            };

            return Task.FromResult(petType);
        }

        public Task<List<IPetType>> FindAllAsync(Expression<Func<PetType, bool>> filterExpression, CancellationToken cancellationToken = default)
        {
            List<IPetType> pet = new List<IPetType>()
            {
                new PetType(){ },
                new PetType(){ }
            };

            IPetType p = new PetType() { };

            if (filterExpression.Compile().Invoke((PetType)pet.FirstOrDefault()))
            {
                return Task.FromResult(pet);
            }
            else
            {
                return null;
            }
        }

        public Task<List<IPetType>> FindAllAsync(Expression<Func<PetType, bool>> filterExpression, Func<IQueryable<PetType>, IQueryable<PetType>> linq, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IPagedResult<IPetType>> FindAllAsync(int pageNo, int pageSize, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IPagedResult<IPetType>> FindAllAsync(Expression<Func<PetType, bool>> filterExpression, int pageNo, int pageSize, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IPagedResult<IPetType>> FindAllAsync(Expression<Func<PetType, bool>> filterExpression, int pageIndex, int pageSize, Func<IQueryable<PetType>, IQueryable<PetType>> linq, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IPetType> FindAsync(Expression<Func<PetType, bool>> filterExpression, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IPetType> FindByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            IPetType petType = new PetType() { };
            if (id > 0)
            {
                return Task.FromResult(petType);
            }
            else
            {
                return null;
            }
        }

        public Task<List<IPetType>> FindByIdsAsync(int[] ids, CancellationToken cancellationToken = default)
        {

            List<IPetType> petType = new List<IPetType>()
            {
                new PetType(){ },
                new PetType(){}
            };

            if (ids != null && ids.Length > 0)
            {
                return Task.FromResult(petType);
            }
            else
            {
                return null;
            }
        }

        public void Remove(IPetType entity)
        {
            throw new NotImplementedException();
        }
    }
}
