using System;
using System.Collections.Generic;

namespace TrocaToy.Models
{
    public partial class Proposta
    {
        public Proposta()
        {
            ItensProposta = new HashSet<ItensProposta>();
        }

        public int Id { get; set; }
        public int? IdUsuarioSolicitante { get; set; }
        public int? IdUsuarioRequisitado { get; set; }
        public bool? Aceito { get; set; }
        public string Observacao { get; set; }

        public virtual Usuario IdUsuarioRequisitadoNavigation { get; set; }
        public virtual Usuario IdUsuarioSolicitanteNavigation { get; set; }
        public virtual ICollection<ItensProposta> ItensProposta { get; set; }
    }
}
