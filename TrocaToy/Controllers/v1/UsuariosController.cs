using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Json;
using Infrastructure.Services;
using Infrastructure.UnitWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using TrocaToy.Business;
using TrocaToy.Controllers.v1;
using TrocaToy.Models;
using TrocaToy.Repository;

namespace TrocaToy.Controllers
{
    /// <summary>
    /// Controle usuário
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]

    public class UsuariosController : BaseController
    {
        IUsuarioBusiness _usuarioBusiness;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context"></param>
        /// <param name="usuarioBusiness"></param>
        /// <param name="unitOfWork"></param>
        /// <param name="uriService"></param>
        public UsuariosController(DbContext context, IUnitOfWork unitOfWork, IUriService uriService, IUsuarioBusiness usuarioBusiness) : base(context, unitOfWork, uriService)
        {
            _usuarioBusiness = usuarioBusiness;
        }

        // Get api/v1/usuarios
        /// <summary>
        /// Retornar todos os usuários
        /// </summary>
        /// <returns>Lista de usuários</returns>
        /// <response code="200">Retorna lista com todos usuários</response>
        /// <response code="401">Retorna quando não estiver autenticado.</response>
        [HttpGet]
        [Authorize]
        public List<Usuario> GetUsuario()
        {
            return JsonService<List<Usuario>>.GetObject(_usuarioBusiness.GetAll().ToList());
        }

        /// Get api/v1/usuarios/id
        /// <summary>
        /// Retorna usuário conforme ID
        /// </summary>
        /// <returns>Lista de usuários</returns>
        /// <response code="200">Retorna o usuário conforme ID</response>
        /// <response code="401">Retorna quando não estiver autenticado.</response>
        [HttpGet("{id}")]
        [Authorize]
        public ActionResult<Usuario> GetUsuario(Guid id)
        {
            var usuario = _usuarioBusiness.GetById(id);
            if (usuario == null)
                return NotFound();

            return Ok(usuario);
        }
        private Usuario GetUsuarioComValorDeDadosNaoEditaveis(Usuario usuarioBanco, Usuario usuarioRequest)
        {
            usuarioRequest.Regra = usuarioBanco.Regra;
            return usuarioRequest;
        }
        /// Put api/v1/usuarios/id
        /// <summary>
        /// Altera dados do usuário
        /// </summary>
        /// <returns>Lista de usuários</returns>
        /// <response code="204">Retorna se o usuário foi alterado com sucesso</response>
        /// <response code="400">Retorna se houve algum erro na alteração do usuário.</response>
        /// <response code="404">Retorna se o usuário não foi encontrado.</response>
        /// <response code="401">Retorna quando não estiver autenticado.</response>

        [HttpPut("{id}")]
        [Authorize]
        public ActionResult<Usuario> PutUsuario(Guid id, Usuario usuario)
        {
            if (id != usuario.Id)
            {
                return BadRequest();
            }

            try
            {

                usuario = GetUsuarioComValorDeDadosNaoEditaveis(usuario, _usuarioBusiness.GetById(id));
                var result = _usuarioBusiness.Update(usuario);

                if (result.IsValid)
                {
                    _unitOfWork.Commit();

                    return CreatedAtAction("GetUsuario", new { id = usuario.Id }, usuario);
                }

                _unitOfWork.Rollback();
                return BadRequest(result);
            }
            catch (DbUpdateConcurrencyException)
            {
                _unitOfWork.Rollback();

                if (!UsuarioExists(id))
                {
                    return NotFound();
                }
                else
                {

                    throw;
                }
            }
        }

        /// Put api/v1/usuarios/id
        /// <summary>
        /// Altera dados do usuário
        /// </summary>
        /// <returns>Lista de usuários</returns>
        /// <response code="201">Retorna se o usuário foi criado com sucesso</response>
        /// <response code="400">Retorna se houve algum erro na criação do usuário.</response>
        [HttpPost]
        public ActionResult<Usuario> PostUsuario([FromBody] Usuario json)
        {
            Usuario usuario = new Usuario();
            try
            {
                usuario = JsonService<Usuario>.GetObject(json);
                var result = _usuarioBusiness.Insert(usuario);

                if (result.IsValid)
                {
                    _unitOfWork.Commit();
                    return CreatedAtAction("GetUsuario", new { id = usuario.Id }, usuario);
                }

                _unitOfWork.Rollback();
                return BadRequest(result);
            }
            catch (DbUpdateException ex)
            {
                _unitOfWork.Rollback();

                if (UsuarioExists(usuario.Id))
                {
                    return Conflict(ex.Message);
                }
                else
                {
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                return BadRequest(ex.Message);
            }
        }

        /// Delete api/v1/usuarios/id
        /// <summary>
        /// Altera dados do usuário
        /// </summary>
        /// <returns>Lista de usuários</returns>
        /// <response code="200">Retorna se o usuário foi deletado com sucesso.</response>
        /// <response code="409">Retorna se houve algum erro na deleção do usuário.</response>
        /// <response code="404">Retorna se o usuário não foi encontrado.</response>
        /// <response code="401">Retorna quando não estiver autenticado.</response>
        [HttpDelete("{id}")]
        [Authorize]
        public ActionResult<Usuario> DeleteUsuario(Guid id)
        {
            var usuarioExiste = UsuarioExists(id);

            if (!usuarioExiste)
            {
                return NotFound();
            }

            try
            {
                _usuarioBusiness.Delete(id);
                _unitOfWork.Commit();
                return Ok(id);
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                return Conflict(ex.Message);
            }
        }

        private bool UsuarioExists(Guid id)
        {
            return _usuarioBusiness.GetById(id) != null;
        }
    }
}
