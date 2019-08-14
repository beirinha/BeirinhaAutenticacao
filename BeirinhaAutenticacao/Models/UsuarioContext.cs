using System.Data.Entity;

namespace BeirinhaAutenticacao.Models
{
    public class UsuarioContext : DbContext
    {
        public UsuarioContext(): base ("Usuarios")
        {

        }

        public DbSet<Usuario> Usuarios { get; set; }
    }
}