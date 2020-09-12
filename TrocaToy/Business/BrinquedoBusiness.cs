using Infrastructure.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrocaToy.Models;
using TrocaToy.Repository;

namespace TrocaToy.Business
{
    /// <summary>
    /// Camada de negócio
    /// </summary>
    public class BrinquedoBusiness : BusinessBase<Brinquedo>, IBrinquedoBusiness
    {
        /// <summary>
        /// Construção
        /// </summary>
        /// <param name="brinquedoRepository"></param>
        public BrinquedoBusiness(IBrinquedoRepository brinquedoRepository) : base(brinquedoRepository)
        {

        }
    }
}
