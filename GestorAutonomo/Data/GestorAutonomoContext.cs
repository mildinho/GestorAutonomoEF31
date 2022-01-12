using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GestorAutonomo.Models;

namespace GestorAutonomo.Data
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


    }
}
