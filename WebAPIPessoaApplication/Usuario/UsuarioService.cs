using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebAPIPessoa.Repository;
using WebAPIPessoa.Repository.Models;

namespace WebAPIPessoaApplication.Usuario
{
    public class UsuarioService
    {
        private readonly PessoaContext _context;
        public UsuarioService(PessoaContext context)
        {
            _context = context; 
        }

        public bool InserirUsuario(UsuarioRequest request)
        {
            try
            {
                var usuario = new TabUsuario()
                {
                    nome = request.nome,
                    usuario = request.usuario,
                    senha = request.senha
                };

                _context.Usuarios.Add(usuario);
                _context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                 return false;
            }
        }

        public List<TabUsuario> ObterUsuarios()
        {
            try
            {
                var usuarios = _context.Usuarios.ToList();
                return usuarios;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public TabUsuario ObterUsuario(int id)
        {
            try
            {
                var usuario = _context.Usuarios.FirstOrDefault(x => x.id == id);
                return usuario;

            }
            catch (Exception ex)
            {
                return null;
            }
           
        }


        public bool AtualizarUsuario(int id, UsuarioRequest request)
        {
            try
            {
                var usuarioDb = _context.Usuarios.FirstOrDefault(x => x.id == id);
                if (usuarioDb == null)
                    return false;

                usuarioDb.nome = request.nome;
                usuarioDb.senha = request.senha;    
                usuarioDb.usuario = request.usuario;

                _context.Usuarios.Update(usuarioDb);
                _context.SaveChanges(); 

                return true;    
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool RemoverUsuario(int id)
        {
            try
            {
                var usuarioDb = _context.Usuarios.FirstOrDefault(x => x.id == id);
                if (usuarioDb == null)
                    return false;

                _context.Usuarios.Remove(usuarioDb);
                _context.SaveChanges();

                return true;
            }

            catch (Exception ex) 
            {
                return false;
            }
        }

    }
}
