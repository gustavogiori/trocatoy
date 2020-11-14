using Infrastructure.Business;
using Infrastructure.Filter;
using System.Collections.Generic;
using TrocaToy.Models;
using TrocaToy.Repository;

namespace TrocaToy.Business
{
    /// <summary>
    /// AnuncioBusiness
    /// </summary>
    public class AnuncioBusiness : BusinessBase<Anuncio>, IAnuncioBusiness
    {
        /// <summary>
        /// AnuncioBusiness
        /// </summary>
        /// <param name="repository"></param>
        public AnuncioBusiness(IAnuncioRepository repository) : base(repository)
        {

        }
    }
}
