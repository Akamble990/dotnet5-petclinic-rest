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
    public class VisitRepositoryTests
    {

        [Test]
        public void FindvisitByID_ReturnNotNull()
        {

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "EmployeeDataBase")
            .Options;

            var dbcontext = new Mock<ApplicationDbContext>(options);
            dbcontext.Setup(x => x.FindAsync(typeof(int), default)).ReturnsAsync(new Visit() { Id = 1, Description = "Amit" });
            var visitRepo = new VisitRepository();
            var task = visitRepo.FindByIdAsync(1);
            task.Wait();
            var result = task.Result;

            Assert.IsNotNull(result);
        }

        [Test]
        public void FindAllVisitByIDs_ReturnNotNull()
        {
            var visitRepo = new VisitRepository();
            var task = visitRepo.FindByIdsAsync(new int[] { 10, 20 }, default);
            task.Wait();
            var result = task.Result;

            Assert.IsNotNull(result);
        
         
        }
        [Test]
        public void FindAllVisitByIDs_ReturnCountGreatorthanZero()
        {
            var visitRepo = new VisitRepository();
            var task = visitRepo.FindByIdsAsync(new int[] { 10, 20 }, default);
            task.Wait();
            var result = task.Result;

            Assert.That(result.Count > 0);

        }



        [Test]
        public void FindAllvisitAsynch_ReturnNotNull()
        {
            var visitRepo = new VisitRepository();
            var task = visitRepo.FindAllAsync();
            task.Wait();
            var result = task.Result;

            Assert.IsNotNull(result);
 
            Assert.That(result.Any(x => x.Id == 2));
        }

        [Test]
        public void FindAllVisitAsynch_ReturnCountGreatorThanZero()
        {
            var visitRepo = new VisitRepository();
            var task = visitRepo.FindAllAsync();
            task.Wait();
            var result = task.Result;

            Assert.That(result.Count > 0);

        }



    }
    public class VisitRepository : IVisitRepository
    {
        public IUnitOfWork UnitOfWork => throw new NotImplementedException();

        public void Add(IVisit entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AnyAsync(Expression<Func<Visit, bool>> filterExpression, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<int> CountAsync(Expression<Func<Visit, bool>> filterExpression, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<List<IVisit>> FindAllAsync(CancellationToken cancellationToken = default)
        {
            List<IVisit> visit = new List<IVisit>()
            {
                new Visit(){Id=2,Description="Sanket" },
                new Visit(){Id=3,Description="Amit" }
            };

            return Task.FromResult(visit);
        }

        public Task<List<IVisit>> FindAllAsync(Expression<Func<Visit, bool>> filterExpression, CancellationToken cancellationToken = default)
        {
            List<IVisit> visit = new List<IVisit>()
            {
                new Visit(){Id=2,Description="Sanket" },
                new Visit(){Id=3,Description="Amit" }
            };

            IVisit vs = new Visit() { Id = 3, Description = "Amit" };

            if (filterExpression.Compile().Invoke((Visit)visit.FirstOrDefault()))
            {
                return Task.FromResult(visit);
            }
            else
            {
                return null;
            }
        }

        public Task<List<IVisit>> FindAllAsync(Expression<Func<Visit, bool>> filterExpression, Func<IQueryable<Visit>, IQueryable<Visit>> linq, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IPagedResult<IVisit>> FindAllAsync(int pageNo, int pageSize, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IPagedResult<IVisit>> FindAllAsync(Expression<Func<Visit, bool>> filterExpression, int pageNo, int pageSize, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IPagedResult<IVisit>> FindAllAsync(Expression<Func<Visit, bool>> filterExpression, int pageIndex, int pageSize, Func<IQueryable<Visit>, IQueryable<Visit>> linq, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IVisit> FindAsync(Expression<Func<Visit, bool>> filterExpression, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IVisit> FindByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            IVisit visit = new Visit() { Id = 10, Description = "aasdfd"};
            if (id > 0)
            {
                return Task.FromResult(visit);
            }
            else
            {
                return null;
            }
        }

        public Task<List<IVisit>> FindByIdsAsync(int[] ids, CancellationToken cancellationToken = default)
        {
            List<IVisit> visit = new List<IVisit>()
            {
                new Visit(){Id=2,Description="Sanket" },
                new Visit(){Id=3,Description="Amit" }
            };

            if (ids != null && ids.Length > 0)
            {
                return Task.FromResult(visit);
            }
            else
            {
                return null;
            }
        }

        public void Remove(IVisit entity)
        {
            throw new NotImplementedException();
        }
    }
}
