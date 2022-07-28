using GestorAutonomo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace GestorAutonomo.Repositories.Interface
{
    public interface ICategoriaProdutoRepository : IGenericoRepository<CategoriaProduto>
    {

        Task<IPagedList<CategoriaProduto>> ListarTodosRegistrosAsync(int? pagina, string pesquisa);

        Task<List<CategoriaProduto>> ObterCategoriasPorCategoriaPai(int? Id);

        Task<List<Produto>> ObterProdutosPorCategoria(int Id);

        Task<IEnumerable<CategoriaProduto>> ListarTodosRegistrosAsync();

        CategoriaProduto AjustarCampos(CategoriaProduto categoriaProduto);



    }
}
