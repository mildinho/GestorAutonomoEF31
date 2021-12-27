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

    public class UFController : Controller
    {
        private readonly IUFRepository _repositoryUF;

        public UFController(IUFRepository repositoryUF)
        {
            _repositoryUF = repositoryUF;
        }


        private CRUD ConfiguraMensagem(Opcoes opcoes)
        {
            CRUD crud = new CRUD();

            if (opcoes == Opcoes.Information)
            {
                crud.Titulo = "UF";
                crud.Descricao = "Aqui você poderá configurar sua Unidade Federativa";
                crud.SubTitulo = "Dados para Controlar as Unidades Federativas";
                crud.Operacao = Opcoes.Information;

            }
            else if (opcoes == Opcoes.Create)
            {
                crud.Titulo = "Incluir Unidade Federativa";
                crud.Descricao = "Aqui você poderá configurar seu Cadastro de Unidade Federativa";
                crud.SubTitulo = "Inserir Nova Unidade Federativa";
                crud.Operacao = Opcoes.Create;
            }
            else if (opcoes == Opcoes.Update)
            {
                crud.Titulo = "Alterar Unidade Federativa";
                crud.Descricao = "Aqui você poderá configurar sua Unidade Federativa";
                crud.SubTitulo = "Alterar Unidade Federativa";
                crud.Operacao = Opcoes.Update;
            }
            else if (opcoes == Opcoes.Delete)
            {
                crud.Titulo = "Excluir Unidade Federativa";
                crud.Descricao = "CUIDADO ao Excluir uma UF, Este processo é irreversivel";
                crud.SubTitulo = "Excluir Unidade Federativa";
                crud.Operacao = Opcoes.Delete;
            }
            else if (opcoes == Opcoes.Read)
            {
                crud.Titulo = "Consultar UF";
                crud.Descricao = "Aqui você poderá consultar sua Unidade Federativa";
                crud.SubTitulo = "Consultar UF";
                crud.Operacao = Opcoes.Read;
            }

            return crud;
        }


        public async Task<IActionResult> Index(int? pagina, string pesquisa)
        {

            ViewBag.CRUD = ConfiguraMensagem(Opcoes.Information);

            var registros = await _repositoryUF.ListarTodosRegistrosAsync(pagina, pesquisa);

            return View(registros);
        }




        [HttpGet]
        public async Task<IActionResult> Cadastrar()
        {

            ViewBag.CRUD = ConfiguraMensagem(Opcoes.Create);

            var registros = await _repositoryUF.ListarTodosRegistrosAsync();
            ViewBag.Categorias = registros.Select(a => new SelectListItem(a.Descricao, a.Id.ToString()));

            return View("Manutencao");
        }



        [HttpGet]
        public async Task<IActionResult> Editar(int Id)
        {

            ViewBag.CRUD = ConfiguraMensagem(Opcoes.Update);

            var registros = await _repositoryUF.ListarTodosRegistrosAsync();
            ViewBag.Categorias = registros.Select(a => new SelectListItem(a.Descricao, a.Id.ToString()));


            var objRegistros = await _repositoryUF.SelecionarPorCodigoAsync(Id);

            return View("Manutencao", objRegistros);
        }


        [HttpGet]
        public async Task<IActionResult> Consultar(int Id)
        {

            ViewBag.CRUD = ConfiguraMensagem(Opcoes.Read);

            var registros = await _repositoryUF.ListarTodosRegistrosAsync();
            ViewBag.Categorias = registros.Select(a => new SelectListItem(a.Descricao, a.Id.ToString()));


            var objRegistros = await _repositoryUF.SelecionarPorCodigoAsync(Id);

            return View("Manutencao", objRegistros);
        }





        [HttpGet]
        public async Task<IActionResult> Deletar(int Id)
        {

            ViewBag.CRUD = ConfiguraMensagem(Opcoes.Delete);

            var registros = await _repositoryUF.ListarTodosRegistrosAsync();
            ViewBag.Categorias = registros.Select(a => new SelectListItem(a.Descricao, a.Id.ToString()));


            var objRegistros = await _repositoryUF.SelecionarPorCodigoAsync(Id);

            return View("Manutencao", objRegistros);
        }





        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Manutencao([FromForm] UF uf, Opcoes operacao)
        {
            if (Opcoes.Delete == (Opcoes)operacao)
            {
                await _repositoryUF.DeletarAsync(uf.Id);
                return RedirectToAction(nameof(Index));
            }
            else if (ModelState.IsValid)
            {
                if (Opcoes.Create == (Opcoes)operacao)
                {
                    await _repositoryUF.InserirAsync(uf);

                }
                else if (Opcoes.Update == (Opcoes)operacao)
                {
                    await _repositoryUF.AtualizarAsync(uf);

                }

                return RedirectToAction(nameof(Index));

            }

            ViewBag.CRUD = ConfiguraMensagem((Opcoes)operacao);

            var registros = await _repositoryUF.ListarTodosRegistrosAsync();
            ViewBag.Categorias = registros.Select(a => new SelectListItem(a.Descricao, a.Id.ToString()));

            return View();

        }
    }
}
