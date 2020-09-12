using Infrastructure.Models;
using System;
using System.Collections.Generic;

namespace TrocaToy.Models
{
    public partial class Estado: EntityBase
    {
        public Estado()
        {
           
        }
        public string Codigo { get; set; }
        public string Descricao { get; set; }
    }
}
