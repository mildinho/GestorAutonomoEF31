using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestorAutonomo.Repositories.Interface
{
    public interface IGenericoRepository<Tabela>where Tabela : class
    {

        Task<Tabela> SelecionarPorCodigoAsync(int Id);
       
    }
}
