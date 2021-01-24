using Infrastructure.Business;
using System;
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

        public void AceitarProposta(Guid id)
        {
            ((IPropostaRepository)_repository).AceitarProposta(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual RespostaPropostas GetCustomItems()
        {
            return ((IPropostaRepository)_repository).GetCustomItems();
        }

        public void RejeitarProposta(Guid id)
        {
            ((IPropostaRepository)_repository).RejeitarProposta(id);
        }
    }
}
