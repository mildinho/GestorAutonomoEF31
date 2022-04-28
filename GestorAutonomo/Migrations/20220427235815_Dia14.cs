using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GestorAutonomo.Migrations
{
    public partial class Dia14 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CS_BANCO",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Codigo = table.Column<string>(nullable: false),
                    Descricao = table.Column<string>(nullable: false),
                    Data_Cadastro = table.Column<DateTime>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Data_Alteracao = table.Column<DateTime>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CS_BANCO", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CS_CATEGORIAPRODUTO",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Descricao = table.Column<string>(nullable: false),
                    CategoriaPaiId = table.Column<int>(nullable: true),
                    Data_Cadastro = table.Column<DateTime>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Data_Alteracao = table.Column<DateTime>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CS_CATEGORIAPRODUTO", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CS_CATEGORIAPRODUTO_CS_CATEGORIAPRODUTO_CategoriaPaiId",
                        column: x => x.CategoriaPaiId,
                        principalTable: "CS_CATEGORIAPRODUTO",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CS_LOGIN",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    EMail = table.Column<string>(nullable: false),
                    Password = table.Column<string>(maxLength: 255, nullable: false),
                    Data_Cadastro = table.Column<DateTime>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Data_Alteracao = table.Column<DateTime>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CS_LOGIN", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CS_PONTOESTOQUE",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Descricao = table.Column<string>(nullable: false),
                    Data_Cadastro = table.Column<DateTime>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Data_Alteracao = table.Column<DateTime>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CS_PONTOESTOQUE", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CS_UF",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Sigla = table.Column<string>(nullable: false),
                    Descricao = table.Column<string>(nullable: false),
                    Data_Cadastro = table.Column<DateTime>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Data_Alteracao = table.Column<DateTime>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CS_UF", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CS_PRODUTO",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Referencia = table.Column<string>(nullable: false),
                    Descricao = table.Column<string>(nullable: false),
                    Data_Cadastro = table.Column<DateTime>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Data_Alteracao = table.Column<DateTime>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn),
                    Altura = table.Column<int>(nullable: false),
                    Largura = table.Column<int>(nullable: false),
                    Comprimento = table.Column<int>(nullable: false),
                    Peso = table.Column<double>(nullable: false),
                    PrecoVenda = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    PrecoCusto = table.Column<double>(nullable: true),
                    PrecoMedio = table.Column<double>(nullable: true),
                    CategoriaProdutoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CS_PRODUTO", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CS_PRODUTO_CS_CATEGORIAPRODUTO_CategoriaProdutoId",
                        column: x => x.CategoriaProdutoId,
                        principalTable: "CS_CATEGORIAPRODUTO",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CS_EMPRESA",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CNPJ_CPF = table.Column<double>(nullable: false),
                    Nome = table.Column<string>(nullable: false),
                    Fantasia = table.Column<string>(nullable: false),
                    Data_Cadastro = table.Column<DateTime>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Data_Alteracao = table.Column<DateTime>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn),
                    Endereco = table.Column<string>(nullable: true),
                    Numero = table.Column<string>(nullable: true),
                    Bairro = table.Column<string>(nullable: true),
                    Complemento = table.Column<string>(nullable: true),
                    Cidade = table.Column<string>(nullable: true),
                    CEP = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    EmailComercial = table.Column<string>(nullable: true),
                    EmailFinanceiro = table.Column<string>(nullable: true),
                    EmailGerencial = table.Column<string>(nullable: true),
                    TelefonePrincipal = table.Column<string>(nullable: false),
                    TelefoneComercial = table.Column<string>(nullable: true),
                    TelefoneFinanceiro = table.Column<string>(nullable: true),
                    UFId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CS_EMPRESA", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CS_EMPRESA_CS_UF_UFId",
                        column: x => x.UFId,
                        principalTable: "CS_UF",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CS_PARCEIRO",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Cliente = table.Column<int>(nullable: false),
                    Fornecedor = table.Column<int>(nullable: false),
                    Vendedor = table.Column<int>(nullable: false),
                    CNPJ_CPF = table.Column<double>(nullable: false),
                    Nome = table.Column<string>(nullable: false),
                    Fantasia = table.Column<string>(nullable: false),
                    Data_Cadastro = table.Column<DateTime>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Data_Alteracao = table.Column<DateTime>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn),
                    Endereco = table.Column<string>(nullable: true),
                    Numero = table.Column<string>(nullable: true),
                    Bairro = table.Column<string>(nullable: true),
                    Complemento = table.Column<string>(nullable: true),
                    Cidade = table.Column<string>(nullable: true),
                    CEP = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    EmailNFE = table.Column<string>(nullable: true),
                    Telefone01 = table.Column<string>(nullable: false),
                    Telefone02 = table.Column<string>(nullable: true),
                    UFId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CS_PARCEIRO", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CS_PARCEIRO_CS_UF_UFId",
                        column: x => x.UFId,
                        principalTable: "CS_UF",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CS_IMAGEM",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Caminho = table.Column<string>(nullable: true),
                    ProdutoId = table.Column<int>(nullable: false),
                    Data_Cadastro = table.Column<DateTime>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Data_Alteracao = table.Column<DateTime>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CS_IMAGEM", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CS_IMAGEM_CS_PRODUTO_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "CS_PRODUTO",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CS_PRODUTOSALDO",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ProdutoId = table.Column<int>(nullable: false),
                    PontosEstoqueId = table.Column<int>(nullable: false),
                    Saldo = table.Column<double>(nullable: false),
                    Reserva = table.Column<double>(nullable: false),
                    Data_Cadastro = table.Column<DateTime>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Data_Alteracao = table.Column<DateTime>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CS_PRODUTOSALDO", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CS_PRODUTOSALDO_CS_PONTOESTOQUE_PontosEstoqueId",
                        column: x => x.PontosEstoqueId,
                        principalTable: "CS_PONTOESTOQUE",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CS_PRODUTOSALDO_CS_PRODUTO_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "CS_PRODUTO",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CS_DUPLICATA",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TipoDuplicata = table.Column<int>(nullable: false),
                    Documento = table.Column<double>(nullable: false),
                    Parcela = table.Column<double>(nullable: false),
                    ParceiroId = table.Column<int>(nullable: false),
                    Data_Emissao = table.Column<DateTime>(nullable: false),
                    Data_Vencimento = table.Column<DateTime>(nullable: false),
                    Data_Pagamento = table.Column<DateTime>(nullable: false),
                    Valor = table.Column<double>(nullable: true),
                    Abatimento = table.Column<double>(nullable: true),
                    Acrescimo = table.Column<double>(nullable: true),
                    Valor_Pago = table.Column<double>(nullable: true),
                    BancoId = table.Column<int>(nullable: false),
                    Historico = table.Column<string>(nullable: true),
                    Nosso_Numero = table.Column<string>(nullable: true),
                    Remessa_Numero = table.Column<string>(nullable: true),
                    Data_Registro_Banco = table.Column<DateTime>(nullable: false),
                    Data_Cadastro = table.Column<DateTime>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Data_Alteracao = table.Column<DateTime>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CS_DUPLICATA", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CS_DUPLICATA_CS_BANCO_BancoId",
                        column: x => x.BancoId,
                        principalTable: "CS_BANCO",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CS_DUPLICATA_CS_PARCEIRO_ParceiroId",
                        column: x => x.ParceiroId,
                        principalTable: "CS_PARCEIRO",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CS_CATEGORIAPRODUTO_CategoriaPaiId",
                table: "CS_CATEGORIAPRODUTO",
                column: "CategoriaPaiId");

            migrationBuilder.CreateIndex(
                name: "IX_CS_DUPLICATA_BancoId",
                table: "CS_DUPLICATA",
                column: "BancoId");

            migrationBuilder.CreateIndex(
                name: "IX_CS_DUPLICATA_ParceiroId",
                table: "CS_DUPLICATA",
                column: "ParceiroId");

            migrationBuilder.CreateIndex(
                name: "IX_CS_EMPRESA_UFId",
                table: "CS_EMPRESA",
                column: "UFId");

            migrationBuilder.CreateIndex(
                name: "IX_CS_IMAGEM_ProdutoId",
                table: "CS_IMAGEM",
                column: "ProdutoId");

            migrationBuilder.CreateIndex(
                name: "IX_CS_PARCEIRO_UFId",
                table: "CS_PARCEIRO",
                column: "UFId");

            migrationBuilder.CreateIndex(
                name: "IX_CS_PRODUTO_CategoriaProdutoId",
                table: "CS_PRODUTO",
                column: "CategoriaProdutoId");

            migrationBuilder.CreateIndex(
                name: "IX_CS_PRODUTOSALDO_PontosEstoqueId",
                table: "CS_PRODUTOSALDO",
                column: "PontosEstoqueId");

            migrationBuilder.CreateIndex(
                name: "IX_CS_PRODUTOSALDO_ProdutoId",
                table: "CS_PRODUTOSALDO",
                column: "ProdutoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CS_DUPLICATA");

            migrationBuilder.DropTable(
                name: "CS_EMPRESA");

            migrationBuilder.DropTable(
                name: "CS_IMAGEM");

            migrationBuilder.DropTable(
                name: "CS_LOGIN");

            migrationBuilder.DropTable(
                name: "CS_PRODUTOSALDO");

            migrationBuilder.DropTable(
                name: "CS_BANCO");

            migrationBuilder.DropTable(
                name: "CS_PARCEIRO");

            migrationBuilder.DropTable(
                name: "CS_PONTOESTOQUE");

            migrationBuilder.DropTable(
                name: "CS_PRODUTO");

            migrationBuilder.DropTable(
                name: "CS_UF");

            migrationBuilder.DropTable(
                name: "CS_CATEGORIAPRODUTO");
        }
    }
}
