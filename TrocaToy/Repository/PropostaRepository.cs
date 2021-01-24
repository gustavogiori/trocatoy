using Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrocaToy.Business.Interfaces;
using TrocaToy.Models;

namespace TrocaToy.Repository
{
    /// <summary>
    /// PropostaRepository
    /// </summary>
    public class PropostaRepository : Repository<Proposta>, IPropostaRepository
    {
        private readonly IAcessoBusiness _acessoBusiness;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="acessoBusiness"></param>
        public PropostaRepository(DbContext context, IAcessoBusiness acessoBusiness) : base(context)
        {
            _acessoBusiness = acessoBusiness;
        }
        public override Proposta Insert(Proposta obj)
        {
            obj.IdUsuarioSolicitante = _acessoBusiness.IdUsuarioLogado();
            return base.Insert(obj);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public RespostaPropostas GetCustomItems()
        {
            RespostaPropostas respostaPropostas = new RespostaPropostas();

            respostaPropostas.RecebidasPendente = GetRecebidasPedentes();
            respostaPropostas.EnviadasPendente = GetEnviadasPedentes();
            respostaPropostas.RecebidasConcluidas = GetRecebidasConcluidas();
            respostaPropostas.EnviadasConcluidas = GetEnviadasConcluidas();

            return respostaPropostas;
        }
        public override IQueryable<Proposta> GetTable()
        {
            return base.GetTable().Include(x => x.BrinquedoProposto).Include(x => x.BrinquedoRequerido).Include(x => x.UsuarioSolicitante);
        }
        public virtual void AceitarProposta(Guid id)
        {
            var proposta = GetById(id);
            proposta.Aceito = true;

            Update(proposta);

        }
        private List<RespostaProposta> GetEnviadasPedentes()
        {

            List<RespostaProposta> respostas = new List<RespostaProposta>();
            var recebidasPedente = GetAll().Where(x => x.IdUsuarioSolicitante == _acessoBusiness.IdUsuarioLogado() && x.Aceito == false && x.Rejeitada == false);

            foreach (var proposta in recebidasPedente)
            {
                var item = new RespostaProposta();
                item.BrinquedoProposto = GetNomeBrinquedo(proposta.BrinquedoProposto);
                item.BrinquedoSolicitado = GetNomeBrinquedo(proposta.BrinquedoRequerido);
                item.Status = "Pendente";
                item.TipoProposta = proposta.TipoProposta == 2 ? "Troca" : "Doação";
                item.NomePessoa = proposta.UsuarioSolicitante.Nome;
                item.Observacao = proposta.Observacao;
                item.Id = proposta.Id;

                respostas.Add(item);
            }

            return respostas;
        }
        private List<RespostaProposta> GetRecebidasPedentes()
        {

            List<RespostaProposta> respostas = new List<RespostaProposta>();
            var idUsuario = _acessoBusiness.IdUsuarioLogado();
            var recebidasPedente = GetAll().Where(x => x.BrinquedoRequerido.IdUsuario == idUsuario && x.Aceito == false && x.Rejeitada == false);

            foreach (var proposta in recebidasPedente)
            {
                var item = new RespostaProposta();
                item.BrinquedoProposto = proposta.IdUsuarioSolicitante.Equals(idUsuario) ? GetNomeBrinquedo(proposta.BrinquedoProposto) : GetNomeBrinquedo(proposta.BrinquedoRequerido);
                item.BrinquedoSolicitado = proposta.IdUsuarioSolicitante.Equals(idUsuario) ? GetNomeBrinquedo(proposta.BrinquedoRequerido) : GetNomeBrinquedo(proposta.BrinquedoProposto);
                item.Status = "Pendente";
                item.TipoProposta = proposta.TipoProposta == 2 ? "Troca" : "Doação";
                item.NomePessoa = proposta.UsuarioSolicitante.Nome;
                item.Observacao = proposta.Observacao;
                item.Id = proposta.Id;

                respostas.Add(item);
            }

            return respostas;
        }
        public string GetNomeBrinquedo(Brinquedo brinquedo)
        {
            if (brinquedo == null)
                return string.Empty;
            else
            {
                return brinquedo.Nome;
            }
        }
        private List<RespostaProposta> GetEnviadasConcluidas()
        {
            List<RespostaProposta> respostas = new List<RespostaProposta>();
            var recebidasPedente = GetAll().Where(x => x.IdUsuarioSolicitante == _acessoBusiness.IdUsuarioLogado() && x.Aceito == true || x.Rejeitada == true);

            foreach (var proposta in recebidasPedente)
            {
                var item = new RespostaProposta();
                item.BrinquedoProposto = GetNomeBrinquedo(proposta.BrinquedoProposto);
                item.BrinquedoSolicitado = GetNomeBrinquedo(proposta.BrinquedoRequerido);
                item.Status = proposta.Aceito ? "Aceito" : "Rejeitado";
                item.TipoProposta = proposta.TipoProposta == 2 ? "Troca" : "Doação";
                item.NomePessoa = proposta.UsuarioSolicitante.Nome;
                item.Observacao = proposta.Observacao;
                item.Telefone = proposta.Aceito ? proposta.UsuarioSolicitante.Telefone : string.Empty;

                item.Id = proposta.Id;

                respostas.Add(item);
            }

            return respostas;
        }
        private List<RespostaProposta> GetRecebidasConcluidas()
        {
            List<RespostaProposta> respostas = new List<RespostaProposta>();
            var idUsuario = _acessoBusiness.IdUsuarioLogado();
            var recebidasPedente = GetAll().Where(x => x.BrinquedoRequerido.IdUsuario == _acessoBusiness.IdUsuarioLogado() && x.Aceito == true || x.Rejeitada == true);

            foreach (var proposta in recebidasPedente)
            {
                var item = new RespostaProposta();
                item.BrinquedoProposto = proposta.IdUsuarioSolicitante.Equals(idUsuario) ? GetNomeBrinquedo(proposta.BrinquedoProposto) : GetNomeBrinquedo(proposta.BrinquedoRequerido);
                item.BrinquedoSolicitado = proposta.IdUsuarioSolicitante.Equals(idUsuario) ? GetNomeBrinquedo(proposta.BrinquedoRequerido) : GetNomeBrinquedo(proposta.BrinquedoProposto);
                item.Status = proposta.Aceito ? "Aceito" : "Rejeitado";
                item.TipoProposta = proposta.TipoProposta == 2 ? "Troca" : "Doação";
                item.NomePessoa = proposta.UsuarioSolicitante.Nome;
                item.Observacao = proposta.Observacao;
                item.Id = proposta.Id;

                respostas.Add(item);
            }

            return respostas;
        }

        public void RejeitarProposta(Guid id)
        {
            var proposta = GetById(id);
            proposta.Rejeitada = true;

            Update(proposta);
        }
    }
}
