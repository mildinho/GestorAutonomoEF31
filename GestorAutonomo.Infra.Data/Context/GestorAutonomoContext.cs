using GestorAutonomo.Domain.Entities;
using GestorAutonomo.Models;
using Microsoft.EntityFrameworkCore;

namespace GestorAutonomo.Infra.Data.Context
{
    public class GestorAutonomoContext : DbContext
    {
        public GestorAutonomoContext(DbContextOptions<GestorAutonomoContext> options)
            : base(options)
        {
        }


        public DbSet<Empresa> Empresa { get; set; }
        public DbSet<CategoriaProduto> CategoriaProduto { get; set; }
        public DbSet<UF> UF { get; set; }
        public DbSet<Banco> Banco { get; set; }
        public DbSet<Login> Login { get; set; }
        public DbSet<Parceiro> Parceiro { get; set; }
        public DbSet<Produto> Produto { get; set; }
        public DbSet<PontosEstoque> PontosEstoque { get; set; }
        public DbSet<ProdutoSaldo> ProdutoSaldo { get; set; }
        public DbSet<Imagem> Imagems { get; set; }
        public DbSet<Duplicata> Duplicatas { get; set; }
      
        public DbSet<TipoEmail> TipoEmail { get; set; }
        public DbSet<TipoLogradouro> TipoLogradouro { get; set; }
        public DbSet<TipoTelefone> TipoTelefone { get; set; }

    }
}
