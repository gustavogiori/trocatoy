using Infrastructure.Business;
using TrocaToy.Models;
using TrocaToy.Repository;

namespace TrocaToy.Business
{
    /// <summary>
    /// Regra de negocio PropostaBusiness
    /// </summary>
    public class PropostaBusiness : BusinessBase<Proposta>, IPropostaBusiness
    {
        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="repository"></param>
        public PropostaBusiness(IPropostaRepository repository) : base(repository)
        {

        }
    }
}
