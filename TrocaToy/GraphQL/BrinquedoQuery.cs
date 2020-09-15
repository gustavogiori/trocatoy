using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrocaToy.Business;

namespace TrocaToy.GraphQL
{
    /// <summary>
    /// Implementação GraphQL
    /// </summary>
    public class BrinquedoQuery : ObjectGraphType
    {
        /// <summary>
        /// BrinquedoQuery
        /// </summary>
        /// <param name="brinquedoBusiness"></param>
        public BrinquedoQuery(IBrinquedoBusiness brinquedoBusiness)
        {
            Field<ListGraphType<BrinquedoType>>(
                "brinquedos",
                resolve: context => brinquedoBusiness.GetAll()
                );
        }
    }
}
