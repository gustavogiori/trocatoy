using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

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

        [ForeignKey("IdUsuario")]
        public virtual Usuario IdUsuarioNavigation { get; set; }

        public virtual ICollection<Anuncio> Anuncio { get; set; }
        public virtual ICollection<ItensProposta> ItensProposta { get; set; }
    }
}
