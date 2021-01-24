using Infrastructure.Business;
using System;
using TrocaToy.Models;

namespace TrocaToy.Business
{
    /// <summary>
    /// IPropostaBusiness
    /// </summary>
    public interface IPropostaBusiness : IBusinessBase<Proposta>
    {
        RespostaPropostas GetCustomItems();
        void AceitarProposta(Guid id);
        void RejeitarProposta(Guid id);
    }
}
