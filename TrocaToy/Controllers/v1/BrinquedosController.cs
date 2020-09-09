﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Json;
using Infrastructure.Query;
using Infrastructure.UnitWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        IBrinquedoRepository _brinquedoRepository;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context"></param>
        /// <param name="unitOfWork"></param>
        /// <param name="brinquedoRepository"></param>
        public BrinquedosController(DbContext context, IUnitOfWork unitOfWork, IBrinquedoRepository brinquedoRepository) : base(context, unitOfWork)
        {
            _brinquedoRepository = brinquedoRepository;
        }

        // GET: api/Brinquedos
        /// <summary>
        /// Retornar todos os brinquedos
        /// </summary>
        /// <returns>Lista de brinquedos</returns>
        /// <response code="200">Retorna lista com todos brinquedos</response>
        /// <response code="401">Retorna quando não estiver autenticado.</response>
        [HttpGet]
        public List<Brinquedo> GetBrinquedo()
        {
            return JsonService<List<Brinquedo>>.GetObject(_brinquedoRepository.GetAll().ToList());
        }

        /// <summary>
        /// Retorna brinquedo conforme Filtro utilizando um campo e o valor desejado
        /// </summary>
        /// <returns>Brinquedo conforme filtro</returns>
        /// <response code="200">Retorna o brinquedo conforme ID</response>
        /// <response code="404">Retorna quando não tiver encontrado.</response>
        [HttpGet, Route("GetBrinquedoCriteria")]
        public ActionResult<List<Brinquedo>> GetBrinquedoCriteria(string campo, string valor)
        {
            var brinquedo = _brinquedoRepository.GetByCriteria(QueryService<Brinquedo>.GetCriteria(campo, valor));
            return Ok(brinquedo);
        }

        /// <summary>
        /// Retorna brinquedo conforme ID
        /// </summary>
        /// <returns>Brinquedo conforme id</returns>
        /// <response code="200">Retorna o brinquedo conforme ID</response>
        /// <response code="404">Retorna quando não tiver encontrado.</response>
        [HttpGet("{id}")]
        public ActionResult<Brinquedo> GetBrinquedo(int id)
        {
            var brinquedo = _brinquedoRepository.GetById(id);
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
        public ActionResult<Brinquedo> PutBrinquedo(int id, Brinquedo brinquedo)
        {
            if (id != brinquedo.Id)
            {
                return BadRequest();
            }

            try
            {
                ModelState.Clear();
                if (this.TryValidateModel(brinquedo))
                {
                    brinquedo = _brinquedoRepository.GetById(brinquedo.Id);
                    _brinquedoRepository.Update(brinquedo);
                    _unitOfWork.Commit();

                    return NoContent();
                }

                _unitOfWork.Rollback();
                return BadRequest(ModelState);
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
        /// Altera dados do binquedo
        /// </summary>
        /// <returns>Brinquedo</returns>
        /// <response code="201">Retorna se o brinquedo foi criado com sucesso</response>
        /// <response code="400">Retorna se houve algum erro na criação do usuário.</response>
        [HttpPost]
        [Authorize]
        public ActionResult<Brinquedo> PostBrinquedo([FromBody] Brinquedo json)
        {
            Brinquedo brinquedo = new Brinquedo();
            try
            {
                brinquedo = JsonService<Brinquedo>.GetObject(json);
                ModelState.Clear();
                if (this.TryValidateModel(brinquedo))
                {
                    _brinquedoRepository.Insert(brinquedo);
                    _unitOfWork.Commit();
                    return CreatedAtAction("GetBrinquedo", new { id = brinquedo.Id }, brinquedo);
                }

                _unitOfWork.Rollback();
                return BadRequest(ModelState);
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
        public ActionResult<Usuario> DeleteBrinquedo(int id)
        {
            if (!BrinquedoExists(id))
            {
                return NotFound();
            }

            try
            {
                _brinquedoRepository.Delete(id);
                _unitOfWork.Commit();
                return Ok(id);
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                return Conflict(ex.Message);
            }
        }

        private bool BrinquedoExists(int id)
        {
            return _brinquedoRepository.GetById(id) != null;
        }
    }
}