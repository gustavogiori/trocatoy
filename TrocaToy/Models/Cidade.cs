using Infrastructure.Models;
using System;
using System.Collections.Generic;

namespace TrocaToy.Models
{
    public partial class Cidade : EntityBase
    {
        public Cidade()
        {

        }

        public string Codigo { get; set; }
        public string Descricao { get; set; }
        public Guid IdEstado { get; set; }
    }
}
