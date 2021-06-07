using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization; // +
using Microsoft.AspNetCore.Authentication.JwtBearer; // +
using bison_api.Interface; // +
using Microsoft.Extensions.Configuration; // +
using bison_api.DTO; // +
using System.Security.Claims; // +
using Microsoft.IdentityModel.Tokens; // +
using System.IdentityModel.Tokens.Jwt; // +
using System.Text; // +
using bison_api.Api; // +
using bison_api.Model; // +

namespace bison_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    // + esto indica que necesita autorización por token
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRep Rep;
        private readonly IConfiguration Configuration;

        public UsuarioController(IConfiguration configuration, IUsuarioRep rep)
        {
            Rep = rep;
            Configuration = configuration;
        }

        /// <summary>
        /// Verifica coincidencias del usuario
        /// </summary>
        /// <param name="usuario">Usuario que se logeara</param>
        /// <returns>Token con la información del usuario</returns>
        [HttpPost("Login")]
        [AllowAnonymous] // permite la consulta de la accion aunque el controlador pida token
        public async Task<ActionResult<UsuarioTokenDTO>> Login([FromBody] UsuarioLoginDTO usuario)
        {
            try
            {
                var nodo = await Rep.IsValid(Rep.Mapper.Map<Usuario>(usuario));

                if (nodo == null)
                {
                    return BadRequest("El usuario y/o contraseña no coinciden");
                }

                // si las credenciales fueron correctas regresamos su token
                return BuildToken(nodo, DateTime.Now.AddMinutes(C.C_AUTH_EXPIRACION_TOKEN));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Crea un usuario
        /// </summary>
        /// <param name="usuario">Usuario a crear</param>
        /// <returns>El usuario creado</returns>
        [HttpPost("SignUp")]
        [AllowAnonymous]
        public async Task<ActionResult<UsuarioDTO>> Create([FromBody] UsuarioSignupDTO usuario)
        {
            try
            {
                string mensajeValidacion = await ValidarUsuarioNuevo(usuario);

                if (!string.IsNullOrEmpty(mensajeValidacion))
                {
                    return BadRequest(mensajeValidacion);
                }

                var map = Rep.Mapper.Map<Usuario>(usuario);
                map.IdRol = (int)C.ROL.USUARIO;
                map.Activo = true;

                var nodo = await Rep.Create(map);
                await Rep.Save();

                return Rep.Mapper.Map<UsuarioDTO>(await Rep.Read(nodo.IdUsuario));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Obtiene todos los usuarios
        /// </summary>
        /// <returns>Lista de usuarios</returns>
        [HttpGet("ReadAll")]
        [AllowAnonymous]
        public async Task<ActionResult<List<UsuarioDTO>>> ReadAll()
        {
            try
            {
                return Rep.Mapper.Map<List<UsuarioDTO>>(await Rep.ReadAll());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Obtiene un usuario por su id
        /// </summary>
        /// <returns>Retorna un usuario, si no hubo coincidencia con su id, devuelve nulo</returns>
        [HttpGet("Read")]
        public async Task<ActionResult<UsuarioDTO>> Read(int id)
        {
            try
            {
                return Rep.Mapper.Map<UsuarioDTO>(await Rep.Read(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Construye un token de seguridad
        /// </summary>
        /// <param name="nodo">Usuario para obtener la informacion</param>
        /// <param name="fechaExpiracion">Fecha en que expirará el token</param>
        /// <returns>objeto anonimo con el token y su fecha de expiracion</returns>
        private ActionResult<UsuarioTokenDTO> BuildToken(Usuario nodo, DateTime fechaExpiracion)
        {
            Claim[] claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, Convert.ToString(nodo.IdUsuario)),
                new Claim(JwtRegisteredClaimNames.Email, nodo.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(C.JWT_SECRETKEY_VALUE));
            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: null,
                audience: null,
                claims: claims,
                expires: fechaExpiracion,
                signingCredentials: credentials);

            return Ok(new UsuarioTokenDTO()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = fechaExpiracion,
                IdUsuario = nodo.IdUsuario,
                IdRol = nodo.IdRol,
                Nick = nodo.Nick,
                Email = nodo.Email,
                Nombre = nodo.Nombre,
                ApPaterno = nodo.ApPaterno,
                ApMaterno = nodo.ApMaterno,
                Activo = nodo.Activo,
                RutaImagen = nodo.RutaImagen,
                FechaAlta = nodo.FechaAlta,
                FechaMod = nodo.FechaMod
            });
        }

        private async Task<string> ValidarUsuarioNuevo(UsuarioSignupDTO usuario)
        {
            Usuario aux = null;

            if (string.IsNullOrEmpty(usuario.Nick))
            {
                return "El nick no ha sido proporcionado";
            }

            if (string.IsNullOrEmpty(usuario.Pass))
            {
                return "La contraseña no ha sido proporcionada";
            }

            if (string.IsNullOrEmpty(usuario.Email))
            {
                return "El email no ha sido proporcionado";
            }

            aux = await Rep.Exists(usuario.Email);

            if (aux != null)
            {
                return $"El email [{usuario.Email}] ya ha sido usado, proporcione uno diferente";
            }

            return null;
        }
    }
}
