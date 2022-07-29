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

            if (!_context.Empresa.Any())
            {

                Empresa empresa = new Empresa(985, "Fernando Casagrande", "Gestor autonomo", "1921013000","SP");
                _context.Empresa.Add(empresa);
            }
            _context.SaveChanges();
            Empresa empresaCadastrada = _context.Empresa.First();



            if (!_context.UF.Any())
            {

                UF uf01 = new UF(empresaCadastrada.Id,"AC", "ACRE");
                UF uf02 = new UF(empresaCadastrada.Id, "AL", "ALAGOAS");
                UF uf03 = new UF(empresaCadastrada.Id, "AP", "AMAPA");
                UF uf04 = new UF(empresaCadastrada.Id, "AM", "AMAZONAS");
                UF uf05 = new UF(empresaCadastrada.Id, "BA", "BAHIA");
                UF uf06 = new UF(empresaCadastrada.Id, "CE", "CEARA");
                UF uf07 = new UF(empresaCadastrada.Id, "ES", "ESPIRITO SANTO");
                UF uf08 = new UF(empresaCadastrada.Id, "GO", "GOIAS");
                UF uf09 = new UF(empresaCadastrada.Id, "MA", "MARANHAO");
                UF uf10 = new UF(empresaCadastrada.Id, "MT", "MATO GROSSO");
                UF uf11 = new UF(empresaCadastrada.Id, "MS", "MATO GROSSO DO SUL");
                UF uf12 = new UF(empresaCadastrada.Id, "MG", "MINAS GERAIS");
                UF uf13 = new UF(empresaCadastrada.Id, "PA", "PARA");
                UF uf14 = new UF(empresaCadastrada.Id, "PB", "PARAIBA");
                UF uf15 = new UF(empresaCadastrada.Id, "PR", "PARANA");
                UF uf16 = new UF(empresaCadastrada.Id, "PE", "PERNAMBUCO");
                UF uf17 = new UF(empresaCadastrada.Id, "PI", "PIAUI");
                UF uf18 = new UF(empresaCadastrada.Id, "RJ", "RIO DE JANEIRO");
                UF uf19 = new UF(empresaCadastrada.Id, "RN", "RIO GRANDE DO NORTE");
                UF uf20 = new UF(empresaCadastrada.Id, "RS", "RIO GRANDE DO SUL");
                UF uf21 = new UF(empresaCadastrada.Id, "RO", "RONDONIA");
                UF uf22 = new UF(empresaCadastrada.Id, "RR", "RORAIMA");
                UF uf23 = new UF(empresaCadastrada.Id, "SC", "SANTA CATARINA");
                UF uf24 = new UF(empresaCadastrada.Id, "SP", "SAO PAULO");
                UF uf25 = new UF(empresaCadastrada.Id, "SE", "SERGIPE");
                UF uf26 = new UF(empresaCadastrada.Id, "TO", "TOCANTINS");
                UF uf27 = new UF(empresaCadastrada.Id, "DF", "DISTRITO FEDERAL");



                _context.UF.AddRange(uf01, uf02, uf03, uf04, uf05, uf06, uf07, uf08, uf09, uf10);
                _context.UF.AddRange(uf11, uf12, uf13, uf14, uf15, uf16, uf17, uf18, uf19, uf20);
                _context.UF.AddRange(uf21, uf22, uf23, uf24, uf25, uf26, uf27);
            }
            _context.SaveChanges();




            if (!_context.Login.Any())
            {

                Login login = new Login("fer@uol.com.br", "123456");
                _context.Login.Add(login);
            }
            _context.SaveChanges();






            if (!_context.PontosEstoque.Any())
            {

                PontosEstoque pontosEstoque01 = new PontosEstoque("Picking");
                PontosEstoque pontosEstoque02 = new PontosEstoque("Apoio");
                PontosEstoque pontosEstoque03 = new PontosEstoque("Porta Palete");
                _context.PontosEstoque.AddRange(pontosEstoque01, pontosEstoque02, pontosEstoque03);
            }
            _context.SaveChanges();

            if (!_context.Banco.Any())
            {

                Banco bc01 = new Banco("237", "BRADESCO");
                Banco bc02 = new Banco("341", "ITAU");
                Banco bc03 = new Banco("001", "BRASIL");
                Banco bc04 = new Banco("999", "CARTEIRA");

                _context.Banco.AddRange(bc01, bc02, bc03, bc04);
            }
            _context.SaveChanges();


            if (!_context.CategoriaProduto.Any())
            {

                CategoriaProduto categoriaProduto01 = new CategoriaProduto(Guid.Parse("1"), "Livros", null);
                CategoriaProduto categoriaProduto02 = new CategoriaProduto(Guid.Parse("2"), "Livros Terror", Guid.Parse("1"));
                CategoriaProduto categoriaProduto03 = new CategoriaProduto(Guid.Parse("3"), "Livros Romance", Guid.Parse("1"));

                _context.CategoriaProduto.AddRange(categoriaProduto01);
                _context.CategoriaProduto.AddRange(categoriaProduto02);
                _context.CategoriaProduto.AddRange(categoriaProduto03);


            }
            _context.SaveChanges();


            if (!_context.Produto.Any())
            {

                Produto Produto01 = new Produto(Guid.Parse("1"), "CAR80", "LUBRIFICANTE AUTOMOTIVO");
                Produto Produto02 = new Produto(Guid.Parse("2"), "ZM501", "PASTILHAS DE FREIO JUPITER");

                _context.Produto.AddRange(Produto01, Produto02);


            }
            _context.SaveChanges();


            if (!_context.ProdutoSaldo.Any())
            {

                ProdutoSaldo ProdutoSaldo01 = new ProdutoSaldo(Guid.Parse("1"), Guid.Parse("1"), 10, 1);
                ProdutoSaldo ProdutoSaldo02 = new ProdutoSaldo(Guid.Parse("1"), Guid.Parse("2"), 0, 0);
                ProdutoSaldo ProdutoSaldo03 = new ProdutoSaldo(Guid.Parse("2"), Guid.Parse("1"), 55, 55);

                _context.ProdutoSaldo.AddRange(ProdutoSaldo01, ProdutoSaldo02, ProdutoSaldo03);


            }
            _context.SaveChanges();

            //if (!_context.Duplicatas.Any())
            //{

            //    //Duplicata duplicata01 = new Duplicata();
            //    //Duplicata duplicata02 = new Duplicata();
            //    //Duplicata duplicata03 = new Duplicata();

            //    //_context.Duplicatas.AddRange(duplicata01, duplicata02, duplicata03);


            //}
            //_context.SaveChanges();


        }
    }
}
