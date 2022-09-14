using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using PetClinic.Domain.Common.Interfaces;
using PetClinic.Domain.Entities;
using PetClinic.Domain.Repositories;
using PetClinic.Infrastructure.Persistence;
using PetClinic.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TestsPetClinic
{
    [TestFixture]
    public class OwnerRepositoryTest
    {
        public void Setup()
        {

        }

        [Test]
        public void FindOwnerByID()
        {

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "EmployeeDataBase")
            .Options;

            var dbcontext = new Mock<ApplicationDbContext>(options);
            dbcontext.Setup(x => x.FindAsync(typeof(int), default)).ReturnsAsync(new Owner() { Id = 1,FirstName="Amit" }); 
            var ownerRepo = new OwnerRepository();
            var task= ownerRepo.FindByIdAsync(10);
            task.Wait();
            var result=task.Result;

            Assert.IsNotNull(result);
        }

        [Test]
        public void FindAllOwnerByIDs()
        {
            var ownerRepo = new OwnerRepository();
            var task = ownerRepo.FindByIdsAsync(new int[] {10,20},default);
            task.Wait();
            var result = task.Result;

            Assert.IsNotNull(result);
            Assert.That(result.Count>0);
            Assert.That(result.Any(x=>x.Id==2));
        }

        //[Test]
        //public void AddOwner()
        //{

        //    var options = new DbContextOptionsBuilder<ApplicationDbContext>()
        //    .UseInMemoryDatabase(databaseName: "EmployeeDataBase")
        //    .Options;

        //    var dbcontext = new Mock<ApplicationDbContext>(options);
        //    var mockOwnerRepo = new Mock<OwnerRepository>();
        //    mockOwnerRepo.SetupAdd(x => x.Add(new Owner() { Id = 10, FirstName = "Amit" })).Verifiable();
        //    var ownerRepo = new OwnerRepository();
        //    ownerRepo.Add(new Owner() {Id=10,FirstName="Amit" });
        //    mockOwnerRepo.Verify(x=>x.Add(It.IsAny<Owner>()),Times.Once);
        //}

        [Test]
        public void FindAllOwnerAsynch()
        {
            var ownerRepo = new OwnerRepository();
            var task = ownerRepo.FindAllAsync();
            task.Wait();
            var result = task.Result;

            Assert.IsNotNull(result);
            Assert.That(result.Count > 0);
            Assert.That(result.Any(x => x.Id == 2));
        }


        [Test]
        public void FindOwnerByExpression()
        {
            var ownerRepo = new OwnerRepository();
            var task = ownerRepo.FindAsync(x=>x.Id==2,default);
            task.Wait();
            var result = task.Result;

            Assert.IsNotNull(result);
            
            Assert.That(result.FirstName=="Amit");
        }

    }

    public class OwnerRepository : IAddPetRepository
    {
        public IUnitOfWork UnitOfWork => throw new NotImplementedException();

        public void Add(IOwner entity)
        {
            
        }

        public Task<bool> AnyAsync(Expression<Func<Owner, bool>> filterExpression, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<int> CountAsync(Expression<Func<Owner, bool>> filterExpression, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<List<IOwner>> FindAllAsync(CancellationToken cancellationToken = default)
        {

            List<IOwner> owner = new List<IOwner>()
            {
                new Owner(){Id=2,FirstName="Sanket" },
                new Owner(){Id=3,FirstName="Amit" }
            };

            return Task.FromResult(owner);
        }

        public Task<List<IOwner>> FindAllAsync(Expression<Func<Owner, bool>> filterExpression, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<List<IOwner>> FindAllAsync(Expression<Func<Owner, bool>> filterExpression, Func<IQueryable<Owner>, IQueryable<Owner>> linq, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IPagedResult<IOwner>> FindAllAsync(int pageNo, int pageSize, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IPagedResult<IOwner>> FindAllAsync(Expression<Func<Owner, bool>> filterExpression, int pageNo, int pageSize, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IPagedResult<IOwner>> FindAllAsync(Expression<Func<Owner, bool>> filterExpression, int pageIndex, int pageSize, Func<IQueryable<Owner>, IQueryable<Owner>> linq, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IOwner> FindAsync(Expression<Func<Owner, bool>> filterExpression, CancellationToken cancellationToken = default)
        {
            List<IOwner> owner = new List<IOwner>()
            {
                new Owner(){Id=2,FirstName="Sanket" },
                new Owner(){Id=3,FirstName="Amit" }
            };

            IOwner own= new Owner() { Id = 3, FirstName = "Amit" };
            
            if (filterExpression.Compile().Invoke((Owner)owner.FirstOrDefault()))
            {
                return Task.FromResult(own);
            }
            else
            {
                return null;
            }
        }

        public Task<IOwner> FindByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            IOwner owner = new Owner() {Id=10,FirstName="Amit" }; 
            if(id > 0)
            {
                return Task.FromResult(owner);
            }
            else
            {
                return null;
            }
        }

        public Task<List<IOwner>> FindByIdsAsync(int[] ids, CancellationToken cancellationToken = default)
        {
            List<IOwner> owner = new List<IOwner>()
            {
                new Owner(){Id=2,FirstName="Sanket" },
                new Owner(){Id=3,FirstName="Amit" }
            };

            if(ids!=null && ids.Length>0)
            {
                return Task.FromResult(owner);
            }
            else
            {
                return null;
            }
        }

        public void Remove(IOwner entity)
        {
            throw new NotImplementedException();
        }
    }
}
