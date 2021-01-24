using Infrastructure;
using Infrastructure.Filter;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrocaToy.Business.Interfaces;
using TrocaToy.Models;

namespace TrocaToy.Repository
{
    /// <summary>
    /// Repositorio para criação de brinquedos
    /// </summary>
    public class BrinquedoRepository : Repository<Brinquedo>, IBrinquedoRepository
    {
        private readonly IAcessoBusiness _acessoBusiness;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="acessoBusiness"></param>
        public BrinquedoRepository(DbContext context, IAcessoBusiness acessoBusiness) : base(context)
        {
            _acessoBusiness = acessoBusiness;
        }

        public override IQueryable<Brinquedo> GetTable()
        {
            return base.GetTable().Include(x => x.Imagens);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="countPages"></param>
        /// <returns></returns>
        public override IEnumerable<Brinquedo> GetAll(PaginationFilter filter, out int countPages)
        {
            return base.GetAll(filter, out countPages).Where(x => x.IdUsuario == _acessoBusiness.IdUsuarioLogado());
        }
    }
}
