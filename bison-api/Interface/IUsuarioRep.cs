using AutoMapper;
using bison_api.DTO;
using bison_api.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace bison_api.Interface
{
    public interface IUsuarioRep
    {
        public bisonContext Context { get; }

        public IMapper Mapper { get; }

        public Task<UsuarioDTO> Read(int id);

        public Task<List<UsuarioDTO>> ReadAll();

        public Task<UsuarioDTO> IsValid(UsuarioLoginDTO usuario);
    }
}
