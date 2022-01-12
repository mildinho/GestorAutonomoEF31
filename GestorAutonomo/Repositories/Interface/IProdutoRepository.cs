using GestorAutonomo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;
using Newtonsoft.Json;

namespace GestorAutonomo.Repositories.Interface
{
    public interface IProdutoRepository
    {
        Task InserirAsync(Produto produto);

        Task AtualizarAsync(Produto produto);

        Task DeletarAsync(int Id);

        Task<Produto> SelecionarPorCodigoAsync(int? Id);
             
        Task<IPagedList<Produto>> ListarTodosRegistrosAsync( int? pagina, string pesquisa);

        Task<IEnumerable<Produto>> ListarTodosRegistrosAsync();

        Produto AjustarCampos(Produto produto);



    }

   
}
