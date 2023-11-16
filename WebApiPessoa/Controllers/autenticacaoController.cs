using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using WebAPIPessoa.Repository;
using WebAPIPessoaApplication.Autenticacao;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class autenticacaoController : ControllerBase
    {
        private readonly PessoaContext _context;
        public autenticacaoController(PessoaContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Login([FromBody] AltenticacaoRequest request)
        {
            var altenticacaoService = new AutenticacaoService(_context);
            var tokenString = altenticacaoService.Autenticar(request);

            if (string.IsNullOrWhiteSpace(tokenString))
            {
                return Unauthorized();
            }
            else
            {
                return Ok(new { token = tokenString });
            }
        }
    }
}
   