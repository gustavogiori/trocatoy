using System;
using System.Collections.Generic;

namespace TrocaToy.Models
{
    public partial class ItensProposta
    {
        public int Id { get; set; }
        public int? IdBrinquedo { get; set; }
        public int? IdProposta { get; set; }

        public virtual Brinquedo IdBrinquedoNavigation { get; set; }
        public virtual Proposta IdPropostaNavigation { get; set; }
    }
}
