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
    /// CidadeRepository
    /// </summary>
    public class CidadeRepository : Repository<Cidade>, ICidadeRepository
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context"></param>
        public CidadeRepository(DbContext context) : base(context)
        {

        }
    }
}
