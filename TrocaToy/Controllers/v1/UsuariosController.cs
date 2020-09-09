using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Json;
using Infrastructure.UnitWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
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
        IUsuarioRepository _usuarioRepository;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context"></param>
        /// <param name="usuarioRepository"></param>
        /// <param name="unitOfWork"></param>
        public UsuariosController(DbContext context, IUnitOfWork unitOfWork, IUsuarioRepository usuarioRepository) : base(context, unitOfWork)
        {
            _usuarioRepository = usuarioRepository;
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
            return JsonService<List<Usuario>>.GetObject(_usuarioRepository.GetAll().ToList());
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
        public ActionResult<Usuario> GetUsuario(int id)
        {
            var usuario = _usuarioRepository.GetById(id);
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
        public ActionResult<Usuario> PutUsuario(int id, Usuario usuario)
        {
            if (id != usuario.Id)
            {
                return BadRequest();
            }

            try
            {
                ModelState.Clear();
                if (this.TryValidateModel(usuario))
                {
                    usuario = GetUsuarioComValorDeDadosNaoEditaveis(usuario, _usuarioRepository.GetById(usuario.Id));
                    _usuarioRepository.Update(usuario);
                    _unitOfWork.Commit();

                    return NoContent();
                }

                _unitOfWork.Rollback();
                return BadRequest(ModelState);
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
                ModelState.Clear();
                if (this.TryValidateModel(usuario))
                {
                    _usuarioRepository.Insert(usuario);
                    _unitOfWork.Commit();
                    return CreatedAtAction("GetUsuario", new { id = usuario.Id }, usuario);
                }

                _unitOfWork.Rollback();
                return BadRequest(ModelState);
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
        public ActionResult<Usuario> DeleteUsuario(int id)
        {
            var usuarioExiste = UsuarioExists(id);

            if (!usuarioExiste)
            {
                return NotFound();
            }

            try
            {
                _usuarioRepository.Delete(id);
                _unitOfWork.Commit();
                return Ok(id);
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                return Conflict(ex.Message);
            }
        }

        private bool UsuarioExists(int id)
        {
            return _usuarioRepository.GetById(id) != null;
        }
    }
}
