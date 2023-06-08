using AutoMapper;
using ColegioAPI.Models;
using ColegioDomain.Entidades;
using ColegioDomain.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Text;
using System.Transactions;

namespace ColegioAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UsuarioController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly IUsuarioService _usuarioService;
        private readonly IMapper _mapper;


        public UsuarioController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IConfiguration configuration, IUsuarioService usuarioService, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _usuarioService = usuarioService;
            _mapper = mapper;
        }

        [HttpPost("Registrar")]
        [AllowAnonymous]
        public async Task<ActionResult<AutenticacionModel>> Registrar(CredencialesModel credenciales)
        {
            var Usuario = new IdentityUser
            {
                Email = credenciales.Email,
                UserName = credenciales.Email
            };

            var UsuarioCreado = await _userManager.CreateAsync(Usuario, credenciales.Password);

            if (UsuarioCreado.Succeeded)
            {
                return BuildToken(credenciales, "admin");
            }
            else
            {
                return BadRequest(UsuarioCreado.Errors);
            }
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<ActionResult<AutenticacionModel>> Login([FromBody] CredencialesModel credenciales)
        {
            try
            {
                var result = await _signInManager.PasswordSignInAsync(credenciales.Email, credenciales.Password, isPersistent: false, lockoutOnFailure: false);
                if (result.Succeeded)
                {

                    return BuildToken(credecialesUsuario: credenciales, "admin");
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CrearUsuario(UsuarioModel usuario)
        {
            try
            {
                var usuarioEntity = _mapper.Map<UsuarioModel, Usuario>(usuario);
                var result = await _usuarioService.CrearUsuario(usuarioEntity);

                return Ok(_mapper.Map<Usuario, UsuarioModel>(result));
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpPut]

        public async Task<IActionResult> ModificarUsuario(UsuarioModel usuario)
        {
            try
            {
                var usuarioEntity = _mapper.Map<UsuarioModel, Usuario>(usuario);
                var usuarioModificado = await _usuarioService.ActualizarUsuario(usuarioEntity);

                return Ok(_mapper.Map<Usuario, UsuarioModel>(usuarioModificado));
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]

        public async Task<IActionResult>EliminarUsuarioPorId(string id)
        {
            try
            {
                var guid = Guid.Parse(id);
                var result = await _usuarioService.EliminarUsuario(guid);
                return Ok(result);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpGet("ConsultarActivos")]

        public async Task<IActionResult>ConsultarActivos()
        {
            try
            {
                var usuarios = await _usuarioService.ObtenerUsuariosActivos();
                return Ok(usuarios);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpGet ("ConsultarPorNombre/{nombre}")]

        public async Task<IActionResult>ConsultarPorNombre(string nombre)
        {
            try
            {
                var usuarios = await _usuarioService.ObtenerUsuariosPorNombre(nombre);
                return Ok(usuarios);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpGet ("ConsultarPorRol/{rol}")]

        public async Task<IActionResult> ConsultarPorRol(string rol)
        {
            try
            {
                var usuarios = await _usuarioService.ObtenerUsuariosPorRol(rol);
                return Ok(usuarios);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        //Endpoint adicional
        [HttpGet("ConsultarInactivos")]

        public async Task<IActionResult> ConsultarInactivos()
        {
            try
            {
                var usuarios = await _usuarioService.ObtenerUsuariosInactivos();
                return Ok(usuarios);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        //Endpoint adicional
        [HttpGet("ConsultarPorId/{id}")]

        public async Task<IActionResult> ConsultarPorId(string id)
        {
            try
            {
                var usuario = await _usuarioService.ObtenerUsuarioPorId(id);
                if (usuario == null)
                    return NotFound();
                return Ok(_mapper.Map<Usuario, UsuarioModel>(usuario));
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        private AutenticacionModel BuildToken(CredencialesModel credecialesUsuario, string rol)
        {
            var claims = new List<Claim>()
            {
                new Claim("rol", rol)

            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["jwtKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expirationTime = DateTime.UtcNow.AddYears(1);

            var securityToken = new JwtSecurityToken(issuer: null, audience: null, claims: claims,
                expires: expirationTime, signingCredentials: creds);
            return new AutenticacionModel()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(securityToken),
                Expiration = expirationTime,
                Rol = rol
            };
        }
    }
}
