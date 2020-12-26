using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TrocaToy.Models
{
    public class Imagens : EntityBase
    {
        public Guid IdBrinquedo { get; set; }
        public string Url { get; set; }

        [ForeignKey("IdBrinquedo")]
        public virtual Brinquedo Brinquedo { get; set; }
    }
}
