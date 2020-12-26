using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using GraphQL.Client;
using GraphQL.Common.Request;
using Infrastructure.Filter;
using Infrastructure.Helpers;
using Infrastructure.Json;
using Infrastructure.Query;
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
    /// Controler brinquedos
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]

    public class BrinquedosController : BaseController
    {
        IBrinquedoBusiness _brinquedoBusiness;
        IUsuarioBusiness _usuarioBusiness;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context"></param>
        /// <param name="unitOfWork"></param>
        /// <param name="uriService"></param>
        /// <param name="brinquedoBusiness"></param>
        public BrinquedosController(DbContext context, IUnitOfWork unitOfWork, IUriService uriService, IBrinquedoBusiness brinquedoBusiness, IUsuarioBusiness usuarioBusiness) : base(context, unitOfWork, uriService)
        {
            _brinquedoBusiness = brinquedoBusiness;
            _usuarioBusiness = usuarioBusiness;
        }

        // GET: api/Brinquedos
        /// <summary>
        /// Retornar todos os brinquedos
        /// </summary>
        /// <returns>Lista de brinquedos</returns>
        /// <response code="200">Retorna lista com todos brinquedos</response>
        /// <response code="401">Retorna quando não estiver autenticado.</response>
        [HttpGet]
        public ActionResult<PagedResponse<List<Brinquedo>>> GetBrinquedo([FromQuery] PaginationFilter filter)
        {
            var route = Request.Path.Value;
            int countPages = 0;
            var pagedData = _brinquedoBusiness.GetAll(filter, out countPages).ToList();
            PagedResponse<List<Brinquedo>> pagedReponse = PaginationHelper.CreatePagedReponse(pagedData, filter, countPages, _uriService, route);

            return Ok(pagedReponse);
        }

        /// <summary>
        /// Retorna brinquedo conforme Filtro utilizando um campo e o valor desejado
        /// </summary>
        /// <returns>Brinquedo conforme filtro</returns>
        /// <response code="200">Retorna o brinquedo conforme ID</response>
        /// <response code="404">Retorna quando não tiver encontrado.</response>
        [HttpGet, Route("GetBrinquedoCriteria")]
        public ActionResult<List<Brinquedo>> GetBrinquedoCriteria(string campo, string valor, [FromQuery] PaginationFilter filter)
        {
            var route = Request.Path.Value;
            int countPages = 0;
            var pagedData = _brinquedoBusiness.GetByCriteria(QueryService<Brinquedo>.GetCriteria(campo, valor), filter, out countPages).ToList();
            PagedResponse<List<Brinquedo>> pagedReponse = PaginationHelper.CreatePagedReponse(pagedData, filter, countPages, _uriService, route);

            return Ok(pagedReponse);
        }

        /// <summary>
        /// Retorna brinquedo conforme ID
        /// </summary>
        /// <returns>Brinquedo conforme id</returns>
        /// <response code="200">Retorna o brinquedo conforme ID</response>
        /// <response code="404">Retorna quando não tiver encontrado.</response>
        [HttpGet("{id}")]
        public ActionResult<Brinquedo> GetBrinquedo(Guid id)
        {
            var brinquedo = _brinquedoBusiness.GetById(id);
            if (brinquedo == null)
                return NotFound();

            return Ok(brinquedo);
        }

        /// Put api/v1/brinquedos/id
        /// <summary>
        /// Altera dados do brinquedo
        /// </summary>
        /// <returns>Brinquedo</returns>
        /// <response code="204">Retorna se o brinquedo foi alterado com sucesso</response>
        /// <response code="400">Retorna se houve algum erro na alteração do usuário.</response>
        /// <response code="404">Retorna se o brinquedo não foi encontrado.</response>
        /// <response code="401">Retorna quando não estiver autenticado.</response>
        [HttpPut("{id}")]
        [Authorize]
        public ActionResult<Brinquedo> PutBrinquedo(Guid id, Brinquedo brinquedo)
        {
            if (id != brinquedo.Id)
            {
                return BadRequest();
            }

            try
            {
                var result = _brinquedoBusiness.Update(brinquedo);
                if (result.IsValid)
                {
                    _unitOfWork.Commit();

                    return CreatedAtAction("GetBrinquedo", new { id = brinquedo.Id }, brinquedo);
                }

                _unitOfWork.Rollback();
                return BadRequest(result.ErrorMessage);
            }
            catch (DbUpdateConcurrencyException)
            {
                _unitOfWork.Rollback();

                if (!BrinquedoExists(id))
                {
                    return NotFound();
                }
                else
                {

                    throw;
                }
            }
        }

        /// Put api/v1/brinquedos/id
        /// <summary>
        /// Adicionar novo brinquedo
        /// </summary>
        /// <returns>Brinquedo</returns>
        /// <response code="201">Retorna se o brinquedo foi criado com sucesso</response>
        /// <response code="400">Retorna se houve algum erro na criação do brinquedo.</response>
        [HttpPost]
        public ActionResult<Brinquedo> PostBrinquedo([FromBody] Brinquedo json)
        {
            Brinquedo brinquedo = new Brinquedo();
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                var teste = identity.Claims.FirstOrDefault().Value;
                json.IdUsuario = _usuarioBusiness.GetByCriteria(x => x.Email == teste).FirstOrDefault().Id;
                brinquedo = JsonService<Brinquedo>.GetObject(json);

                var result = _brinquedoBusiness.Insert(brinquedo);

                if (result.IsValid)
                {
                    _unitOfWork.Commit();
                    return CreatedAtAction("GetBrinquedo", new { id = brinquedo.Id }, brinquedo);
                }

                _unitOfWork.Rollback();
                return BadRequest(result.ErrorMessage);
            }
            catch (DbUpdateException ex)
            {
                _unitOfWork.Rollback();

                if (BrinquedoExists(brinquedo.Id))
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

        /// Delete api/v1/brinquedos/id
        /// <summary>
        /// Deleta dados do brinquedo
        /// </summary>
        /// <returns>Lista de usuários</returns>
        /// <response code="200">Retorna se o brinquedo foi deletado com sucesso.</response>
        /// <response code="409">Retorna se houve algum erro na deleção </response>
        /// <response code="404">Retorna se o brinquedo não foi encontrado.</response>
        /// <response code="401">Retorna quando não estiver autenticado.</response>
        [HttpDelete("{id}")]
        [Authorize]
        public ActionResult<Usuario> DeleteBrinquedo(Guid id)
        {
            if (!BrinquedoExists(id))
            {
                return NotFound();
            }

            try
            {
                _brinquedoBusiness.Delete(id);
                _unitOfWork.Commit();
                return Ok(id);
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                return Conflict(ex.Message);
            }
        }

        private bool BrinquedoExists(Guid id)
        {
            return _brinquedoBusiness.GetById(id) != null;
        }
    }
}
