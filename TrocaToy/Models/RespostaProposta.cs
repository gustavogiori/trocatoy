using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrocaToy.Models
{
    public class RespostaProposta
    {
        public Guid Id { get; set; }
        public string BrinquedoSolicitado { get; set; }
        public string BrinquedoProposto { get; set; }
        public string Status { get; set; }
        public string NomePessoa { get; set; }
        public string Telefone { get; set; }

        public string TipoProposta { get; set; }
        public string Observacao { get; set; }
    }
}
