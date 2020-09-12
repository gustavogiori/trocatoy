using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Models;
using Infrastructure.Security;
using Infrastructure.Wrappers;
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
            if (string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Senha))
            {
                return BadRequest(new Response<UsuarioLogin>() { Succeeded = false, Message = "É preciso preencher usuário e senha!" });
            }
            var teste = Guid.NewGuid();
            // Recupera o usuário
            var user = _usuarioRepository.GetByCriteria(x => x.Email == model.Email && x.Senha == MD5Operation.GerarHashMd5(model.Senha)).FirstOrDefault();

            // Verifica se o usuário existe
            if (user == null)
                return NotFound(new { message = "Usuário ou senha inválidos" });

            model.SetNivelPermissao(Convert.ToInt32(user.Regra));

            // Gera o Token
            var token = TokenService.GenerateToken(model);

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
