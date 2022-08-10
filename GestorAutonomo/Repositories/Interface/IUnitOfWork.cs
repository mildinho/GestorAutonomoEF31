using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestorAutonomo.Repositories.Interface
{
    public interface IUnitOfWork : IDisposable
    {

        IBancoRepository Banco { get; }
        IUFRepository UF { get; }
        ILoginRepository Login { get; }
        IEmpresaRepository Empresa { get; }
        ICategoriaProdutoRepository CategoriaProduto { get; }
        IParceiroRepository Parceiro { get; }
        IPontosEstoqueRepository PontosEstoque { get; }
        IProdutoRepository Produto { get; }
        IProdutoSaldoRepository ProdutoSaldo { get; }
        IDuplicataRepository Duplicata { get; }



        Task<int> SaveAsync();

    }
}
