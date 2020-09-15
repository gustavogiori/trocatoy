using Infrastructure.Models;
using System;
using System.Collections.Generic;

namespace TrocaToy.Models
{
    public partial class Anuncio : EntityBase
    {
        public Guid IdBrinquedo { get; set; }
        public Guid IdEnderecoEntrega { get; set; }
        public int TipoDisponibilidade { get; set; }
        public string TelefoneContato { get; set; }
        public DateTime? DataAnuncio { get; set; }
        public bool? Encerrado { get; set; }

        public virtual Brinquedo IdBrinquedoNavigation { get; set; }
        public virtual Endereco IdEnderecoEntregaNavigation { get; set; }
    }
}
