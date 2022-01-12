using GestorAutonomo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;
using Newtonsoft.Json;

namespace GestorAutonomo.Repositories.Interface
{
    public interface IPontosEstoqueRepository
    {
        Task InserirAsync(PontosEstoque pontos);

        Task AtualizarAsync(PontosEstoque pontos);

        Task DeletarAsync(int Id);

        Task<PontosEstoque> SelecionarPorCodigoAsync(int? Id);
             
        Task<IPagedList<PontosEstoque>> ListarTodosRegistrosAsync( int? pagina, string pesquisa);

        Task<IEnumerable<PontosEstoque>> ListarTodosRegistrosAsync();

        PontosEstoque AjustarCampos(PontosEstoque pontos);



    }

   
}
