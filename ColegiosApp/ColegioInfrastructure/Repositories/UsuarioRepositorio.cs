using ColegioData.Context;
using ColegioDomain.Entidades;
using ColegioDomain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColegioInfrastructure.Repositories
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly ColegioDbContext _context;
        public UsuarioRepositorio(ColegioDbContext context)
        {
            _context = context;
        }
        public async Task<Usuario> ActualizarUsuario(Usuario usuario)
        {
            _context.Usuarios.Update(usuario);
            await _context.SaveChangesAsync();
            return usuario;
        }

        public async Task<Usuario> CrearUsuario(Usuario usuario)
        {
            await _context.Usuarios.AddAsync(usuario);
            await _context.SaveChangesAsync();
            return usuario;
        }

        public async Task<bool> EliminarUsuario(Guid id)
        {
             var Usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == id);
            
            if (Usuario == null) 
            {
                return false; 
            }
            
            else 
            { 
                _context.Usuarios.Remove(Usuario);
                return await _context.SaveChangesAsync() > 0;
            }
        }

        public async Task<IEnumerable<Usuario>> ObtenerUsuarios()
        {
            return await _context.Usuarios.ToListAsync();
        }

        public async Task<IEnumerable<Usuario>> ObtenerUsuariosActivos()
        {
            return await _context.Usuarios.Where(u => u.Estado == true).ToListAsync();
        }

        public async Task<IEnumerable<Usuario>> ObtenerUsuariosPorNombre(string nombre)
        {
            return await _context.Usuarios.Where(u => u.Nombres == nombre).ToListAsync();
        }

        public async Task<IEnumerable<Usuario>> ObtenerUsuariosPorRol(string rol)
        {
            return await _context.Usuarios.Where(u => u.Rol  == rol).ToListAsync();
        }

        //Adicionales
        public async Task<IEnumerable<Usuario>> ObtenerUsuariosInactivos()
        {
            return await _context.Usuarios.Where(u => !u.Estado).ToListAsync();
        }

        public async Task<Usuario>ObtenerUsuarioPorId(Guid id)
        {

            return await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == id);
        }

    }
}
