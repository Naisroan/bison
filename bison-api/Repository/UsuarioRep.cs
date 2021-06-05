using AutoMapper;
using bison_api.DTO;
using bison_api.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace bison_api.Repository
{
    public class UsuarioRep : Interface.IUsuarioRep
    {
        public bisonContext Context { get; }

        public IMapper Mapper { get; }

        public UsuarioRep(bisonContext cntx, IMapper mapper)
        {
            Context = cntx;
            Mapper = mapper;
        }
        public async Task<UsuarioDTO> Read(int id)
        {
            return Mapper.Map<UsuarioDTO>(await Context.Usuario.FirstOrDefaultAsync(n => n.IdUsuario == id));
        }

        public async Task<List<UsuarioDTO>> ReadAll()
        {
            return Mapper.Map<List<UsuarioDTO>>(await Context.Usuario.AsNoTracking().ToListAsync());
        }

        public async Task<UsuarioDTO> IsValid(UsuarioLoginDTO usuario)
        {
            var nodo = await Context.Usuario.FirstOrDefaultAsync(n => n.Nick == usuario.Nick && n.Pass == usuario.Pass);

            return nodo != null ? Mapper.Map<UsuarioDTO>(nodo) : null;
        }
    }
}
