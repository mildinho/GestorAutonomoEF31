using GestorAutonomo.Biblioteca.CRUD;
using GestorAutonomo.Biblioteca.Filtro;
using GestorAutonomo.Models;
using GestorAutonomo.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestorAutonomo.Areas.Admin.Controllers
{
    [Area("Admin")]
    [LoginAutorizacao]

    public class BancoController : Controller
    {
        private readonly IBancoRepository _repositoryBanco;

        public BancoController(IBancoRepository repositoryBanco)
        {
            _repositoryBanco = repositoryBanco;
        }


        private CRUD ConfiguraMensagem(Opcoes opcoes)
        {
            CRUD crud = new CRUD();

            if (opcoes == Opcoes.Information)
            {
                crud.Titulo = "Banco";
                crud.Descricao = "Aqui você poderá configurar seu Banco Financeiro";
                crud.SubTitulo = "Dados para Controlar seus Bancos";
                crud.Operacao = Opcoes.Information;

            }
            else if (opcoes == Opcoes.Create)
            {
                crud.Titulo = "Incluir Banco Financeiro";
                crud.Descricao = "Aqui você poderá configurar seu Cadastro de Bancos Financeiros";
                crud.SubTitulo = "Inserir Novo Banco";
                crud.Operacao = Opcoes.Create;
            }
            else if (opcoes == Opcoes.Update)
            {
                crud.Titulo = "Alterar Banco Financeiro";
                crud.Descricao = "Aqui você poderá configurar seu Cadastro Bancos Financeiros";
                crud.SubTitulo = "Alterar Banco Financeiro";
                crud.Operacao = Opcoes.Update;
            }
            else if (opcoes == Opcoes.Delete)
            {
                crud.Titulo = "Excluir Banco Financeiro";
                crud.Descricao = "CUIDADO ao Excluir um Banco, Este processo é irreversivel";
                crud.SubTitulo = "Excluir Banco Financeiro";
                crud.Operacao = Opcoes.Delete;
            }
            else if (opcoes == Opcoes.Read)
            {
                crud.Titulo = "Consultar Banco Financeiro";
                crud.Descricao = "Aqui você poderá consultar seu Cadastro de Banco Financeiro";
                crud.SubTitulo = "Consultar Banco Financeiro";
                crud.Operacao = Opcoes.Read;
            }

            return crud;
        }


        public async Task<IActionResult> Index(int? pagina, string pesquisa)
        {

            ViewBag.CRUD = ConfiguraMensagem(Opcoes.Information);

            var categorias = await _repositoryBanco.ListarTodosRegistrosAsync(pagina, pesquisa);

            return View(categorias);
        }




        [HttpGet]
        public async Task<IActionResult> Cadastrar()
        {

            ViewBag.CRUD = ConfiguraMensagem(Opcoes.Create);

            var categorias = await _repositoryBanco.ListarTodosRegistrosAsync();
            ViewBag.Categorias = categorias.Select(a => new SelectListItem(a.Descricao, a.Id.ToString()));

            return View("Manutencao");
        }



        [HttpGet]
        public async Task<IActionResult> Editar(int Id)
        {

            ViewBag.CRUD = ConfiguraMensagem(Opcoes.Update);

            var categorias = await _repositoryBanco.ListarTodosRegistrosAsync();
            ViewBag.Categorias = categorias.Select(a => new SelectListItem(a.Descricao, a.Id.ToString()));


            var objCategoria = await _repositoryBanco.SelecionarPorCodigoAsync(Id);

            return View("Manutencao", objCategoria);
        }


        [HttpGet]
        public async Task<IActionResult> Consultar(int Id)
        {

            ViewBag.CRUD = ConfiguraMensagem(Opcoes.Read);

            var categorias = await _repositoryBanco.ListarTodosRegistrosAsync();
            ViewBag.Categorias = categorias.Select(a => new SelectListItem(a.Descricao, a.Id.ToString()));


            var objCategoria = await _repositoryBanco.SelecionarPorCodigoAsync(Id);

            return View("Manutencao", objCategoria);
        }





        [HttpGet]
        public async Task<IActionResult> Deletar(int Id)
        {

            ViewBag.CRUD = ConfiguraMensagem(Opcoes.Delete);

            var categorias = await _repositoryBanco.ListarTodosRegistrosAsync();
            ViewBag.Categorias = categorias.Select(a => new SelectListItem(a.Descricao, a.Id.ToString()));


            var objCategoria = await _repositoryBanco.SelecionarPorCodigoAsync(Id);

            return View("Manutencao", objCategoria);
        }





        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Manutencao([FromForm] Banco banco, Opcoes operacao)
        {
            if (Opcoes.Delete == (Opcoes)operacao)
            {
                await _repositoryBanco.DeletarAsync(banco.Id);
                return RedirectToAction(nameof(Index));
            }
            else if (ModelState.IsValid)
            {
                if (Opcoes.Create == (Opcoes)operacao)
                {
                    await _repositoryBanco.InserirAsync(banco);

                }
                else if (Opcoes.Update == (Opcoes)operacao)
                {
                    await _repositoryBanco.AtualizarAsync(banco);

                }

                return RedirectToAction(nameof(Index));

            }

            ViewBag.CRUD = ConfiguraMensagem((Opcoes)operacao);

            var categorias = await _repositoryBanco.ListarTodosRegistrosAsync();
            ViewBag.Categorias = categorias.Select(a => new SelectListItem(a.Descricao, a.Id.ToString()));

            return View();

        }
    }
}
