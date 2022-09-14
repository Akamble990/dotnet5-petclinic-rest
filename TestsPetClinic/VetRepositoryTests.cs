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
    public class VetRepositoryTests
    {
        [Test]
        public void FindSVetByID_ReturnNotNull()
        {

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "EmployeeDataBase")
            .Options;

            var dbcontext = new Mock<ApplicationDbContext>(options);
            dbcontext.Setup(x => x.FindAsync(typeof(int), default)).ReturnsAsync(new Vet() { Id = 1, FirstName = "amit" });
            var vetRepo = new VetRepository();
            var task = vetRepo.FindByIdAsync(1);
            task.Wait();
            var result = task.Result;

            Assert.IsNotNull(result);
        }

        [Test]
        public void FindAllVetByIDs_ReturnNotNull()
        {
            var vetRepo = new VetRepository();
            var task = vetRepo.FindByIdsAsync(new int[] { 10, 20 }, default);
            task.Wait();
            var result = task.Result;

            Assert.IsNotNull(result);
            Assert.That(result.Count > 0);
            Assert.That(result.Any(x => x.Id == 2));
        }
        [Test]
        public void FindAllVetByIDs_ReturnCountGreatorthanZero()
        {
            var vetRepo = new VetRepository();
            var task = vetRepo.FindByIdsAsync(new int[] { 10, 20 }, default);
            task.Wait();
            var result = task.Result;

            Assert.That(result.Count > 0);

        }



        [Test]
        public void FindAllVetAsynch_ReturnNotNull()
        {
            var vetRepo = new VetRepository();
            var task = vetRepo.FindAllAsync();
            task.Wait();
            var result = task.Result;

            Assert.IsNotNull(result);
           
            Assert.That(result.Any(x => x.Id == 2));
        }

        [Test]
        public void FindAllVetAsynch_ReturnCountGreatorThanZero()
        {
            var vetRepo = new VetRepository();
            var task = vetRepo.FindAllAsync();
            task.Wait();
            var result = task.Result;

            Assert.That(result.Count > 0);

        }


        [Test]
        public void FindVetByExpression()
        {
            var vetRepo = new VetRepository();
            var task = vetRepo.FindAsync(x => x.Id == 2, default);
            task.Wait();
            var result = task.Result;

            Assert.IsNotNull(result);

            Assert.That(result.FirstName == "Amit");
        }
    }
    public class VetRepository : IVetRepository
    {
        public IUnitOfWork UnitOfWork => throw new NotImplementedException();

        public void Add(IVet entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AnyAsync(Expression<Func<Vet, bool>> filterExpression, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<int> CountAsync(Expression<Func<Vet, bool>> filterExpression, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<List<IVet>> FindAllAsync(CancellationToken cancellationToken = default)
        {

            List<IVet> vet = new List<IVet>()
            {
                new Vet(){Id=2,FirstName="Sanket" },
                new Vet(){Id=3,FirstName="Amit" }
            };

            return Task.FromResult(vet);
        }

        public Task<List<IVet>> FindAllAsync(Expression<Func<Vet, bool>> filterExpression, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<List<IVet>> FindAllAsync(Expression<Func<Vet, bool>> filterExpression, Func<IQueryable<Vet>, IQueryable<Vet>> linq, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IPagedResult<IVet>> FindAllAsync(int pageNo, int pageSize, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IPagedResult<IVet>> FindAllAsync(Expression<Func<Vet, bool>> filterExpression, int pageNo, int pageSize, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IPagedResult<IVet>> FindAllAsync(Expression<Func<Vet, bool>> filterExpression, int pageIndex, int pageSize, Func<IQueryable<Vet>, IQueryable<Vet>> linq, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IVet> FindAsync(Expression<Func<Vet, bool>> filterExpression, CancellationToken cancellationToken = default)
        {
            IVet vet = new Vet() { Id = 10, FirstName = "Amit" };
           
            
                return Task.FromResult(vet);
            

        }

        public Task<IVet> FindByIdAsync(int id, CancellationToken cancellationToken = default)
        {

            IVet vet = new Vet() { Id = 10, FirstName = "Amit" };
            if (id > 0)
            {
                return Task.FromResult(vet);
            }
            else
            {
                return null;
            }
        }

        public Task<List<IVet>> FindByIdsAsync(int[] ids, CancellationToken cancellationToken = default)
        {
            List<IVet> owner = new List<IVet>()
            {
                new Vet(){Id=2,FirstName="Sanket" },
                new Vet(){Id=3,FirstName="Amit" }
            };

            if (ids != null && ids.Length > 0)
            {
                return Task.FromResult(owner);
            }
            else
            {
                return null;
            }
        }

        public void Remove(IVet entity)
        {
            throw new NotImplementedException();
        }
    }
}
