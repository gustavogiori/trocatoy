using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrocaToy.Models
{
    public class RespostaPropostas
    {
        public List<RespostaProposta> RecebidasPendente { get; set; }
        public List<RespostaProposta> EnviadasPendente { get; set; }
        public List<RespostaProposta> RecebidasConcluidas { get; set; }
        public List<RespostaProposta> EnviadasConcluidas { get; set; }
    }
}
