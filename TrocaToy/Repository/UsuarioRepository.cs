using Infrastructure.Security;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrocaToy.Models;

namespace TrocaToy.Repository
{
    /// <summary>
    /// Repositorio crud Usuario
    /// </summary>
    public class UsuarioRepository : Infrastructure.Repository<Models.Usuario>, IUsuarioRepository
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context"></param>
        public UsuarioRepository(DbContext context) : base(context)
        {

        }

        /// <summary>
        /// Insere novo usuário
        /// </summary>
        /// <param name="obj"></param>
        public override void Insert(Usuario obj)
        {
            obj.Senha = MD5Operation.GerarHashMd5(obj.Senha);
            obj.Regra = string.IsNullOrEmpty(obj.Regra) ? RegraUsuario.Usuario.ToString() : obj.Regra;

            string cpfValue = obj.Cpf;
            if (this.GetByCriteria(x => x.Cpf == obj.Cpf).Any())
            {
                throw new Exception("Já existe usuário com o cpf cadastrado!");
            }
            if (this.GetByCriteria(x => x.Email == obj.Email).Any())
            {
                throw new Exception("Já existe usuário com o email cadastrado!");
            }
            base.Insert(obj);
        }
    }
}
