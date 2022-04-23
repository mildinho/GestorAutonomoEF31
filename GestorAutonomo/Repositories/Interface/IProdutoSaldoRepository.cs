using GestorAutonomo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace GestorAutonomo.Repositories.Interface
{
    public interface IProdutoSaldoRepository
    {
        Task InserirAsync(ProdutoSaldo ProdutoSaldo);

        Task AtualizarAsync(ProdutoSaldo ProdutoSaldo);

        Task DeletarAsync(int Id);

        Task<ProdutoSaldo> SelecionarPorCodigoAsync(int? Id);

        Task<IPagedList<ProdutoSaldo>> ListarTodosRegistrosAsync(int? pagina);

        Task<IEnumerable<ProdutoSaldo>> ListarTodosRegistrosAsync();

        Task<List<ProdutoSaldo>> ObterProdutosPorPonto(int Id, bool SomenteComSaldo);
        
        Task<List<ProdutoSaldo>> ObterPontosPorProduto(int Id, bool SomenteComSaldo);

        ProdutoSaldo AjustarCampos(ProdutoSaldo ProdutoSaldo);



    }
}
