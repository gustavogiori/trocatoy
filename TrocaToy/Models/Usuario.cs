using Infrastructure.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TrocaToy.Models
{
    public partial class Usuario : EntityBase
    {
        public Usuario()
        {
            Endereco = new HashSet<Endereco>();
        }
        [Required]
        public string Nome { get; set; }
        [Required]
        [MinLength(11)]
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

        public virtual ICollection<Endereco> Endereco { get; set; }

    }
}
