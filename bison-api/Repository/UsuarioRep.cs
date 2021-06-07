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

        public async Task<int> Save()
        {
            return await Context.SaveChangesAsync();
        }

        public async Task<Usuario> Create(Usuario usuario)
        {
            var nodo = Mapper.Map<Usuario>(usuario);
            await Context.Usuario.AddAsync(nodo);

            return nodo;
        }

        public async Task<Usuario> Exists(string email)
        {
            return await Context.Usuario.FirstOrDefaultAsync(n => n.Email == email);
        }

        public async Task<Usuario> Read(int id)
        {
            return await Context.Usuario.FirstOrDefaultAsync(n => n.IdUsuario == id);
        }

        public async Task<List<Usuario>> ReadAll()
        {
            return await Context.Usuario.AsNoTracking().ToListAsync();
        }

        public async Task<Usuario> IsValid(Usuario usuario)
        {
            return await Context.Usuario.FirstOrDefaultAsync(n => n.Email == usuario.Email && n.Pass == usuario.Pass);
        }
    }
}
