using Infrastructure.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

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
   
   

        [ForeignKey("IdBrinquedo")]
        public virtual Brinquedo Brinquedo { get; set; }
        [ForeignKey("IdEnderecoEntrega")]
        public virtual Endereco Endereco { get; set; }
    }
}
