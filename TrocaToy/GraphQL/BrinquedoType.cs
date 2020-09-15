using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrocaToy.Models;

namespace TrocaToy.GraphQL
{
    /// <summary>
    /// Tipo brinquedo
    /// </summary>
    public class BrinquedoType : ObjectGraphType<Brinquedo>
    {
        /// <summary>
        /// Construtor
        /// </summary>
        public BrinquedoType()
        {
            Field(m => m.Id, type: typeof(IdGraphType));
            Field(a => a.Nome);
            Field(a => a.Marca);
            Field(a => a.Novo, type: typeof(BooleanGraphType));
            Field(a => a.IdUsuario, type: typeof(IdGraphType));
        }
    }
}
