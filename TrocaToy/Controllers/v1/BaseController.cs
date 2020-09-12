using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Services;
using Infrastructure.UnitWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace TrocaToy.Controllers.v1
{
    /// <summary>
    /// Controle base
    /// </summary>
    [ApiController]
    public class BaseController : ControllerBase
    {
        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="context"></param>
        /// <param name="unitOfWork"></param>
        /// <param name="uriService"></param>
        public BaseController(DbContext context, IUnitOfWork unitOfWork, IUriService uriService)
        {
            _context = context;
            _unitOfWork = unitOfWork;
            _uriService = uriService;

        }
        /// <summary>
        /// Base de dados
        /// </summary>
        protected DbContext _context;
        /// <summary>
        /// Unidade de trabalho
        /// </summary>
        protected IUnitOfWork _unitOfWork;

        /// <summary>
        /// Uri Service
        /// </summary>
        protected IUriService _uriService;
    }
}