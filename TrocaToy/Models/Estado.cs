using System;
using System.Collections.Generic;

namespace TrocaToy.Models
{
    public partial class Estado
    {
        public Estado()
        {
            Endereco = new HashSet<Endereco>();
        }

        public string Codigo { get; set; }
        public string Descricao { get; set; }

        public virtual ICollection<Endereco> Endereco { get; set; }
    }
}
