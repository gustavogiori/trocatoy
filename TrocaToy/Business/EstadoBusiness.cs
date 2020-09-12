using Infrastructure.Business;
using TrocaToy.Models;
using TrocaToy.Repository;

namespace TrocaToy.Business
{
    /// <summary>
    /// EstadoBusiness
    /// </summary>
    public class EstadoBusiness : BusinessBase<Estado>, IEstadoBusiness
    {
        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="estadoRepository"></param>
        public EstadoBusiness(IEstadoRepository estadoRepository) : base(estadoRepository)
        {

        }
    }
}
