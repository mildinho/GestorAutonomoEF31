using GestorAutonomo.Biblioteca.CRUD;
using GestorAutonomo.Biblioteca.Filtro;
using GestorAutonomo.Models;
using GestorAutonomo.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace GestorAutonomo.Areas.Admin.Controllers
{
    [Area("Admin")]
    [LoginAutorizacao]

    public class HabilitarParceiroController : Controller
    {

        private readonly IParceiroRepository _repositoryParceiro;


        public HabilitarParceiroController(IParceiroRepository parceiroRepository)
        {
            _repositoryParceiro = parceiroRepository;

        }


        private CRUD ConfiguraMensagem(Opcoes opcoes)
        {
            CRUD crud = new CRUD();

            if (opcoes == Opcoes.Information)
            {
                crud.Titulo = "Parceiro";
                crud.Descricao = "Aqui você poderá configurar seu Parceiro como Cliente, Fornecedor, Vendedor";
                crud.SubTitulo = "Tipo de Parceiro";
                crud.Operacao = Opcoes.Information;

            }

            else if (opcoes == Opcoes.Update)
            {
                crud.Titulo = "Alterar Parceiro";
                crud.Descricao = "Aqui você poderá configurar seu Parceiro como Cliente, Fornecedor, Vendedor";
                crud.SubTitulo = "Tipo de Parceiro";
                crud.Operacao = Opcoes.Update;
            }

            else if (opcoes == Opcoes.Read)
            {
                crud.Titulo = "Consultar Parceiro";
                crud.Descricao = "Aqui você poderá configurar seu Parceiro como Cliente, Fornecedor, Vendedor";
                crud.SubTitulo = "Tipo de Parceiro";
                crud.Operacao = Opcoes.Read;
            }

            return crud;
        }


        public async Task<IActionResult> Index(int? pagina, string pesquisa)
        {
            ViewBag.CRUD = ConfiguraMensagem(Opcoes.Information);

            var registros = await _repositoryParceiro.ListarTodosRegistrosAsync(TipoParceiro.Cliente, pagina, pesquisa);

            return View(registros);
        }








        [HttpGet]
        public async Task<IActionResult> Consultar(int Id)
        {

            ViewBag.CRUD = ConfiguraMensagem(Opcoes.Read);

            var obj01 = await _repositoryParceiro.SelecionarPorCodigoAsync(Id);

            return View("Manutencao", obj01);
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Manutencao(int Id, int tipo)
        {

            var parceiro2 = await _repositoryParceiro.SelecionarPorCodigoAsync(Id);
            if (parceiro2 != null)
            {
                parceiro2.Fornecedor = 1;
                await _repositoryParceiro.AtualizarAsync(parceiro2);
            }

            return RedirectToAction(nameof(Index));

        }
    }
}
