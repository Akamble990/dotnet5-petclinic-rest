using System;
using System.Collections.Generic;
using AutoMapper;
using Intent.RoslynWeaver.Attributes;
using PetClinic.Application.Common.Mappings;
using PetClinic.Domain.Entities;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.Dtos.DtoModel", Version = "1.0")]

namespace PetClinic.Application.Dtos
{

    public class SpecialtyCreateDTO : IMapFrom<Specialty>
    {
        public SpecialtyCreateDTO()
        {
        }

        public static SpecialtyCreateDTO Create(
            int? id,
            string name)
        {
            return new SpecialtyCreateDTO
            {
                Id = id,
                Name = name,
            };
        }

        public int? Id { get; set; }

        public string Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Specialty, SpecialtyCreateDTO>()
                .ForMember(d => d.Id, opt => opt.MapFrom(src => (int?)src.Id));
        }
    }
}