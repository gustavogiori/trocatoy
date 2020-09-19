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
    /// ItensPropostasController
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]

    public class ItensPropostasController : BaseController
    {
        IItensPropostaBusiness _itensPropostaBusiness;
        /// <summary>
        /// ItensPropostasController
        /// </summary>
        /// <param name="context"></param>
        /// <param name="unitOfWork"></param>
        /// <param name="uriService"></param>
        /// <param name="itensPropostaBusiness"></param>
        public ItensPropostasController(DbContext context, IUnitOfWork unitOfWork, IUriService uriService, IItensPropostaBusiness itensPropostaBusiness) : base(context, unitOfWork, uriService)
        {
            _itensPropostaBusiness = itensPropostaBusiness;
        }

        /// <summary>
        /// Retornar todos os itens de proposta com paginação
        /// </summary>
        /// <returns>Lista de items de proposta</returns>
        /// <response code="200">Retorna lista com todos itens de proposta</response>
        /// <response code="401">Retorna quando não estiver autenticado.</response>
        [HttpGet("{idProposta}")]
        [Authorize]
        public ActionResult<PagedResponse<List<ItensProposta>>> GetItensProposta([FromQuery] PaginationFilter filter, Guid idProposta)
        {
            var route = Request.Path.Value;
            int countPages = 0;
            var pagedData = _itensPropostaBusiness.GetByCriteria(x => x.IdProposta == idProposta, filter, out countPages).ToList();
            PagedResponse<List<ItensProposta>> pagedReponse = PaginationHelper.CreatePagedReponse(pagedData, filter, countPages, _uriService, route);

            return Ok(pagedReponse);
        }

        /// Get api/v1/anuncios/id
        /// <summary>
        /// Retorna item de proposta conforme ID
        /// </summary>
        /// <returns>Lista de anuncios</returns>
        /// <response code="200">Retorna o item de proposta conforme ID</response>
        /// <response code="401">Retorna quando não estiver autenticado.</response>
        [HttpGet("{id}")]
        [Authorize]
        public ActionResult<Anuncio> GetItensProposta(Guid id)
        {
            var itemproposta = _itensPropostaBusiness.GetById(id);
            if (itemproposta == null)
                return NotFound();

            return Ok(itemproposta);
        }

        /// Put api/v1/anuncios/id
        /// <summary>
        /// Altera dados do item da proposta
        /// </summary>
        /// <returns>Lista de usuários</returns>
        /// <response code="204">Retorna se o item de proposta foi alterado com sucesso</response>
        /// <response code="400">Retorna se houve algum erro na alteração do item de proposta.</response>
        /// <response code="404">Retorna se o item de proposta não foi encontrado.</response>
        /// <response code="401">Retorna quando não estiver autenticado.</response>

        [HttpPut("{id}")]
        [Authorize]
        public ActionResult<ItensProposta> PutItensProposta(Guid id, ItensProposta itensProposta)
        {
            if (id != itensProposta.Id)
            {
                return BadRequest();
            }

            try
            {
                var result = _itensPropostaBusiness.Update(itensProposta);

                if (result.IsValid)
                {
                    _unitOfWork.Commit();

                    return CreatedAtAction("GetAnuncio", new { id = itensProposta.Id }, itensProposta);
                }

                _unitOfWork.Rollback();
                return BadRequest(result.ErrorMessage);
            }
            catch (DbUpdateConcurrencyException)
            {
                _unitOfWork.Rollback();

                if (!ItemPropostaExists(id))
                {
                    return NotFound();
                }
                else
                {

                    throw;
                }
            }
        }

        /// Put api/v1/anuncios/id
        /// <summary>
        /// Adiciona dados do item da proposta
        /// </summary>
        /// <returns>Lista de anuncio</returns>
        /// <response code="201">Retorna se o item de proposta foi criado com sucesso</response>
        /// <response code="400">Retorna se houve algum erro na criação da cidade.</response>
        [HttpPost]
        public ActionResult<ItensProposta> PostItensProposta([FromBody] ItensProposta itensProposta)
        {
            try
            {
                var result = _itensPropostaBusiness.Insert(itensProposta);
                if (result.IsValid)
                {
                    _unitOfWork.Commit();
                    return CreatedAtAction("GetItensProposta", new { id = itensProposta.Id }, itensProposta);
                }

                _unitOfWork.Rollback();
                return BadRequest(result);
            }
            catch (DbUpdateException ex)
            {
                _unitOfWork.Rollback();

                if (ItemPropostaExists(itensProposta.Id))
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

        /// Delete api/v1/anuncios/id
        /// <summary>
        /// Deleta dados do item da proposta
        /// </summary>
        /// <returns>Lista de usuários</returns>
        /// <response code="200">Retorna se o item da proposta foi deletado com sucesso.</response>
        /// <response code="409">Retorna se houve algum erro na deleção do item da proposta.</response>
        /// <response code="404">Retorna se o item da proposta não foi encontrado.</response>
        /// <response code="401">Retorna quando não estiver autenticado.</response>
        [HttpDelete("{id}")]
        [Authorize]
        public ActionResult<Usuario> DeleteItensProposta(Guid id)
        {
            var itempropostaExiste = ItemPropostaExists(id);

            if (!itempropostaExiste)
            {
                return NotFound();
            }

            try
            {
                _itensPropostaBusiness.Delete(id);
                _unitOfWork.Commit();
                return Ok(id);
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                return Conflict(ex.Message);
            }
        }

        private bool ItemPropostaExists(Guid id)
        {
            return _itensPropostaBusiness.GetById(id) != null;
        }
    }
}


