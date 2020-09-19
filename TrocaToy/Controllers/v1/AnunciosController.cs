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

namespace TrocaToy.Controllers.v1
{
    /// <summary>
    /// Controle Anuncios
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class AnunciosController : BaseController
    {
        IAnuncioBusiness _anuncioBusiness;
        /// <summary>
        /// AnunciosController
        /// </summary>
        /// <param name="context"></param>
        /// <param name="unitOfWork"></param>
        /// <param name="uriService"></param>
        /// <param name="anuncioBusiness"></param>
        public AnunciosController(DbContext context, IUnitOfWork unitOfWork, IUriService uriService, IAnuncioBusiness anuncioBusiness) : base(context, unitOfWork, uriService)
        {
            _context = context;
            _anuncioBusiness = anuncioBusiness;
        }


        /// <summary>
        /// Retornar todas os anuncios com paginação
        /// </summary>
        /// <returns>Lista de anuncios</returns>
        /// <response code="200">Retorna lista com todos anuncios</response>
        /// <response code="401">Retorna quando não estiver autenticado.</response>
        [HttpGet]
        [Authorize]
        public ActionResult<PagedResponse<List<Brinquedo>>> GetAnuncios([FromQuery] PaginationFilter filter)
        {
            var route = Request.Path.Value;
            int countPages = 0;
            var pagedData = _anuncioBusiness.GetAll(filter, out countPages).ToList();
            PagedResponse<List<Anuncio>> pagedReponse = PaginationHelper.CreatePagedReponse(pagedData, filter, countPages, _uriService, route);

            return Ok(pagedReponse);
        }

        /// Get api/v1/anuncios/id
        /// <summary>
        /// Retorna anuncio conforme ID
        /// </summary>
        /// <returns>Lista de anuncios</returns>
        /// <response code="200">Retorna o anuncio conforme ID</response>
        /// <response code="401">Retorna quando não estiver autenticado.</response>
        [HttpGet("{id}")]
        [Authorize]
        public ActionResult<Estado> GetAnuncio(Guid id)
        {
            var anuncio = _anuncioBusiness.GetById(id);
            if (anuncio == null)
                return NotFound();

            return Ok(anuncio);
        }

        /// Put api/v1/anuncios/id
        /// <summary>
        /// Altera dados do anuncio
        /// </summary>
        /// <returns>Lista de usuários</returns>
        /// <response code="204">Retorna se o anuncio foi alterado com sucesso</response>
        /// <response code="400">Retorna se houve algum erro na alteração do anuncio.</response>
        /// <response code="404">Retorna se o anuncio não foi encontrado.</response>
        /// <response code="401">Retorna quando não estiver autenticado.</response>

        [HttpPut("{id}")]
        [Authorize]
        public ActionResult<Estado> PutAnuncio(Guid id, Anuncio anuncio)
        {
            if (id != anuncio.Id)
            {
                return BadRequest();
            }

            try
            {
                var result = _anuncioBusiness.Update(anuncio);

                if (result.IsValid)
                {
                    _unitOfWork.Commit();

                    return CreatedAtAction("GetAnuncio", new { id = anuncio.Id }, anuncio);
                }

                _unitOfWork.Rollback();
                return BadRequest(result.ErrorMessage);
            }
            catch (DbUpdateConcurrencyException)
            {
                _unitOfWork.Rollback();

                if (!AnuncioExists(id))
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
        /// Altera dados do anuncio
        /// </summary>
        /// <returns>Lista de anuncio</returns>
        /// <response code="201">Retorna se a anuncio foi criado com sucesso</response>
        /// <response code="400">Retorna se houve algum erro na criação da cidade.</response>
        [HttpPost]
        public ActionResult<Cidade> PostCidade([FromBody] Anuncio anuncio)
        {
            try
            {
                var result = _anuncioBusiness.Insert(anuncio);
                if (result.IsValid)
                {
                    _unitOfWork.Commit();
                    return CreatedAtAction("GetAnuncio", new { id = anuncio.Id }, anuncio);
                }

                _unitOfWork.Rollback();
                return BadRequest(result);
            }
            catch (DbUpdateException ex)
            {
                _unitOfWork.Rollback();

                if (AnuncioExists(anuncio.Id))
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
        /// Altera dados do anuncio
        /// </summary>
        /// <returns>Lista de usuários</returns>
        /// <response code="200">Retorna se o anuncio foi deletado com sucesso.</response>
        /// <response code="409">Retorna se houve algum erro na deleção do anuncio.</response>
        /// <response code="404">Retorna se o anuncio não foi encontrado.</response>
        /// <response code="401">Retorna quando não estiver autenticado.</response>
        [HttpDelete("{id}")]
        [Authorize]
        public ActionResult<Usuario> DeleteAnuncio(Guid id)
        {
            var anuncioExiste = AnuncioExists(id);

            if (!anuncioExiste)
            {
                return NotFound();
            }

            try
            {
                _anuncioBusiness.Delete(id);
                _unitOfWork.Commit();
                return Ok(id);
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                return Conflict(ex.Message);
            }
        }

        private bool AnuncioExists(Guid id)
        {
            return _anuncioBusiness.GetById(id) != null;
        }
    }
}

