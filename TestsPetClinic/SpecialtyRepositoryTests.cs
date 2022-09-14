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
    public class SpecialtyRepositoryTests
    {
        [Test]
        public void FindSpecialtyByID_ReturnNotNull()
        {

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "EmployeeDataBase")
            .Options;

            var dbcontext = new Mock<ApplicationDbContext>(options);
            dbcontext.Setup(x => x.FindAsync(typeof(int), default)).ReturnsAsync(new Specialty() { Id = 1, Name = "Amit" });
            var specialtyRepo = new SpecialtyRepository();
            var task = specialtyRepo.FindByIdAsync(1);
            task.Wait();
            var result = task.Result;

            Assert.IsNotNull(result);
        }

        [Test]
        public void FindAllSpecialtyByIDs_ReturnNotNull()
        {
            var specialtyRepo = new SpecialtyRepository();
            var task = specialtyRepo.FindByIdsAsync(new int[] { 10, 20 }, default);
            task.Wait();
            var result = task.Result;

            Assert.IsNotNull(result);
            Assert.That(result.Count > 0);
            Assert.That(result.Any(x => x.Id == 2));
        }
        [Test]
        public void FindAllSpecialtyByIDs_ReturnCountGreatorthanZero()
        {
            var specialtyRepo = new SpecialtyRepository();
            var task = specialtyRepo.FindByIdsAsync(new int[] { 10, 20 }, default);
            task.Wait();
            var result = task.Result;

            Assert.That(result.Count > 0);
           
        }

        

        [Test]
        public void FindAllSpecialtyAsynch_ReturnNotNull()
        {
            var specialtyRepo = new SpecialtyRepository();
            var task = specialtyRepo.FindAllAsync();
            task.Wait();
            var result = task.Result;

            Assert.IsNotNull(result);
            Assert.That(result.Count > 0);
            Assert.That(result.Any(x => x.Id == 2));
        }

        [Test]
        public void FindAllSpecialtyAsynch_ReturnCountGreatorThanZero()
        {
            var specialtyRepo = new SpecialtyRepository();
            var task = specialtyRepo.FindAllAsync();
            task.Wait();
            var result = task.Result;

            Assert.That(result.Count > 0);
        
        }


        [Test]
        public void FindOwnerByExpression()
        {
            var specialtyRepo = new SpecialtyRepository();
            var task = specialtyRepo.FindAsync(x => x.Id == 2, default);
            task.Wait();
            var result = task.Result;

            Assert.IsNotNull(result);

            Assert.That(result.Name == "Amit");
        }

    }
    public class SpecialtyRepository : ISpecialtyRepository
    {
        public IUnitOfWork UnitOfWork => throw new NotImplementedException();

        public void Add(ISpecialty entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AnyAsync(Expression<Func<Specialty, bool>> filterExpression, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<int> CountAsync(Expression<Func<Specialty, bool>> filterExpression, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<List<ISpecialty>> FindAllAsync(CancellationToken cancellationToken = default)
        {
            List<ISpecialty> owner = new List<ISpecialty>()
            {
                new Specialty(){Id=2,Name="Sanket" },
                new Specialty(){Id=3,Name="Amit" }
            };

            return Task.FromResult(owner);
        }

        public Task<List<ISpecialty>> FindAllAsync(Expression<Func<Specialty, bool>> filterExpression, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<List<ISpecialty>> FindAllAsync(Expression<Func<Specialty, bool>> filterExpression, Func<IQueryable<Specialty>, IQueryable<Specialty>> linq, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IPagedResult<ISpecialty>> FindAllAsync(int pageNo, int pageSize, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IPagedResult<ISpecialty>> FindAllAsync(Expression<Func<Specialty, bool>> filterExpression, int pageNo, int pageSize, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IPagedResult<ISpecialty>> FindAllAsync(Expression<Func<Specialty, bool>> filterExpression, int pageIndex, int pageSize, Func<IQueryable<Specialty>, IQueryable<Specialty>> linq, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<ISpecialty> FindAsync(Expression<Func<Specialty, bool>> filterExpression, CancellationToken cancellationToken = default)
        {
            List<ISpecialty> specialty = new List<ISpecialty>()
            {
                new Specialty(){Id=2,Name="Sanket" },
                new Specialty(){Id=3,Name="Amit" }
            };

            ISpecialty spe = new Specialty() { Id = 3, Name = "Amit" };

            if (filterExpression.Compile().Invoke((Specialty)specialty.FirstOrDefault()))
            {
                return Task.FromResult(spe);
            }
            else
            {
                return null;
            }
        }

        public Task<ISpecialty> FindByIdAsync(int id, CancellationToken cancellationToken = default)
        {

            ISpecialty specialty = new Specialty() { Id = 10,Name = "amitraj" };
            if (id > 0)
            {
                return Task.FromResult(specialty);
            }
            else
            {
                return null;
            }
        }

        public Task<List<ISpecialty>> FindByIdsAsync(int[] ids, CancellationToken cancellationToken = default)
        {
            List<ISpecialty> specialty = new List<ISpecialty>()
            {
                new Specialty(){Id=2,Name="Sanket" },
                new Specialty(){Id=3,Name="Amit" }
            };

            if (ids != null && ids.Length > 0)
            {
                return Task.FromResult(specialty);
            }
            else
            {
                return null;
            }
        }

        public void Remove(ISpecialty entity)
        {
            throw new NotImplementedException();
        }
    }
}
