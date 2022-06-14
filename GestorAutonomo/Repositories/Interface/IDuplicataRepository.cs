using GestorAutonomo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;
using Newtonsoft.Json;

namespace GestorAutonomo.Repositories.Interface
{
    public interface IDuplicataRepository : IGenericoRepository<Duplicata>
    {
        Task InserirAsync(Duplicata duplicata);

        Task AtualizarAsync(Duplicata duplicata);

        Task DeletarAsync(int Id);

        Task<IPagedList<Duplicata>> ListarTodosRegistrosAsync(TipoDuplicata Tipo, int IdParceiro, int? pagina);

        Task<IEnumerable<Duplicata>> ListarTodosRegistrosAsync(TipoDuplicata Tipo, int IdParceiro);

        Duplicata AjustarCampos(Duplicata duplicata);



    }

   
}
