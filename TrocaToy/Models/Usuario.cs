using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TrocaToy.Models
{
    public partial class Usuario
    {
        public Usuario()
        {
            //Brinquedo = new HashSet<Brinquedo>();
            //Endereco = new HashSet<Endereco>();
            //PropostaIdUsuarioRequisitadoNavigation = new HashSet<Proposta>();
            //PropostaIdUsuarioSolicitanteNavigation = new HashSet<Proposta>();
        }

        public int Id { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Cpf { get; set; }
        [Required]
        public string Rg { get; set; }
        public string Telefone { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        public string Senha { get; set; }
        public string Regra { get; set; }
        //[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        //public virtual ICollection<Brinquedo> Brinquedo { get; set; }
        //[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        //public virtual ICollection<Endereco> Endereco { get; set; }
        //[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        //public virtual ICollection<Proposta> PropostaIdUsuarioRequisitadoNavigation { get; set; }
        //[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        //public virtual ICollection<Proposta> PropostaIdUsuarioSolicitanteNavigation { get; set; }
    }
}
