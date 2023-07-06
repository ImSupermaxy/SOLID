using Alura.LeilaoOnline.WebApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Alura.LeilaoOnline.WebApp.Dados.EfCore
{
    public class CategoriaDaoComEfCore : ICategoriaDao
    {
        public AppDbContext _context;

        public CategoriaDaoComEfCore(AppDbContext context)
        {
            _context = context;
        }
       
        public Categoria BuscarPorId(int id)
        {
            return _context.Categorias.Include(c => c.Leiloes).Where(c => c.Id == id).FirstOrDefault();
        }

        public IEnumerable<Categoria> BuscarTodos()
        {
            return _context.Categorias.Include(c => c.Leiloes);
        }
    }
}
