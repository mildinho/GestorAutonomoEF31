using GestorAutonomo.Domain.Entities;
using System.Linq;

namespace GestorAutonomo.Infra.Data.Context
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

            Empresa empresaCadastrada;
            CategoriaProduto categoriaCadastrada;
            PontosEstoque pontosCadastrado;
            Produto produtoCadastrado;


            if (!_context.Empresa.Any())
            {

                Empresa empresa = new Empresa(985, "Fernando Casagrande", "Gestor autonomo", "1921013000", "SP");
                _context.Empresa.Add(empresa);
                _context.SaveChanges();
            }

            empresaCadastrada = _context.Empresa.First();

            //if (!_context.TipoTelefone.Any())
            //{

            //    TipoTelefone tipoTelefone01 = new TipoTelefone(empresaCadastrada.Id, "Celuar");
            //    TipoTelefone tipoTelefone02 = new TipoTelefone(empresaCadastrada.Id, "Recado");
            //    _context.TipoTelefone.AddRange(tipoTelefone01, tipoTelefone02);

            //    _context.SaveChanges();
            //}

            if (!_context.UF.Any())
            {

                UF uf01 = new UF(empresaCadastrada.Id, "AC", "ACRE");
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

                _context.SaveChanges();
            }





            if (!_context.Login.Any())
            {

                Login login = new Login(empresaCadastrada.Id, "fer@uol.com.br", "123456");
                _context.Login.Add(login);
                _context.SaveChanges();
            }







            if (!_context.PontosEstoque.Any())
            {

                PontosEstoque pontosEstoque01 = new PontosEstoque(empresaCadastrada.Id, "Picking");
                PontosEstoque pontosEstoque02 = new PontosEstoque(empresaCadastrada.Id, "Apoio");
                PontosEstoque pontosEstoque03 = new PontosEstoque(empresaCadastrada.Id, "Porta Palete");
                _context.PontosEstoque.AddRange(pontosEstoque01, pontosEstoque02, pontosEstoque03);

                _context.SaveChanges();


            }


            if (!_context.Banco.Any())
            {

                Banco bc01 = new Banco(empresaCadastrada.Id, "237", "BRADESCO");
                Banco bc02 = new Banco(empresaCadastrada.Id, "341", "ITAU");
                Banco bc03 = new Banco(empresaCadastrada.Id, "001", "BRASIL");
                Banco bc04 = new Banco(empresaCadastrada.Id, "999", "CARTEIRA");

                _context.Banco.AddRange(bc01, bc02, bc03, bc04);

                _context.SaveChanges();
            }



            if (!_context.CategoriaProduto.Any())
            {

                CategoriaProduto categoriaProduto01 = new CategoriaProduto(empresaCadastrada.Id, "Livros", null);
                _context.CategoriaProduto.AddRange(categoriaProduto01);
                _context.SaveChanges();

                categoriaCadastrada = _context.CategoriaProduto.First(x => x.Descricao == "Livros");

                CategoriaProduto categoriaProduto02 = new CategoriaProduto(empresaCadastrada.Id, "Livros Terror", categoriaCadastrada.CategoriaPaiId);
                CategoriaProduto categoriaProduto03 = new CategoriaProduto(empresaCadastrada.Id, "Livros Romance", categoriaCadastrada.CategoriaPaiId);


                _context.CategoriaProduto.AddRange(categoriaProduto02);
                _context.CategoriaProduto.AddRange(categoriaProduto03);
                _context.SaveChanges();

            }

            categoriaCadastrada = _context.CategoriaProduto.First(x => x.Descricao == "Livros");


            if (!_context.Produto.Any())
            {

                Produto Produto01 = new Produto(empresaCadastrada.Id, "CAR80", "LUBRIFICANTE AUTOMOTIVO", categoriaCadastrada.Id);
                Produto Produto02 = new Produto(empresaCadastrada.Id, "ZM501", "PASTILHAS DE FREIO JUPITER", categoriaCadastrada.Id);

                _context.Produto.AddRange(Produto01, Produto02);

                _context.SaveChanges();
            }



            if (!_context.ProdutoSaldo.Any())
            {
                produtoCadastrado = _context.Produto.First(x => x.Referencia == "CAR80");
                pontosCadastrado = _context.PontosEstoque.First(x => x.Descricao == "Picking");
                ProdutoSaldo ProdutoSaldo01 = new ProdutoSaldo(empresaCadastrada.Id, produtoCadastrado.Id, pontosCadastrado.Id, 10, 1);

                pontosCadastrado = _context.PontosEstoque.First(x => x.Descricao == "Apoio");
                ProdutoSaldo ProdutoSaldo02 = new ProdutoSaldo(empresaCadastrada.Id, produtoCadastrado.Id, pontosCadastrado.Id, 0, 0);


                produtoCadastrado = _context.Produto.First(x => x.Referencia == "ZM501");
                pontosCadastrado = _context.PontosEstoque.First(x => x.Descricao == "Apoio");
                ProdutoSaldo ProdutoSaldo03 = new ProdutoSaldo(empresaCadastrada.Id, produtoCadastrado.Id, pontosCadastrado.Id, 55, 55);

                _context.ProdutoSaldo.AddRange(ProdutoSaldo01, ProdutoSaldo02, ProdutoSaldo03);

                _context.SaveChanges();
            }


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
