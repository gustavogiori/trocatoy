using Infrastructure;
using Microsoft.EntityFrameworkCore;
using TrocaToy.Models;

namespace TrocaToy.Repository
{
    /// <summary>
    /// ItensPropostaRepository
    /// </summary>
    public class ItensPropostaRepository : Repository<ItensProposta>, IItensPropostaRepository
    {
        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="context"></param>
        public ItensPropostaRepository(DbContext context) : base(context)
        {

        }
    }
}
