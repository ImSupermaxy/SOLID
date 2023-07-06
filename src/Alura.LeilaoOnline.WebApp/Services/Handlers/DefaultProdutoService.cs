using Alura.LeilaoOnline.WebApp.Dados;
using Alura.LeilaoOnline.WebApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace Alura.LeilaoOnline.WebApp.Services.Handlers
{
    public class DefaultProdutoService : IProdutoService
    {
        ILeilaoDao _leilaoDao;
        ICategoriaDao _categoriaDao;

        public DefaultProdutoService(ILeilaoDao leilaoDao, ICategoriaDao categoriaDao)
        {
            _leilaoDao = leilaoDao;
            _categoriaDao = categoriaDao;
        }

        public Categoria ConsultaCategoriaPorIdComLeiloesEmPregao(int id)
        {
            return _categoriaDao.BuscarPorId(id);
        }

        public IEnumerable<CategoriaComInfoLeilao> ConsultaCategoriasComTotalDeLeiloesEmPregao()
        {
            return _categoriaDao
                 .BuscarTodos()
                 .Select(c => new CategoriaComInfoLeilao
                 {
                     Id = c.Id,
                     Descricao = c.Descricao,
                     Imagem = c.Imagem,
                     EmRascunho = c.Leiloes.Where(l => l.Situacao == SituacaoLeilao.Rascunho).Count(),
                     EmPregao = c.Leiloes.Where(l => l.Situacao == SituacaoLeilao.Pregao).Count(),
                     Finalizados = c.Leiloes.Where(l => l.Situacao == SituacaoLeilao.Finalizado).Count(),
                 });
        }

        public IEnumerable<Leilao> PesquisaLeilaoEmPregaoPorTermo(string termo)
        {
            var termoNormalized = termo.ToUpper();
            return _leilaoDao.BuscarTodos().Where(l => l.Titulo.ToUpper().Contains(termoNormalized) ||
            l.Descricao.ToUpper().Contains(termoNormalized) || 
            l.Categoria.Descricao.ToUpper().Contains(termoNormalized));            
        }
    }
}
