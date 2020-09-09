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
    /// Repositorio para criação de brinquedos
    /// </summary>
    public class BrinquedoRepository : Repository<Brinquedo>, IBrinquedoRepository
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">Context banco de dados</param>
        public BrinquedoRepository(DbContext context) : base(context)
        {

        }
    }
}
