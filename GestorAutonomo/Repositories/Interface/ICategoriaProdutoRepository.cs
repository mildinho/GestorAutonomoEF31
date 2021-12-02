using GestorAutonomo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace GestorAutonomo.Repositories.Interface
{
    public interface ICategoriaProdutoRepository
    {
        Task InserirAsync(CategoriaProduto categoriaProduto);

        Task AtualizarAsync(CategoriaProduto categoriaProduto);

        Task DeletarAsync(int Id);

        Task<CategoriaProduto> SelecionarPorCodigoAsync(int? Id);

        Task<IPagedList<CategoriaProduto>> ListarTodosRegistrosAsync(int? pagina);

        Task<IEnumerable<CategoriaProduto>> ListarTodosRegistrosAsync();

        CategoriaProduto AjustarCampos(CategoriaProduto categoriaProduto);



    }
}
