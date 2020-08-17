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
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly DbContext _context;
        IUsuarioRepository _usuarioRepository;
        IUnitOfWork _unitOfWork;
        public UsuariosController(DbContext context, IUsuarioRepository usuarioRepository, IUnitOfWork unitOfWork)
        {
            _context = context;
            _usuarioRepository = usuarioRepository;
            _unitOfWork = unitOfWork;
        }

        // GET: api/Usuarios
        [HttpGet]
        [Authorize]
        public string GetUsuario()
        {
            return JsonService<List<Usuario>>.GetJson(_usuarioRepository.GetAll().ToList());
        }

        // GET: api/Usuarios/5
        [HttpGet("{id}")]
        [Authorize]
        public ActionResult<Usuario> GetUsuario(int id)
        {
            var usuario = _usuarioRepository.GetById(id);
            if (usuario == null)
                return NotFound();

            return Ok(usuario);
        }

        // PUT: api/Usuarios/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
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

        // POST: api/Usuarios
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public ActionResult<Usuario> PostUsuario([FromBody] JObject json)
        {
            Usuario usuario = new Usuario();
            try
            {
                usuario = json.ToObject<Usuario>();
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
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

        }

        // DELETE: api/Usuarios/5
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
