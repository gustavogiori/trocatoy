using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrocaToy.Models
{
    public partial class Brinquedo : EntityBase
    {
        public Brinquedo()
        {
        }
        public Guid IdUsuario { get; set; }
        public string Nome { get; set; }
        public string Marca { get; set; }
        public bool? Novo { get; set; }

        [ForeignKey("IdUsuario")]
        public virtual Usuario Usuario { get; set; }

        public ICollection<Imagens> Imagens { get; set; }

    }
}
