using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestorAutonomo.Repositories.Interface
{
    public interface IGenericoRepository<Tabela>where Tabela : class
    {

        Task InserirAsync(Tabela tabela);

        Task AtualizarAsync(Tabela tabela);

        Task DeletarAsync(Guid Id);

        Task<Tabela> SelecionarPorCodigoAsync(Guid Id);
    }
}
