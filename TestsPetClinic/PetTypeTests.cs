using PetClinic.Application.Dtos;
using PetClinic.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestsPetClinic
{
    public class PetTypeServicesTests
    {
    }

    public class PetTypeService : IPetTypeService
    {
        public Task<int> AddPetType(PetTypeDTO dto)
        {
            throw new NotImplementedException();
        }

        public Task DeletePetType(int petTypeId)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task<List<PetTypeDTO>> GetAllPetTypes()
        {
            throw new NotImplementedException();
        }

        public Task<PetTypeDTO> GetPetType(int petTypeId)
        {
            throw new NotImplementedException();
        }

        public Task UpdatePetType(int petTypeId, PetTypeDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}
