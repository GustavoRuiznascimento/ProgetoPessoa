using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OlaMundo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OlaMundoController : ControllerBase
    {

        public OlaMundoController(ILogger<OlaMundoController> logger)
        {
           
        }

        [HttpGet]
       
        public OlaMundo obterMensagem()
        {
            var retorno = new OlaMundo();
            retorno.Mensagem = "Integração do front com beck";
            return retorno;
        }
    }
}
