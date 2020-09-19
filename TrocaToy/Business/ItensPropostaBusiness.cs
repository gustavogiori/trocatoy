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
    /// ItemPropostaBusiness
    /// </summary>
    public class ItensPropostaBusiness : BusinessBase<ItensProposta>, IItensPropostaBusiness
    {
        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="repository"></param>
        public ItensPropostaBusiness(IItensPropostaRepository repository) : base(repository)
        {

        }
    }
}
