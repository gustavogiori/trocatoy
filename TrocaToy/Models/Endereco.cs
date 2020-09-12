using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrocaToy.Models
{
    public partial class Endereco : EntityBase
    {
        public Endereco()
        {
        }
        public Guid? IdUsuario { get; set; }
        public Guid? IdEstado { get; set; }
        public Guid? IdCidade { get; set; }
        public string Rua { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Cep { get; set; }

        [ForeignKey("IdCidade")]
        public virtual Cidade Cidade { get; set; }
        [ForeignKey("IdEstado")]
        public virtual Estado Estado { get; set; }
        [ForeignKey("IdUsuario")]
        public virtual Usuario Usuario { get; set; }
    }
}
