using AutoMapper;
using bison_api.DTO;
using bison_api.Model;

namespace bison_api.Helpers
{
    /// <summary>
    /// + Aquí se asignan los mapeos de las clases abstractas a sus respectivos DTOs
    /// </summary>
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            // Usuario.cs
            CreateMap<Usuario, UsuarioDTO>().ReverseMap();
        }
    }
}
