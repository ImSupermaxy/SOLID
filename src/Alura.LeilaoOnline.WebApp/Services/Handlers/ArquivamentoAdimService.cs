using Alura.LeilaoOnline.WebApp.Dados;
using Alura.LeilaoOnline.WebApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace Alura.LeilaoOnline.WebApp.Services.Handlers
{
    public class ArquivamentoAdimService : IAdminService
    {
        IAdminService _defaultAdmin;

        public ArquivamentoAdimService(ILeilaoDao leilaodao, ICategoriaDao categoriaDao)
        {
            _defaultAdmin = new DefaultAdminService(leilaodao, categoriaDao);
        }

        public void CadastraLeilao(Leilao leilao)
        {
            _defaultAdmin.CadastraLeilao(leilao);
        }

        public IEnumerable<Categoria> ConsultaCategorias()
        {
            return _defaultAdmin.ConsultaCategorias();
        }

        public IEnumerable<Leilao> ConsultaLeilao()
        {
            return _defaultAdmin.ConsultaLeilao().Where(l => l.Situacao != SituacaoLeilao.Arquivado);
        }

        public Leilao ConsultaLeilaoPorId(int id)
        {
            return _defaultAdmin.ConsultaLeilaoPorId(id);
        }

        public void FinalizaPregaoDoLeilaoComId(int id)
        {
            _defaultAdmin.FinalizaPregaoDoLeilaoComId(id);
        }

        public void IniciaPregaoDoLeilaoComId(int id)
        {
            _defaultAdmin.IniciaPregaoDoLeilaoComId(id);
        }

        public void ModificaLeilao(Leilao leilao)
        {
            _defaultAdmin.ModificaLeilao(leilao);
        }

        public void RemoveLeilao(Leilao leilao)
        {
            if (leilao != null && leilao.Situacao != SituacaoLeilao.Pregao)
            {
                leilao.Situacao = SituacaoLeilao.Arquivado;
                _defaultAdmin.ModificaLeilao(leilao);
            }
        }
    }
}
