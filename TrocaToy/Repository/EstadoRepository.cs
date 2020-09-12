using Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrocaToy.Models;

namespace TrocaToy.Repository
{
    /// <summary>
    /// EstadoRepository
    /// </summary>
    public class EstadoRepository : Repository<Estado>, IEstadoRepository
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context"></param>
        public EstadoRepository(DbContext context) : base(context)
        {

        }
    }
}
