using ColegioDomain.Entidades;
using ColegioDomain.Repositories;
using ColegioDomain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColegioInfrastructure.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepositorio _repositorio;

        public UsuarioService(IUsuarioRepositorio repositorio)
        {
            _repositorio = repositorio;
        }
        public async Task<Usuario> ActualizarUsuario(Usuario usuario)
        {
            return await _repositorio.ActualizarUsuario(usuario);
        }

        public async Task<Usuario> CrearUsuario(Usuario usuario) => await _repositorio.CrearUsuario(usuario);

        public async Task<bool> EliminarUsuario(Guid id) => await _repositorio.EliminarUsuario(id);

        public async Task<IEnumerable<Usuario>> ObtenerUsuarios() => await _repositorio.ObtenerUsuarios();

        public async Task<IEnumerable<Usuario>> ObtenerUsuariosActivos() => await _repositorio.ObtenerUsuariosActivos();

        public async Task<IEnumerable<Usuario>> ObtenerUsuariosPorNombre(string nombre) => await _repositorio.ObtenerUsuariosPorNombre(nombre);


        public async Task<IEnumerable<Usuario>> ObtenerUsuariosPorRol(string rol) => await _repositorio.ObtenerUsuariosPorRol(rol);

        //Adicionales
        public async Task<IEnumerable<Usuario>> ObtenerUsuariosInactivos() => await _repositorio.ObtenerUsuariosInactivos();
        public async Task<Usuario>ObtenerUsuarioPorId(string id)
        {
            return await _repositorio.ObtenerUsuarioPorId(Guid.Parse(id));
        } 



    }
}
