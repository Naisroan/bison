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

        public Task<int> Save();

        public Task<Usuario> Exists(string email);

        public Task<Usuario> Create(Usuario usuario);

        public Task<Usuario> Read(int id);

        public Task<List<Usuario>> ReadAll();

        public Task<Usuario> IsValid(Usuario usuario);
    }
}
