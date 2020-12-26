using Infrastructure.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrocaToy.Business.Interfaces;
using TrocaToy.Models;
using TrocaToy.Repository.Interfaces;

namespace TrocaToy.Business
{
    public class ImagensBusiness : BusinessBase<Imagens>, IImagensBusiness
    {
        public ImagensBusiness(IImagensRepository brinquedoRepository) : base(brinquedoRepository)
        {
        }
    }
}
