using System;
using System.Collections.Generic;

namespace TrocaToy.Models
{
    public partial class Anuncio
    {
        public int Id { get; set; }
        public int IdBrinquedo { get; set; }
        public int IdEnderecoEntrega { get; set; }
        public int TipoDisponibilidade { get; set; }
        public string TelefoneContato { get; set; }
        public DateTime? DataAnuncio { get; set; }
        public bool? Encerrado { get; set; }

        public virtual Brinquedo IdBrinquedoNavigation { get; set; }
        public virtual Endereco IdEnderecoEntregaNavigation { get; set; }
    }
}
