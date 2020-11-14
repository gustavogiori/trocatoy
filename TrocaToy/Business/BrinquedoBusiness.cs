using Infrastructure.Business;
using Infrastructure.Validation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TrocaToy.Business.Interfaces;
using TrocaToy.Models;
using TrocaToy.Repository;

namespace TrocaToy.Business
{
    /// <summary>
    /// Camada de negócio
    /// </summary>
    public class BrinquedoBusiness : BusinessBase<Brinquedo>, IBrinquedoBusiness
    {
        IAcessoBusiness _acessoBusiness;
        /// <summary>
        /// Construção
        /// </summary>
        /// <param name="brinquedoRepository"></param>
        public BrinquedoBusiness(IBrinquedoRepository brinquedoRepository, IAcessoBusiness acessoBusiness) : base(brinquedoRepository)
        {
            _acessoBusiness = acessoBusiness;
        }
        public override ValidationModel Insert(Brinquedo obj)
        {
            obj.IdUsuario = _acessoBusiness.IdUsuarioLogado();
            return base.Insert(obj);
        }
    }
}
