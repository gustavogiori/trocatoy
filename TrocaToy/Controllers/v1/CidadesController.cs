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
    public class CidadesController : BaseController
    {
        private readonly ICidadeBusiness _cidadeBusiness;
        /// <summary>
        /// EstadosController
        /// </summary>
        /// <param name="context"></param>
        /// <param name="unitOfWork"></param>
        /// <param name="uriService"></param>
        /// <param name="cidadeRepository"></param>
        public CidadesController(DbContext context, IUnitOfWork unitOfWork, IUriService uriService, ICidadeBusiness cidadeRepository) : base(context, unitOfWork, uriService)
        {
            this._cidadeBusiness = cidadeRepository;
        }

        // Get api/v1/usuarios
        /// <summary>
        /// Retornar todas as cidades
        /// </summary>
        /// <returns>Lista de usuários</returns>
        /// <response code="200">Retorna lista com todos cidades</response>
        [HttpGet]
        public ActionResult<PagedResponse<List<Cidade>>> GetCidades(int PageNumber, int PageSize)
        {
            PaginationFilter filter = new PaginationFilter(PageNumber, PageSize);
            var route = Request.Path.Value;
            int countPages = 0;
            var pagedData = _cidadeBusiness.GetAll(filter, out countPages).ToList();
            PagedResponse<List<Cidade>> pagedReponse = PaginationHelper.CreatePagedReponse(pagedData, filter, countPages, _uriService, route);

            return Ok(pagedReponse);
        }

        /// Get api/v1/usuarios/id
        /// <summary>
        /// Retorna cidaded conforme ID
        /// </summary>
        /// <returns>Lista de usuários</returns>
        /// <response code="200">Retorna o cidade conforme ID</response>
        /// <response code="401">Retorna quando não estiver autenticado.</response>
        [HttpGet("{id}")]
        [Authorize]
        public ActionResult<Estado> GetCidade(Guid id)
        {
            var cidade = _cidadeBusiness.GetById(id);
            if (cidade == null)
                return NotFound();

            return Ok(cidade);
        }

        /// Put api/v1/usuarios/id
        /// <summary>
        /// Altera dados do cidade
        /// </summary>
        /// <returns>Lista de usuários</returns>
        /// <response code="204">Retorna se o cidade foi alterado com sucesso</response>
        /// <response code="400">Retorna se houve algum erro na alteração do cidade.</response>
        /// <response code="404">Retorna se o cidade não foi encontrado.</response>
        /// <response code="401">Retorna quando não estiver autenticado.</response>

        [HttpPut("{id}")]
        [Authorize]
        public ActionResult<Estado> PutCidade(Guid id, Cidade cidade)
        {
            if (id != cidade.Id)
            {
                return BadRequest();
            }

            try
            {
                var result = _cidadeBusiness.Update(cidade);

                if (result.IsValid)
                {
                    _unitOfWork.Commit();

                    return CreatedAtAction("GetCidade", new { id = cidade.Id }, cidade);
                }

                _unitOfWork.Rollback();
                return BadRequest(result.ErrorMessage);
            }
            catch (DbUpdateConcurrencyException)
            {
                _unitOfWork.Rollback();

                if (!CidadeExists(id))
                {
                    return NotFound();
                }
                else
                {

                    throw;
                }
            }
        }

        /// Put api/v1/cidades/id
        /// <summary>
        /// Altera dados do cidade
        /// </summary>
        /// <returns>Lista de cidade</returns>
        /// <response code="201">Retorna se a cidade foi criado com sucesso</response>
        /// <response code="400">Retorna se houve algum erro na criação da cidade.</response>
        [HttpPost]
        public ActionResult<Cidade> PostCidade([FromBody] Cidade json)
        {
            Cidade cidade = new Cidade();
            try
            {
                cidade = JsonService<Cidade>.GetObject(json);

                var result = _cidadeBusiness.Insert(cidade);
                if (result.IsValid)
                {
                    _unitOfWork.Commit();
                    return CreatedAtAction("GetCidade", new { id = cidade.Id }, cidade);
                }

                _unitOfWork.Rollback();
                return BadRequest(result);
            }
            catch (DbUpdateException ex)
            {
                _unitOfWork.Rollback();

                if (CidadeExists(cidade.Id))
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
        /// Altera dados da cidade
        /// </summary>
        /// <returns>Lista de usuários</returns>
        /// <response code="200">Retorna se a cidade foi deletado com sucesso.</response>
        /// <response code="409">Retorna se houve algum erro na deleção da cidade.</response>
        /// <response code="404">Retorna se a cidade não foi encontrado.</response>
        /// <response code="401">Retorna quando não estiver autenticado.</response>
        [HttpDelete("{id}")]
        [Authorize]
        public ActionResult<Usuario> DeleteCidade(Guid id)
        {
            var cidadeExiste = CidadeExists(id);

            if (!cidadeExiste)
            {
                return NotFound();
            }

            try
            {
                _cidadeBusiness.Delete(id);
                _unitOfWork.Commit();
                return Ok(id);
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                return Conflict(ex.Message);
            }
        }

        private bool CidadeExists(Guid id)
        {
            return _cidadeBusiness.GetById(id) != null;
        }
    }
}
