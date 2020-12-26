using Infrastructure;
using Infrastructure.Filter;
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
    public class AnuncioRepository : Repository<Anuncio>, IAnuncioRepository
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">Context banco de dados</param>
        public AnuncioRepository(DbContext context) : base(context)
        {

        }
        /// <summary>
        /// GetTable
        /// </summary>
        /// <returns></returns>
        public override IQueryable<Anuncio> GetTable()
        {
            return base.GetTable().Include(x => x.Brinquedo).Include(x => x.Brinquedo.Imagens).Include(x => x.Endereco).Include(x => x.Usuario);
        }
    }
}
