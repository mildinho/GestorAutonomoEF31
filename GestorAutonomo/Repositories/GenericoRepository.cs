using GestorAutonomo.Data;
using GestorAutonomo.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestorAutonomo.Repositories
{
    public class GenericoRepository<Tabela> : IGenericoRepository<Tabela> where Tabela : class
    {
        private readonly GestorAutonomoContext _context;
     

        public GenericoRepository(GestorAutonomoContext context)
        {
            _context = context;
        
        }
        public async Task<Tabela> SelecionarPorCodigoAsync(int Id)
        {
            return await _context.Set<Tabela>().FindAsync(Id);
        }

     
    }
}
