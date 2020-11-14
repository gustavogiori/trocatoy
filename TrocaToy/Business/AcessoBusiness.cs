using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TrocaToy.Business.Interfaces;

namespace TrocaToy.Business
{
    public class AcessoBusiness : IAcessoBusiness
    {
        IHttpContextAccessor _httpContextAccessor;
        IUsuarioBusiness _usuarioBusiness;
        public AcessoBusiness(IHttpContextAccessor httpContextAccessor, IUsuarioBusiness usuarioBusiness)
        {
            _httpContextAccessor = httpContextAccessor;
            _usuarioBusiness = usuarioBusiness;
        }

        public Guid IdUsuarioLogado()
        {
            var identity = _httpContextAccessor.HttpContext.User.Identity as ClaimsIdentity;
            var emailLogado = identity.Claims.FirstOrDefault().Value;
            return _usuarioBusiness.GetByCriteria(x => x.Email == emailLogado).FirstOrDefault().Id;
        }
    }
}
