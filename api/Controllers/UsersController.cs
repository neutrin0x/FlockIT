using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WebApi.Services;
using WebApi.Entities;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UsuariosController : ControllerBase
    {
        private IUsuarioService _UsuarioService;

        public UsuariosController(IUsuarioService Usuarioservice)
        {
            _UsuarioService = Usuarioservice;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]AuthenticateModel model)
        {
            var user = _UsuarioService.Authenticate(model.NombreUsuario, model.Password);

            if (user == null)
                return BadRequest(new { message = "Usuario y/o password incorrecto(s)." });

            return Ok(user);
        }

        [Authorize(Roles = Role.Admin)]
        [HttpGet]
        public IActionResult GetAll()
        {
            var Usuarios =  _UsuarioService.GetAll();
            return Ok(Usuarios);
        }

        [Authorize(Roles = Role.Admin)]
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            // Permitir SOLO a los admins acceder a registros de otros usuarios
            var currentUserId = int.Parse(User.Identity.Name);
            if (id != currentUserId && !User.IsInRole(Role.Admin))
                return Forbid();

            var user = _UsuarioService.GetById(id);

            if (user == null)
                return NotFound();

            return Ok(user);
        }
    }
}
