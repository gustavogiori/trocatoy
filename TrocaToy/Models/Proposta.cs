using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrocaToy.Models
{
    public partial class Proposta : EntityBase
    {
        public Proposta()
        {

        }
        public bool Aceito { get; set; }
        public string Observacao { get; set; }
        public int TipoProposta { get; set; }
        public bool Rejeitada { get; set; }

        public Guid? IdBrinquedoProposto { get; set; }
        public Guid? IdBrinquedoRequerido { get; set; }

        public Guid IdUsuarioSolicitante { get; set; }

        [ForeignKey("IdBrinquedoProposto")]
        public Brinquedo BrinquedoProposto { get; set; }
        [ForeignKey("IdBrinquedoRequerido")]
        public Brinquedo BrinquedoRequerido { get; set; }

        [ForeignKey("IdUsuarioSolicitante")]
        public Usuario UsuarioSolicitante { get; set; }

    }
}
