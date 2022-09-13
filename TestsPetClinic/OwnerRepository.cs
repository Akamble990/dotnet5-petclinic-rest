using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using PetClinic.Domain.Entities;
using PetClinic.Domain.Repositories;
using PetClinic.Infrastructure.Persistence;
using PetClinic.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestsPetClinic
{
    [TestFixture]
    public class OwnerRepositoryTest
    {
        public void Setup()
        {

        }

        //[Test]
        //public void FindOwnerByID()
        //{

        //    var options = new DbContextOptionsBuilder<ApplicationDbContext>()
        //    .UseInMemoryDatabase(databaseName: "EmployeeDataBase")
        //    .Options;

        //    var dbcontext = new Mock<ApplicationDbContext>(options);
        //    dbcontext.Setup(x => x.Add(new Owner() { Id = 1, FirstName = "Amit" }));
        //    var iownerRepo = new Mock<IAddPetRepository>();
        //    iownerRepo.Setup(x => x.Add(new Owner() { Id = 1, FirstName = "Amit" }));
        //    iownerRepo.Verify(x => x.Add(new Owner() { Id = 1, FirstName = "Amit" }));
           
        //}

        


    }
}
