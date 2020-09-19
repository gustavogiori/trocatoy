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
    /// AnuncioRepository
    /// </summary>
    public class AnuncioRepository: Repository<Anuncio>, IAnuncioRepository
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">Context banco de dados</param>
        public AnuncioRepository(DbContext context) : base(context)
        {

        }
    }
}
