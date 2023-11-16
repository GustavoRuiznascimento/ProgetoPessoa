using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using WebAPIPessoa.Repository;
using WebAPIPessoa.Repository.Models;

namespace WebAPIPessoaApplication.Autenticacao
{
    public class AutenticacaoService
    {
        private readonly PessoaContext _context;
        public AutenticacaoService(PessoaContext context)
        { 
           _context = context;
        } 
        public string Autenticar(AltenticacaoRequest request)
        {
            var usuario = _context.Usuarios.FirstOrDefault(x => x.usuario == request.UserName && x.senha == request.Password);              
            if (usuario != null)
            {
                var tokenString = GerarTokenJwt(usuario);
                return tokenString;
            }
            else
            {
                return null;
            }
        }

        private string GerarTokenJwt(TabUsuario usuario)
        {
            var issuer = "var";
            var audience = "var";
            var key = "a4f5b7bf-cdd4-4d37-ae31-f2040a1066e1";

            var secwrityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(secwrityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
               new Claim("usuarioId", usuario.id.ToString()),
            };


            var token = new JwtSecurityToken(issuer: issuer, claims: claims, audience: audience, expires: DateTime.Now.AddMinutes(60), signingCredentials: credentials);
            var TokenHandler = new JwtSecurityTokenHandler();
            var stringToken = TokenHandler.WriteToken(token);
            return stringToken;
        }
    }
}
