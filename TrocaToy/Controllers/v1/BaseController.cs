using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public BaseController(DbContext context, IUnitOfWork unitOfWork)
        {
            _context = context;
            _unitOfWork = unitOfWork;
        }
        /// <summary>
        /// Base de dados
        /// </summary>
        protected DbContext _context;
        /// <summary>
        /// Unidade de trabalho
        /// </summary>
        protected IUnitOfWork _unitOfWork;
    }
}