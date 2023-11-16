using Microsoft.AspNetCore.Mvc;
using WebAPIPessoa.Repository;
using WebAPIPessoaApplication.Usuario;

namespace WebAPIPessoa.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly PessoaContext _context;
        public UsuarioController(PessoaContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult InserirUsuario([FromBody] UsuarioRequest request)
        {
            var usuarioService = new UsuarioService(_context);
            var sucesso = usuarioService.InserirUsuario(request);

            if (sucesso == true)
            {
                return NoContent();
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpGet]
        public IActionResult ObterUsuario()
        {
            var usuarioService = new UsuarioService(_context);
            var usuarios = usuarioService.ObterUsuarios();

            if (usuarios == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(usuarios);
            }
            return Ok(usuarios);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult ObterUsuario([FromRoute] int id)
        {
            var usuarioService = new UsuarioService(_context);
            var usuario = usuarioService.ObterUsuario(id);

            if (usuario == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(usuario);
            }
        }

        [HttpPut]
        [Route("{id}")]

        public IActionResult AtualizarUsuario([FromRoute] int id, [FromBody] UsuarioRequest request)
        {
            var UsuarioService = new UsuarioService(_context);
            var sucesso = UsuarioService.AtualizarUsuario(id, request);

            if (sucesso == true)
            {
                return NoContent();
            }
            else
            {
                return BadRequest();
            }
        }


        [HttpDelete]
        [Route("{id}")]
        public IActionResult RemoverUsuario([FromRoute]int id) 
        {
            var UsuarioService = new UsuarioService(_context);
            var sucesso = UsuarioService.RemoverUsuario(id);

            if (sucesso == true)
            {
                return NoContent();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
