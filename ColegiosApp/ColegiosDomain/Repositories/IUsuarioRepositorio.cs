using ColegioDomain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColegioDomain.Repositories
{
    public interface IUsuarioRepositorio
    {
        Task<Usuario> CrearUsuario(Usuario usuario);
        Task<Usuario> ActualizarUsuario(Usuario usuario);
        Task<bool> EliminarUsuario(Guid id);
        Task<IEnumerable<Usuario>> ObtenerUsuarios();
        Task<IEnumerable<Usuario>> ObtenerUsuariosActivos();
        Task<IEnumerable<Usuario>> ObtenerUsuariosPorNombre(string nombre);
        Task<IEnumerable<Usuario>> ObtenerUsuariosPorRol(string rol);

        //Adicionales
        Task<IEnumerable<Usuario>> ObtenerUsuariosInactivos();
        Task<Usuario>ObtenerUsuarioPorId(Guid id);

    }
}
