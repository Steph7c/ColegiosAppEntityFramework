using AutoMapper;
using ColegioAPI.Models;
using ColegioDomain.Entidades;

namespace ColegioAPI.Utilidades
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<UsuarioModel, Usuario>()
                .ForMember(u => u.Id,
                opt => opt.MapFrom(src => Guid.Parse(src.Id)));

            CreateMap<Usuario, UsuarioModel>()
                .ForMember(u => u.Id,
                opt => opt.MapFrom(src => src.Id.ToString()));
        }
    }
}
