using Infrastructure.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace TrocaToy.Models
{
    public partial class Anuncio : EntityBase
    {
        public Guid IdBrinquedo { get; set; }
        public Guid IdEnderecoEntrega { get; set; }
        public Guid IdUsuario { get; set; }
        public int TipoDisponibilidade { get; set; }
        public string TelefoneContato { get; set; }
        public DateTime? DataAnuncio { get; set; }
        public bool? Encerrado { get; set; }

        [NotMapped]
        public bool? brinquedoNovo
        {
            get
            {
                return Brinquedo?.Novo;
            }
        }

        [NotMapped]
        public string nomeBrinquedo
        {
            get
            {
                return Brinquedo?.Nome;
            }
        }
        [NotMapped]
        public string nomeUsuario
        {
            get
            {
                return Usuario?.Nome;
            }
        }

        public string UrlPrincipal
        {
            get
            {
                return Brinquedo?.Imagens.FirstOrDefault()?.Url;
            }
        }
        [ForeignKey("IdBrinquedo")]
        public virtual Brinquedo Brinquedo { get; set; }
        [ForeignKey("IdEnderecoEntrega")]
        public virtual Endereco Endereco { get; set; }
        [ForeignKey("IdUsuario")]
        public virtual Usuario Usuario { get; set; }

    }
}
