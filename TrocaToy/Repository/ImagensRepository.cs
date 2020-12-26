using Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrocaToy.Models;
using TrocaToy.Repository.Interfaces;

namespace TrocaToy.Repository
{
    public class ImagensRepository : Repository<Imagens>, IImagensRepository
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">Context banco de dados</param>
        public ImagensRepository(DbContext context) : base(context)
        {

        }
    }
}
