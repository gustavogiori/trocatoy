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
    /// PropostaRepository
    /// </summary>
    public class PropostaRepository : Repository<Proposta>, IPropostaRepository
    {
        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="context"></param>
        public PropostaRepository(DbContext context) : base(context)
        {

        }
    }
}
