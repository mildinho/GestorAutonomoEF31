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

    public class CategoriaProdutoController : Controller
    {

        private CRUD crud = new CRUD();
        private readonly ICategoriaProdutoRepository _repositoryCategoriaProduto;

        public CategoriaProdutoController(ICategoriaProdutoRepository categoriaProdutoRepository)
        {
            _repositoryCategoriaProduto = categoriaProdutoRepository;
        }


        public async Task<IActionResult> Index(int? pagina, string pesquisa)
        {

            crud.Descricao = "Aqui você poderá configurar seu Cadastro de Categoria";
            crud.Titulo = "Categoria";
            crud.SubTitulo = "Dados para Categorizar seu Produto";
            crud.Operacao = Opcoes.Information;
            ViewBag.CRUD = crud;

          
            var categorias = await _repositoryCategoriaProduto.ListarTodosRegistrosAsync(pagina, pesquisa);

            return View(categorias);

        }




        [HttpGet]
        public async Task<IActionResult> Cadastrar()
        {
            crud.Descricao = "Aqui você poderá configurar seu Cadastro de Categoria";
            crud.Titulo = "";
            crud.SubTitulo = "Inserir Nova Categoria";
            crud.Operacao = Opcoes.Create;
            ViewBag.CRUD = crud;

            var categorias = await _repositoryCategoriaProduto.ListarTodosRegistrosAsync();
            ViewBag.Categorias = categorias.Select(a => new SelectListItem(a.Descricao, a.Id.ToString()));

            return View();
        }




        [HttpPost]
        public async Task<IActionResult> Cadastrar([FromForm] CategoriaProduto categoria)
        {
            if (ModelState.IsValid)
            {
                await _repositoryCategoriaProduto.InserirAsync(categoria);
                return RedirectToAction(nameof(Index));

            }

            crud.Descricao = "Aqui você poderá configurar seu Cadastro de Categoria";
            crud.Titulo = "";
            crud.SubTitulo = "Inserir Nova Categoria";
            crud.Operacao = Opcoes.Create;
            ViewBag.CRUD = crud;

            var categorias = await _repositoryCategoriaProduto.ListarTodosRegistrosAsync();
            ViewBag.Categorias = categorias.Select(a => new SelectListItem(a.Descricao, a.Id.ToString()));

            return View();



        }
    }
}
