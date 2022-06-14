using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestorAutonomo.Repositories.Interface
{
    public interface IGenericoRepository<Tabela>where Tabela : class
    {

        Task InserirAsync(Tabela tabela);

        Task<Tabela> SelecionarPorCodigoAsync(int Id);

        Task DeletarAsync(int Id);
       
    }
}
