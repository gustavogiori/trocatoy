using System;
using System.Collections.Generic;

namespace TrocaToy.Models
{
    public partial class Endereco
    {
        public Endereco()
        {
            Anuncio = new HashSet<Anuncio>();
        }

        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public string CodEstado { get; set; }
        public string CodCidade { get; set; }
        public string Rua { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Cep { get; set; }

        public virtual Cidade CodCidadeNavigation { get; set; }
        public virtual Estado CodEstadoNavigation { get; set; }
        public virtual Usuario IdUsuarioNavigation { get; set; }
        public virtual ICollection<Anuncio> Anuncio { get; set; }
    }
}
