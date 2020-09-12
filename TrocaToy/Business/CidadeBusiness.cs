using Infrastructure.Business;
using TrocaToy.Models;
using TrocaToy.Repository;

namespace TrocaToy.Business
{
    /// <summary>
    /// Camada de regra de negócio cidade
    /// </summary>
    public class CidadeBusiness : BusinessBase<Cidade>, ICidadeBusiness
    {
        /// <summary>
        /// UsuarioBusiness
        /// </summary>
        /// <param name="repository"></param>
        public CidadeBusiness(ICidadeRepository repository) : base(repository)
        {

        }
    }
}
