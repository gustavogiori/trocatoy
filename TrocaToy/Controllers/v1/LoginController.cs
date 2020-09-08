using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using TrocaToy.Models;
using TrocaToy.Repository;
using TrocaToy.Security;

namespace TrocaToy.Controllers.v1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/account")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        IUsuarioRepository _usuarioRepository;
        public LoginController(IUsuarioRepository usuarioRepository)
        {
            this._usuarioRepository = usuarioRepository;
        }

        // POST api/v1/login
        /// <summary>
        /// Realiza login na aplicação
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Um novo item criado</returns>
        /// <response code="200">Retorna quando o usuário e senha está correto, e retorna também o token para autenticação</response>
        /// <response code="404">Retorna se o usuário ou senha estiverem errados.</response>    
        [HttpPost]
        [Route("login")]
        public ActionResult<dynamic> Authenticate(UsuarioLogin model)
        {
            var userModel = model;
            // Recupera o usuário
            var user = _usuarioRepository.GetByCriteria(x => x.Email == userModel.Email && x.Senha == MD5Operation.GerarHashMd5(userModel.Senha)).FirstOrDefault();

            // Verifica se o usuário existe
            if (user == null)
                return NotFound(new { message = "Usuário ou senha inválidos" });

            // Gera o Token
            var token = TokenService.GenerateToken(user);

            // Oculta a senha
            user.Senha = "";

            // Retorna os dados
            return new
            {
                user = user,
                token = token
            };
        }
    }
}
