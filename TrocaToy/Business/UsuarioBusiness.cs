using Infrastructure;
using Infrastructure.Business;
using Infrastructure.Models;
using Infrastructure.Security;
using Infrastructure.Utils;
using Infrastructure.Validation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.Dependencies;
using System.Web.Mvc;
using TrocaToy.Models;
using TrocaToy.Repository;

namespace TrocaToy.Business
{
    /// <summary>
    /// UsuarioBusiness
    /// </summary>
    public class UsuarioBusiness : BusinessBase<Usuario>, IUsuarioBusiness
    {
        /// <summary>
        /// UsuarioBusiness
        /// </summary>
        /// <param name="repository"></param>
        public UsuarioBusiness(IUsuarioRepository repository) : base(repository)
        {

        }
        private Usuario SetIdUsuarioEndereco(Usuario usuario)
        {
            foreach (Endereco endereco in usuario.Endereco)
            {
                if (GConvert.IsGuidEmpty(endereco.Id))
                {
                    endereco.IdUsuario = usuario.Id;
                    endereco.Id = Guid.NewGuid();
                }
            }
            return usuario;
        }
        /// <summary>
        /// Inserindo dados
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override ValidationModel Insert(Usuario obj)
        {
            var validationModel = IsValid(obj);
            validationModel = ValidaDados(obj, validationModel);
            if (!validationModel.IsValid)
                return validationModel;
            obj.Senha = MD5Operation.GerarHashMd5(obj.Senha);
            obj.Regra = string.IsNullOrEmpty(obj.Regra) ? ((int)NivelPermissaoEnum.User).ToString() : obj.Regra;
            GeraNovoGuid(obj);

            obj = SetIdUsuarioEndereco(obj);
            return base.Insert(obj);
        }
        public override IEnumerable<Usuario> GetAll()
        {
            return base.GetAll();
        }

        public override void Delete(Guid id)
        {
            base.Delete(id);
        }

        private ValidationModel ValidaDados(Usuario obj, ValidationModel validationModel)
        {
            if (!CpfValido(obj))
            {
                validationModel = GeraMsgErro("CPF não é valido!", validationModel);
            }
            if (EmailJaCadastrado(obj))
            {
                validationModel = GeraMsgErro("Já existe usuário com o email cadastrado!", validationModel);
            }
            if (CPFJaCadastrado(obj))
            {
                validationModel = GeraMsgErro("Já existe usuário com o cpf cadastrado!", validationModel);
            }
            return validationModel;
        }

        private ValidationModel GeraMsgErro(string msgErro, ValidationModel validationModel)
        {
            validationModel.ErrorMessage?.Add(msgErro);
            validationModel.IsValid = false;
            return validationModel;
        }

        private bool CpfValido(Usuario obj)
        {
            return Validacoes.IsValidCpf(obj.Cpf);
        }

        private bool EmailJaCadastrado(Usuario obj)
        {
            return this.GetByCriteria(x => x.Email == obj.Email).Any();
        }

        private bool CPFJaCadastrado(Usuario obj)
        {
            return this.GetByCriteria(x => x.Cpf == obj.Cpf).Any();
        }
    }
}
