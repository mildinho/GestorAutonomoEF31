using GestorAutonomo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace GestorAutonomo.Repositories.Interface
{
    public interface IParceiroRepository
    {
        Task InserirAsync(Parceiro parceiro);

        Task AtualizarAsync(Parceiro parceiro);

        Task DeletarAsync(int Id);

        Task<Parceiro> SelecionarPorCodigoAsync(int? Id);

        Task<IPagedList<Parceiro>> ListarTodosRegistrosAsync(int? pagina, string pesquisa);

        Task<IEnumerable<Parceiro>> ListarTodosRegistrosAsync();

        Parceiro AjustarCampos(Parceiro parceiro);



    }
}
