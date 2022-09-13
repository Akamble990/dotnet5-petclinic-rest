using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using AutoMapper;
using Intent.RoslynWeaver.Attributes;
using PetClinic.Domain.Entities;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.Dtos.AutoMapper.MappingExtensions", Version = "1.0")]

namespace PetClinic.Application.Dtos
{
    public static class SpecialtyDTOMappingExtensions
    {
        public static SpecialtyCreateDTO MapToSpecialtyDTO(this ISpecialty projectFrom, IMapper mapper)
        {
            return mapper.Map<SpecialtyCreateDTO>(projectFrom);
        }

        public static List<SpecialtyCreateDTO> MapToSpecialtyDTOList(this IEnumerable<ISpecialty> projectFrom, IMapper mapper)
        {
            return projectFrom.Select(x => x.MapToSpecialtyDTO(mapper)).ToList();
        }
    }
}