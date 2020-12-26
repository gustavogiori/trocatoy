using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Json;
using Infrastructure.Services;
using Infrastructure.UnitWork;
using Infrastructure.Wrappers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestSharp;
using TrocaToy.Business;
using TrocaToy.Business.Interfaces;
using TrocaToy.Models;

namespace TrocaToy.Controllers.v1
{
    /// <summary>
    /// Controler brinquedos
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]

    public class ImagensController : BaseController
    {
        IImagensBusiness _imagensBusiness;

        /// <summary>
        /// ImagensController
        /// </summary>
        /// <param name="context"></param>
        /// <param name="unitOfWork"></param>
        /// <param name="uriService"></param>
        /// <param name="imagensBusiness"></param>
        public ImagensController(DbContext context, IUnitOfWork unitOfWork, IUriService uriService, IImagensBusiness imagensBusiness) : base(context, unitOfWork, uriService)
        {
            _imagensBusiness = imagensBusiness;
        }
        [HttpGet("{idBrinquedo}")]
        public ActionResult<List<Imagens>> GetImagens(Guid idBrinquedo)
        {
            return Ok(_imagensBusiness.GetByCriteria(x => x.IdBrinquedo == idBrinquedo));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="arquivo"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Imagens>> PostImagensAsync([FromForm] Arquivo arquivo)
        {
            try
            {
                if (arquivo.file == null || arquivo.file.Length == 0)
                    throw new Exception("Imagem não enviada!");

                var folderName = Path.Combine("Resources", "filesPicture");
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                CreateDirectory(filePath);

                var uniqueFileName = $"{arquivo.idUser}_{arquivo.file.FileName}";
                var dbPath = Path.Combine(folderName, uniqueFileName);

                if (System.IO.File.Exists(dbPath))
                {
                    System.IO.File.Delete(dbPath);
                }
                await CreatePhisicalFile(arquivo, filePath, uniqueFileName);
                string base64 = GetBase64File(dbPath);

                Service.ImgurService imgurService = new Service.ImgurService();
                var urlImage = imgurService.UploadFile(base64);

                Imagens imagem = new Imagens { IdBrinquedo = arquivo.idUser, Url = urlImage };
                var result = _imagensBusiness.Insert(imagem);

                if (result.IsValid)
                {
                    _unitOfWork.Commit();
                    return imagem;
                }

                _unitOfWork.Rollback();
                return BadRequest(result);
            }
            catch (DbUpdateException ex)
            {
                _unitOfWork.Rollback();

                //if (EstadoExists(estado.Id))
                //{
                //    return Conflict(ex.Message);
                //}
                //else
                //{
                throw ex;
                //}
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                return BadRequest(ex.Message);
            }
        }

        private string GetBase64File(string dbPath)
        {
            var file = System.IO.File.OpenRead(dbPath);
            byte[] bytes;
            using (var memoryStream = new MemoryStream())
            {
                file.CopyTo(memoryStream);
                bytes = memoryStream.ToArray();
            }

            string base64 = Convert.ToBase64String(bytes);
            return base64;
        }

        private async Task CreatePhisicalFile(Arquivo arquivo, string filePath, string uniqueFileName)
        {
            using (var fileStream = new FileStream(Path.Combine(filePath, uniqueFileName), FileMode.Create))
            {
                await arquivo.file.CopyToAsync(fileStream);
            }
        }

        private void CreateDirectory(string filePath)
        {
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
        }
    }
    public class Arquivo
    {
        public IFormFile file { get; set; }
        public Guid idUser { get; set; }
    }
}

