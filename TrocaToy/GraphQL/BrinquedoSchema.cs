using GraphQL;
using GraphQL.Types;

namespace TrocaToy.GraphQL
{
    /// <summary>
    /// Schema
    /// </summary>
    public class BrinquedoSchema : Schema
    {
        /// <summary>
        /// BrinquedoSchema
        /// </summary>
        /// <param name="resolver"></param>
        public BrinquedoSchema(IDependencyResolver resolver) : base(resolver)
        {
            Query = resolver.Resolve<BrinquedoQuery>();
        }
    }
}
