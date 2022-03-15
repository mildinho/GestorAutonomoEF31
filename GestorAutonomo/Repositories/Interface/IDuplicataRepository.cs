using GestorAutonomo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;
using Newtonsoft.Json;

namespace GestorAutonomo.Repositories.Interface
{
    public interface IDuplicataRepository
    {
        Task InserirAsync(Duplicata duplicata);

        Task AtualizarAsync(Duplicata duplicata);

        Task DeletarAsync(int Id);

        Task<Duplicata> SelecionarPorCodigoAsync(int? Id);
             
        Task<IPagedList<Duplicata>> ListarTodosRegistrosAsync( int? pagina, string pesquisa);

        Task<IEnumerable<Duplicata>> ListarTodosRegistrosAsync();

        Duplicata AjustarCampos(Duplicata duplicata);



    }

   
}
