using System;
using System.Collections.Generic;

namespace TrocaToy.Models
{
    public partial class Brinquedo
    {
        public Brinquedo()
        {
            Anuncio = new HashSet<Anuncio>();
            ItensProposta = new HashSet<ItensProposta>();
        }

        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public string Nome { get; set; }
        public string Marca { get; set; }
        public bool? Novo { get; set; }

        public virtual Usuario IdUsuarioNavigation { get; set; }
        public virtual ICollection<Anuncio> Anuncio { get; set; }
        public virtual ICollection<ItensProposta> ItensProposta { get; set; }
    }
}
