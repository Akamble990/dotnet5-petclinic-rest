using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Intent.RoslynWeaver.Attributes;
using PetClinic.Application.Dtos;
using PetClinic.Application.Interfaces;
using PetClinic.Domain.Common.Interfaces;
using PetClinic.Domain.Entities;
using PetClinic.Domain.Repositories;

[assembly: DefaultIntentManaged(Mode.Merge)]
[assembly: IntentTemplate("Intent.Application.ServiceImplementations", Version = "1.0")]

namespace PetClinic.Application.Implementation
{
    public class SpecialtyService : ISpecialtyService
    {
        private ISpecialtyRepository _specialtyRepository;
        private IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public SpecialtyService(ISpecialtyRepository specialtyRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _specialtyRepository = specialtyRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [IntentManaged(Mode.Merge, Body = Mode.Ignore, Signature = Mode.Fully)]
        public async Task<List<SpecialtyCreateDTO>> GetAllSpecialties()
        {
            var elements = await _specialtyRepository.FindAllAsync();
            return elements.MapToSpecialtyDTOList(_mapper);
        }

        [IntentManaged(Mode.Merge, Body = Mode.Ignore, Signature = Mode.Fully)]
        public async Task<SpecialtyCreateDTO> GetSpecialty(int specialtyId)
        {
            var element = await _specialtyRepository.FindByIdAsync(specialtyId);
            return element.MapToSpecialtyDTO(_mapper);
        }

        [IntentManaged(Mode.Merge, Body = Mode.Ignore, Signature = Mode.Fully)]
        public async Task<int> AddSpecialty(SpecialtyCreateDTO dto)
        {
            var newSpecialty = new Specialty
            {
                Name = dto.Name,
            };

            _specialtyRepository.Add(newSpecialty);
            await _unitOfWork.SaveChangesAsync();
            return newSpecialty.Id;
        }

        [IntentManaged(Mode.Merge, Body = Mode.Ignore, Signature = Mode.Fully)]
        public async Task UpdateSpecialty(int specialtyId, SpecialtyCreateDTO dto)
        {
            var existingSpecialty = await _specialtyRepository.FindByIdAsync(specialtyId);
            existingSpecialty.Name = dto.Name;
        }

        [IntentManaged(Mode.Merge, Body = Mode.Ignore, Signature = Mode.Fully)]
        public async Task DeleteSpecialty(int specialtyId)
        {
            var existingSpecialty = await _specialtyRepository.FindByIdAsync(specialtyId);
            _specialtyRepository.Remove(existingSpecialty);
        }

        public void Dispose()
        {
        }
    }
}