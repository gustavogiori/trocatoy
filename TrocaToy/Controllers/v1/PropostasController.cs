using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Filter;
using Infrastructure.Helpers;
using Infrastructure.Services;
using Infrastructure.UnitWork;
using Infrastructure.Wrappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrocaToy.Business;
using TrocaToy.Models;

namespace TrocaToy.Controllers.v1
{
    /// <summary>
    /// Controler Proposta
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class PropostasController : BaseController
    {
        IPropostaBusiness _propostaBusiness;
        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="context"></param>
        /// <param name="unitOfWork"></param>
        /// <param name="uriService"></param>
        /// <param name="propostaBusiness"></param>
        public PropostasController(DbContext context, IUnitOfWork unitOfWork, IUriService uriService, IPropostaBusiness propostaBusiness) : base(context, unitOfWork, uriService)
        {
            _propostaBusiness = propostaBusiness;
        }

        /// <summary>
        /// Retornar todas as propostas com paginação
        /// </summary>
        /// <returns>Lista de anuncios</returns>
        /// <response code="200">Retorna lista com todos anuncios</response>
        /// <response code="401">Retorna quando não estiver autenticado.</response>
        [HttpGet]
        [Authorize]
        public ActionResult<PagedResponse<List<Proposta>>> GetPropostas([FromQuery] PaginationFilter filter)
        {
            var route = Request.Path.Value;
            int countPages = 0;
            var pagedData = _propostaBusiness.GetAll(filter, out countPages).ToList();
            PagedResponse<List<Proposta>> pagedReponse = PaginationHelper.CreatePagedReponse(pagedData, filter, countPages, _uriService, route);

            return Ok(pagedReponse);
        }

        /// Get api/v1/propostas/id
        /// <summary>
        /// Retorna proposta conforme ID
        /// </summary>
        /// <returns>Lista de anuncios</returns>
        /// <response code="200">Retorna a proposta conforme ID</response>
        /// <response code="401">Retorna quando não estiver autenticado.</response>
        [HttpGet("{id}")]
        [Authorize]
        public ActionResult<Estado> GetProposta(Guid id)
        {
            var proposta = _propostaBusiness.GetById(id);
            if (proposta == null)
                return NotFound();

            return Ok(proposta);
        }

        /// Put api/v1/proposta/id
        /// <summary>
        /// Altera dados da proposta
        /// </summary>
        /// <returns>Lista de usuários</returns>
        /// <response code="204">Retorna se a proposta foi alterado com sucesso</response>
        /// <response code="400">Retorna se houve algum erro na alteração da proposta.</response>
        /// <response code="404">Retorna se a proposta não foi encontrado.</response>
        /// <response code="401">Retorna quando não estiver autenticado.</response>

        [HttpPut("{id}")]
        [Authorize]
        public ActionResult<Proposta> PutProposta(Guid id, Proposta proposta)
        {
            if (id != proposta.Id)
            {
                return BadRequest();
            }

            try
            {
                var result = _propostaBusiness.Update(proposta);

                if (result.IsValid)
                {
                    _unitOfWork.Commit();

                    return CreatedAtAction("GetProposta", new { id = proposta.Id }, proposta);
                }

                _unitOfWork.Rollback();
                return BadRequest(result.ErrorMessage);
            }
            catch (DbUpdateConcurrencyException)
            {
                _unitOfWork.Rollback();

                if (!PropostaExists(id))
                {
                    return NotFound();
                }
                else
                {

                    throw;
                }
            }
        }

        /// Put api/v1/propostas/id
        /// <summary>
        /// Altera dados da proposta
        /// </summary>
        /// <returns>Lista de anuncio</returns>
        /// <response code="201">Retorna se a proposta foi criado com sucesso</response>
        /// <response code="400">Retorna se houve algum erro na criação da proposta.</response>
        [HttpPost]
        public ActionResult<Proposta> PostProposta([FromBody] Proposta proposta)
        {
            try
            {
                var result = _propostaBusiness.Insert(proposta);
                if (result.IsValid)
                {
                    _unitOfWork.Commit();
                    return CreatedAtAction("GetProposta", new { id = proposta.Id }, proposta);
                }

                _unitOfWork.Rollback();
                return BadRequest(result);
            }
            catch (DbUpdateException ex)
            {
                _unitOfWork.Rollback();

                if (PropostaExists(proposta.Id))
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

        /// Delete api/v1/propostas/id
        /// <summary>
        /// Altera dados do anuncio
        /// </summary>
        /// <returns>Lista de usuários</returns>
        /// <response code="200">Retorna se a proposta foi deletado com sucesso.</response>
        /// <response code="409">Retorna se houve algum erro na deleção da proposta.</response>
        /// <response code="404">Retorna se a proposta não foi encontrado.</response>
        /// <response code="401">Retorna quando não estiver autenticado.</response>
        [HttpDelete("{id}")]
        [Authorize]
        public ActionResult<Usuario> DeleteProposta(Guid id)
        {
            var propostaExiste = PropostaExists(id);

            if (!propostaExiste)
            {
                return NotFound();
            }

            try
            {
                _propostaBusiness.Delete(id);
                _unitOfWork.Commit();
                return Ok(id);
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                return Conflict(ex.Message);
            }
        }

        private bool PropostaExists(Guid id)
        {
            return _propostaBusiness.GetById(id) != null;
        }
    }
}
