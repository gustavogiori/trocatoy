using Infrastructure.Models;
using System;
using System.Collections.Generic;

namespace TrocaToy.Models
{
    public partial class ItensProposta : EntityBase
    {
        public Guid IdBrinquedo { get; set; }
        public Guid IdProposta { get; set; }

        public virtual Brinquedo Brinquedo { get; set; }
        public virtual Proposta Proposta { get; set; }
    }
}
