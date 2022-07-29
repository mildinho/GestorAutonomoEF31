using GestorAutonomo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace GestorAutonomo.Repositories.Interface
{
    public interface IProdutoSaldoRepository : IGenericoRepository<ProdutoSaldo>
    {
        Task<IPagedList<ProdutoSaldo>> ListarTodosRegistrosAsync(int? pagina);

        Task<IEnumerable<ProdutoSaldo>> ListarTodosRegistrosAsync();

        Task<List<ProdutoSaldo>> ObterProdutosPorPonto(Guid Id, bool SomenteComSaldo);

        Task<List<ProdutoSaldo>> ObterPontosPorProduto(Guid Id, bool SomenteComSaldo);

        ProdutoSaldo AjustarCampos(ProdutoSaldo ProdutoSaldo);



    }
}
