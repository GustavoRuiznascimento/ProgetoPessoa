using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebAPIPessoa.Repository;
using WebAPIPessoaApplication.Pessoa;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PessoaController : ControllerBase
    {
        private readonly PessoaContext _context;
        public PessoaController(PessoaContext context)
        {
            _context = context;
        }
        public object DataNascimento { get; private set; }

        /// <summary>
        /// Rota responsavel por realizar processamento de dados de uma pessoa = Calcula idade - Calcula Imc - Calcula inss - Realiza conversao de Real para Dolar
        /// </summary>
        /// <returns>Retorna os dados processados da pessoa </returns>
        /// <response code="200">Retorna os dados processados com sucesso</response>
        /// <response code="400">Erro de validação </response>

        [HttpPost]
        [Authorize]
        public PessoaReponse ProcessarInformacoesPessoa([FromBody] PessoaRequest request)
        {
            var identidade = (ClaimsIdentity)HttpContext.User.Identity;
            var usuarioId = identidade.FindFirst("usuarioId").Value;

            var pessoaService = new PessoaService(_context);
            var pessoaResponse = pessoaService.ProcessarInformacoesPessoa(request, Convert.ToInt32(usuarioId));

            return pessoaResponse;
        }

        [HttpGet]
        [Authorize]
        public List<PessoaHistoricoResponse> ObterHistoricoPessoas()
        {
            var pessoaService = new PessoaService(_context);
            var pessoas = pessoaService.ObterHistoricoPessoas();

            return pessoas; 

        }

        [HttpGet]
        [Authorize]
        [Route("{id}")]

        public PessoaHistoricoResponse ObterHistoricoPessoa([FromRoute] int id)
        {
            var pessoaService = new PessoaService(_context);
            var pessoa = pessoaService.ObterHistoricoPessoa(id);

            return pessoa;
        }


    }
}
