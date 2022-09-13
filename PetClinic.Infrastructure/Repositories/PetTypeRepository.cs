using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Intent.RoslynWeaver.Attributes;
using Microsoft.EntityFrameworkCore;
using PetClinic.Domain.Entities;
using PetClinic.Domain.Repositories;
using PetClinic.Infrastructure.Persistence;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.EntityFrameworkCore.Repositories.Repository", Version = "1.0")]

namespace PetClinic.Infrastructure.Repositories
{
    [IntentManaged(Mode.Merge)]
    public class PetTypeRepository : RepositoryBase<IPetType, PetType, ApplicationDbContext>, IPetTypeRepository
    {
        [IntentManaged(Mode.Ignore, Signature = Mode.Fully)]
        public PetTypeRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        [IntentManaged(Mode.Fully)]
        public async Task<IPetType> FindByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await FindAsync(x => x.Id == id, cancellationToken);
        }

        [IntentManaged(Mode.Fully)]
        public async Task<List<IPetType>> FindByIdsAsync(int[] ids, CancellationToken cancellationToken = default)
        {
            return await FindAllAsync(x => ids.Contains(x.Id), cancellationToken);
        }
    }
}