using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestorAutonomo.Domain.ServiceInterfaces
{
    public interface IGenericoServico<Tabela> where Tabela : class
    {

        Task<Tabela> BuscarPorCodigoAsync(Guid Id);
        Task<IEnumerable<Tabela>> TodosRegistrosPorEmpresaAsync(Guid EmpresaId);

    }
}
