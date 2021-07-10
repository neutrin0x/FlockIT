using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using WebApi.Entities;
using WebApi.Helpers;

namespace WebApi.Services
{
    public interface IUsuarioService
    {
        Usuario Authenticate(string nombreUsuario, string password);
        IEnumerable<Usuario> GetAll();
        Usuario GetById(int id);
    }

    public class UsuarioService : IUsuarioService
    {
        // hard code de usuarios para simplificar...
        private List<Usuario> _Usuarios = new List<Usuario>
        { 
            new Usuario { Id = 1, Nombre = "Admin", Apellido = "User", NombreUsuario = "admin", Password = "admin", Rol = Role.Admin },
            new Usuario { Id = 2, Nombre = "Pablo", Apellido = "Insaurralde", NombreUsuario = "prueba", Password = "1234", Rol = Role.User } 
        };

        private readonly AppSettings _appSettings;

        public UsuarioService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public Usuario Authenticate(string username, string password)
        {
            var user = _Usuarios.SingleOrDefault(x => x.NombreUsuario == username && x.Password == password);

            // si no existe el usuario...
            if (user == null)
                return null;

            // autenticacion ok!
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] 
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Rol)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            //devolvemos la lista de usuarios pero sin el password
            return user.WithoutPassword();
        }

        public IEnumerable<Usuario> GetAll()
        {
            return _Usuarios.WithoutPasswords();
        }

        public Usuario GetById(int id) 
        {
            var user = _Usuarios.FirstOrDefault(x => x.Id == id);
            return user.WithoutPassword();
        }
    }
}