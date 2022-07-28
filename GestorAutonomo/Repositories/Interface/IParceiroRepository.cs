using GestorAutonomo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;
using Newtonsoft.Json;

namespace GestorAutonomo.Repositories.Interface
{
    public interface IParceiroRepository : IGenericoRepository<Parceiro>
    {
    
        Task<Parceiro> SelecionarPorCNPJ_CPFAsync(double documento);

        Task<IPagedList<Parceiro>> ListarTodosRegistrosAsync(TipoParceiro tipo, int? pagina, string pesquisa);

        Task<IEnumerable<Parceiro>> ListarTodosRegistrosAsync(TipoParceiro tipo);

        Parceiro AjustarCampos(Parceiro parceiro);



    }

   
}
