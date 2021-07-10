using System.Collections.Generic;
using System.Linq;
using WebApi.Entities;

namespace WebApi.Helpers
{
    public static class ExtensionMethods
    {
        public static IEnumerable<Usuario> WithoutPasswords(this IEnumerable<Usuario> Usuarios) 
        {
            if (Usuarios == null) return null;

            return Usuarios.Select(x => x.WithoutPassword());
        }

        public static Usuario WithoutPassword(this Usuario user) 
        {
            if (user == null) return null;

            user.Password = null;
            return user;
        }
    }
}