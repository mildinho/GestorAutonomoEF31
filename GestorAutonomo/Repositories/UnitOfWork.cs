using GestorAutonomo.Domain.Interfaces;
using GestorAutonomo.Infra.Data.Context;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace GestorAutonomo.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly GestorAutonomoContext _context;
        private readonly IConfiguration _configuration;

        public IBancoRepository Banco { get; private set; }
        public IUFRepository UF { get; private set; }
        public ILoginRepository Login { get; private set; }
        public IEmpresaRepository Empresa { get; private set; }
        public ICategoriaProdutoRepository CategoriaProduto { get; private set; }
        public IParceiroRepository Parceiro { get; private set; }
        public IPontosEstoqueRepository PontosEstoque { get; private set; }
        public IProdutoRepository Produto { get; private set; }
        public IProdutoSaldoRepository ProdutoSaldo { get; private set; }
        public IDuplicataRepository Duplicata { get; private set; }


        public UnitOfWork(GestorAutonomoContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;

            CriaInstancia();

        }

        private void CriaInstancia()
        {
            Banco = new BancoRepository(_context, _configuration);
            UF = new UFRepository(_context, _configuration);
            Login = new LoginRepository(_context, _configuration);
            Empresa = new EmpresaRepository(_context, _configuration);
            CategoriaProduto = new CategoriaProdutoRepository(_context, _configuration);
            Parceiro = new ParceiroRepository(_context, _configuration);
            PontosEstoque = new PontosEstoqueRepository(_context, _configuration);
            Produto = new ProdutoRepository(_context, _configuration);
            ProdutoSaldo = new ProdutoSaldoRepository(_context, _configuration);
            Duplicata = new DuplicataRepository(_context, _configuration);
        }


        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }


        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
