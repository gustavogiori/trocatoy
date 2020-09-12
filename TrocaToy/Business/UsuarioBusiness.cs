﻿using Infrastructure;
using Infrastructure.Business;
using Infrastructure.Security;
using Infrastructure.Utils;
using Infrastructure.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public override Tuple<Usuario, ValidationModel> Insert(Usuario obj)
        {
            obj.Senha = MD5Operation.GerarHashMd5(obj.Senha);
            obj.Regra = string.IsNullOrEmpty(obj.Regra) ? RegraUsuario.Usuario.ToString() : obj.Regra;

            var validationModel = IsValid(obj);

            ValidaDados(obj, validationModel);

            GeraNovoGuid(obj);

            obj = SetIdUsuarioEndereco(obj);


            if (!validationModel.IsValid)
                return new Tuple<Usuario, ValidationModel>(obj, validationModel);

            return base.Insert(obj);
        }



        private void ValidaDados(Usuario obj, ValidationModel validationModel)
        {
            if (this.GetByCriteria(x => x.Cpf == obj.Cpf).Any())
            {
                validationModel.ErrorMessage?.Add("Já existe usuário com o cpf cadastrado!");
                validationModel.IsValid = false;
            }
            if (this.GetByCriteria(x => x.Email == obj.Email).Any())
            {
                validationModel.ErrorMessage?.Add("Já existe usuário com o email cadastrado!");
                validationModel.IsValid = false;
            }
            if (!Validacoes.IsValidCpf(obj.Cpf))
            {
                validationModel.ErrorMessage?.Add("CPF não é válido!");
                validationModel.IsValid = false;
            }
        }
    }
}