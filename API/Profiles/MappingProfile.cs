using API.Dtos;
using API.Dtos.Generic;
using AutoMapper;
using Dominio.Entities;
using Dominio.Entities.Generic;

namespace API.Profiles;
    public class MappingProfile : Profile {

        public MappingProfile() {

            CreateMap<Rol, RolDto>()
                .ReverseMap();

            CreateMap<Usuario, UsuarioDto>()
                .ReverseMap();

            CreateMap<Candidato, CandidatoDto>()
                .ReverseMap();

        }

    }
