using Alura.LeilaoOnline.WebApp.Models;
using System.Collections.Generic;
using Alura.LeilaoOnline.WebApp.Dados;
using System;

namespace Alura.LeilaoOnline.WebApp.Services.Handlers
{
    public class DefaultAdminService : IAdminService
    {
        ILeilaoDao _leilaoDao;

        public DefaultAdminService(ILeilaoDao leilaoDao)
        {
            _leilaoDao = leilaoDao;
        }

        public void CadastraLeilao(Leilao leilao)
        {
            _leilaoDao.Incluir(leilao);
        }

        public IEnumerable<Categoria> ConsultaCategorias()
        {
            return _leilaoDao.BuscarCategorias();            
        }

        public IEnumerable<Leilao> ConsultaLeilao()
        {
            return _leilaoDao.BuscarLeiloes();
        }

        public Leilao ConsultaLeilaoPorId(int id)
        {
            return _leilaoDao.BuscarPorId(id);
        }
      
        public void IniciaPregaoDoLeilaoComId(int id)
        {
            var leilao = _leilaoDao.BuscarPorId(id);
            if (!(leilao != null && leilao.Situacao == SituacaoLeilao.Rascunho))
                return;
            leilao.Situacao = SituacaoLeilao.Pregao;
            leilao.Inicio = DateTime.Now;
            _leilaoDao.Alterar(leilao);
        }
        public void FinalizaPregaoDoLeilaoComId(int id)
        {
            var leilao = _leilaoDao.BuscarPorId(id);
            if (!(leilao != null && leilao.Situacao == SituacaoLeilao.Pregao))
                return;
            leilao.Situacao = SituacaoLeilao.Finalizado;
            leilao.Termino = DateTime.Now;
            _leilaoDao.Alterar(leilao);
        }

        public void ModificaLeilao(Leilao leilao)
        {
            _leilaoDao.Alterar(leilao);
        }      

        public void RemoveLeilao(Leilao leilao)
        {
            if (leilao == null || leilao.Situacao == SituacaoLeilao.Pregao)
                return;
            _leilaoDao.Excluir(leilao);
        }
    }
}
