using Infrastructure.Models;
using System;
using System.Collections.Generic;

namespace TrocaToy.Models
{
    public partial class Proposta : EntityBase
    {
        public Proposta()
        {

        }

        public Guid IdUsuarioSolicitante { get; set; }
        public Guid IdUsuarioRequisitado { get; set; }
        public bool? Aceito { get; set; }
        public string Observacao { get; set; }

        public virtual Usuario UsuarioSolicitante { get; set; }
        public virtual Usuario UsuarioRequisitado { get; set; }

    }
}
