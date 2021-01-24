using Infrastructure;
using System;
using TrocaToy.Models;

namespace TrocaToy.Repository
{
    /// <summary>
    /// IPropostaRepository
    /// </summary>
    public interface IPropostaRepository : IRepository<Proposta>
    {
        RespostaPropostas GetCustomItems();
        void AceitarProposta(Guid id);
        void RejeitarProposta(Guid id);
    }
}
