using GestorAutonomo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestorAutonomo.Data
{
    public class SeedingService
    {
        private readonly GestorAutonomoContext _context;
        public SeedingService(GestorAutonomoContext context)
        {
            _context = context;
        }

        public void Seed()
        {

            if (_context.UF.Any())
            {
                return;
            }


            UF uf01 = new UF(1, "AC", "ACRE");
            UF uf02 = new UF(2, "AL", "ALAGOAS");
            UF uf03 = new UF(3, "AP", "AMAPA");
            UF uf04 = new UF(4, "AM", "AMAZONAS");
            UF uf05 = new UF(5, "BA", "BAHIA");
            UF uf06 = new UF(6, "CE", "CEARA");
            UF uf07 = new UF(7, "ES", "ESPIRITO SANTO");
            UF uf08 = new UF(8, "GO", "GOIAS");
            UF uf09 = new UF(9, "MA", "MARANHAO");
            UF uf10 = new UF(10, "MT", "MATO GROSSO");
            UF uf11 = new UF(11, "MS", "MATO GROSSO DO SUL");
            UF uf12 = new UF(12, "MG", "MINAS GERAIS");
            UF uf13 = new UF(13, "PA", "PARA");
            UF uf14 = new UF(14, "PB", "PARAIBA");
            UF uf15 = new UF(15, "PR", "PARANA");
            UF uf16 = new UF(16, "PE", "PERNAMBUCO");
            UF uf17 = new UF(17, "PI", "PIAUI");
            UF uf18 = new UF(18, "RJ", "RIO DE JANEIRO");
            UF uf19 = new UF(19, "RN", "RIO GRANDE DO NORTE");
            UF uf20 = new UF(20, "RS", "RIO GRANDE DO SUL");
            UF uf21 = new UF(21, "RO", "RONDONIA");
            UF uf22 = new UF(22, "RR", "RORAIMA");
            UF uf23 = new UF(23, "SC", "SANTA CATARINA");
            UF uf24 = new UF(24, "SP", "SAO PAULO");
            UF uf25 = new UF(25, "SE", "SERGIPE");
            UF uf26 = new UF(26, "TO", "TOCANTINS");
            UF uf27 = new UF(27, "DF", "DISTRITO FEDERAL");



            _context.UF.AddRangeAsync(uf01, uf02, uf03, uf04, uf05, uf06, uf07, uf08, uf09, uf10);
            _context.UF.AddRangeAsync(uf11, uf12, uf13, uf14, uf15, uf16, uf17, uf18, uf19, uf20);
            _context.UF.AddRangeAsync(uf21, uf22, uf23, uf24, uf25, uf26, uf27);

            _context.SaveChangesAsync();



            _context.SaveChangesAsync();


        }
    }
}
