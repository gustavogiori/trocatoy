using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Filter;
using Infrastructure.Helpers;
using Infrastructure.Json;
using Infrastructure.Services;
using Infrastructure.UnitWork;
using Infrastructure.Wrappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrocaToy.Business;
using TrocaToy.Models;
using TrocaToy.Repository;

namespace TrocaToy.Controllers.v1
{
    /// <summary>
    /// Controle estado
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class EstadosController : BaseController
    {

        private readonly IEstadoBusiness _estadoBusiness;
        /// <summary>
        /// EstadosController
        /// </summary>
        /// <param name="context"></param>
        /// <param name="unitOfWork"></param>
        /// <param name="uriService"></param>
        /// <param name="estadoRepository"></param>
        public EstadosController(DbContext context, IUnitOfWork unitOfWork, IUriService uriService, IEstadoBusiness estadoRepository) : base(context, unitOfWork, uriService)
        {
            this._estadoBusiness = estadoRepository;
        }
        // Get api/v1/usuarios
        /// <summary>
        /// Retornar todos os estados
        /// </summary>
        /// <returns>Lista de usuários</returns>
        /// <response code="200">Retorna lista com todos estados</response>
        /// <response code="401">Retorna quando não estiver autenticado.</response>
        [HttpGet]
        [Authorize]
        public ActionResult<PagedResponse<List<Estado>>> GetEstados([FromQuery] PaginationFilter filter)
        {
            var route = Request.Path.Value;
            int countPages = 0;
            var pagedData = _estadoBusiness.GetAll(filter, out countPages).ToList();
            PagedResponse<List<Estado>> pagedReponse = PaginationHelper.CreatePagedReponse(pagedData, filter, countPages, _uriService, route);

            return Ok(pagedReponse);
        }

        /// Get api/v1/usuarios/id
        /// <summary>
        /// Retorna estado conforme ID
        /// </summary>
        /// <returns>Lista de usuários</returns>
        /// <response code="200">Retorna o estado conforme ID</response>
        /// <response code="401">Retorna quando não estiver autenticado.</response>
        [HttpGet("{id}")]
        [Authorize]
        public ActionResult<Estado> GetEstado(Guid id)
        {
            var estado = _estadoBusiness.GetById(id);
            if (estado == null)
                return NotFound();

            return Ok(estado);
        }

        /// Put api/v1/usuarios/id
        /// <summary>
        /// Altera dados do estado
        /// </summary>
        /// <returns>Lista de usuários</returns>
        /// <response code="204">Retorna se o estado foi alterado com sucesso</response>
        /// <response code="400">Retorna se houve algum erro na alteração do estado.</response>
        /// <response code="404">Retorna se o estado não foi encontrado.</response>
        /// <response code="401">Retorna quando não estiver autenticado.</response>

        [HttpPut("{id}")]
        [Authorize]
        public ActionResult<Estado> PutEstado(Guid id, Estado estado)
        {
            if (id != estado.Id)
            {
                return BadRequest();
            }

            try
            {
                var result = _estadoBusiness.Update(estado);

                if (result.IsValid)
                {
                    _unitOfWork.Commit();

                    return CreatedAtAction("GetEstado", new { id = estado.Id }, estado);
                }

                _unitOfWork.Rollback();
                return BadRequest(result);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _unitOfWork.Rollback();

                if (!EstadoExists(id))
                {
                    return NotFound();
                }
                else
                {

                    throw ex;
                }
            }
        }

        /// Put api/v1/usuarios/id
        /// <summary>
        /// Altera dados do estado
        /// </summary>
        /// <returns>Lista de estado</returns>
        /// <response code="201">Retorna se o usuário foi criado com sucesso</response>
        /// <response code="400">Retorna se houve algum erro na criação do estado.</response>
        [HttpPost]
        public ActionResult<Estado> PostEstado([FromBody] Estado json)
        {
            Estado estado = new Estado();
            try
            {
                estado = JsonService<Estado>.GetObject(json);
                var result = _estadoBusiness.Insert(estado);

                if (result.IsValid)
                {
                    _unitOfWork.Commit();
                    return CreatedAtAction("GetEstado", new { id = estado.Id }, estado);
                }

                _unitOfWork.Rollback();
                return BadRequest(result);
            }
            catch (DbUpdateException ex)
            {
                _unitOfWork.Rollback();

                if (EstadoExists(estado.Id))
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
        /// deleta dados do estado
        /// </summary>
        /// <returns>Lista de usuários</returns>
        /// <response code="200">Retorna se o usuário foi deletado com sucesso.</response>
        /// <response code="409">Retorna se houve algum erro na deleção do usuário.</response>
        /// <response code="404">Retorna se o usuário não foi encontrado.</response>
        /// <response code="401">Retorna quando não estiver autenticado.</response>
        [HttpDelete("{id}")]
        [Authorize]
        public ActionResult<Usuario> DeleteEstado(Guid id)
        {
            var usuarioExiste = EstadoExists(id);

            if (!usuarioExiste)
            {
                return NotFound();
            }

            try
            {
                _estadoBusiness.Delete(id);
                _unitOfWork.Commit();
                return Ok(id);
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                return Conflict(ex.Message);
            }
        }

        private bool EstadoExists(Guid id)
        {
            return _estadoBusiness.GetById(id) != null;
        }
    }
}
