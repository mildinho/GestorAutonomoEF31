﻿using GestorAutonomo.Biblioteca.CRUD;
using GestorAutonomo.Biblioteca.Filtro;
using GestorAutonomo.Biblioteca.Lang;
using GestorAutonomo.Models;
using GestorAutonomo.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorAutonomo.Areas.Admin.Controllers
{
    [Area("Admin")]
    [LoginAutorizacao]

    public class CategoriaProdutoController : Controller
    {
        private readonly ICategoriaProdutoRepository _repositoryCategoriaProduto;

        public CategoriaProdutoController(ICategoriaProdutoRepository categoriaProdutoRepository)
        {
            _repositoryCategoriaProduto = categoriaProdutoRepository;
        }


        private CRUD ConfiguraMensagem(Opcoes opcoes)
        {
            CRUD crud = new CRUD();

            if (opcoes == Opcoes.Information)
            {
                crud.Titulo = "Categoria";
                crud.Descricao = "Aqui você poderá configurar seu Cadastro de Categoria";
                crud.SubTitulo = "Dados para Categorizar seu Produto";
                crud.Operacao = Opcoes.Information;

            }
            else if (opcoes == Opcoes.Create)
            {
                crud.Titulo = "Incluir Categoria";
                crud.Descricao = "Aqui você poderá configurar seu Cadastro de Categoria";
                crud.SubTitulo = "Inserir Nova Categoria";
                crud.Operacao = Opcoes.Create;
            }
            else if (opcoes == Opcoes.Update)
            {
                crud.Titulo = "Alterar Categoria";
                crud.Descricao = "Aqui você poderá configurar seu Cadastro de Categoria";
                crud.SubTitulo = "Alterar Categoria";
                crud.Operacao = Opcoes.Update;
            }
            else if (opcoes == Opcoes.Delete)
            {
                crud.Titulo = "Excluir Categoria";
                crud.Descricao = "CUIDADO ao Excluir uma Categoria, Este processo é irreversivel";
                crud.SubTitulo = "Excluir Categoria";
                crud.Operacao = Opcoes.Delete;
            }
            else if (opcoes == Opcoes.Read)
            {
                crud.Titulo = "Consultar Categoria";
                crud.Descricao = "Aqui você poderá consultar seu Cadastro de Categoria";
                crud.SubTitulo = "Consultar Categoria";
                crud.Operacao = Opcoes.Read;
            }

            return crud;
        }


        public async Task<IActionResult> Index(int? pagina, string pesquisa)
        {

            ViewBag.CRUD = ConfiguraMensagem(Opcoes.Information);

            var categorias = await _repositoryCategoriaProduto.ListarTodosRegistrosAsync(pagina, pesquisa);

            return View(categorias);
        }




        [HttpGet]
        public async Task<IActionResult> Cadastrar()
        {

            ViewBag.CRUD = ConfiguraMensagem(Opcoes.Create);

            var categorias = await _repositoryCategoriaProduto.ListarTodosRegistrosAsync();
            ViewBag.Categorias = categorias.Select(a => new SelectListItem(a.Descricao, a.Id.ToString()));

            return View("Manutencao");
        }



        [HttpGet]
        public async Task<IActionResult> Editar(int Id)
        {

            ViewBag.CRUD = ConfiguraMensagem(Opcoes.Update);

            var categorias = await _repositoryCategoriaProduto.ListarTodosRegistrosAsync();
            ViewBag.Categorias = categorias.Select(a => new SelectListItem(a.Descricao, a.Id.ToString()));


            var obj01 = await _repositoryCategoriaProduto.SelecionarPorCodigoAsync(Id);
            if (obj01 == null)
                return View("NoDataFound");


            return View("Manutencao", obj01);
        }


        [HttpGet]
        public async Task<IActionResult> Consultar(int Id)
        {

            ViewBag.CRUD = ConfiguraMensagem(Opcoes.Read);

            var categorias = await _repositoryCategoriaProduto.ListarTodosRegistrosAsync();
            ViewBag.Categorias = categorias.Select(a => new SelectListItem(a.Descricao, a.Id.ToString()));


            var obj01 = await _repositoryCategoriaProduto.SelecionarPorCodigoAsync(Id);
            if (obj01 == null)
                return View("NoDataFound");

            return View("Manutencao", obj01);
        }





        [HttpGet]
        public async Task<IActionResult> Deletar(int Id)
        {

            ViewBag.CRUD = ConfiguraMensagem(Opcoes.Delete);

            var categorias = await _repositoryCategoriaProduto.ListarTodosRegistrosAsync();
            ViewBag.Categorias = categorias.Select(a => new SelectListItem(a.Descricao, a.Id.ToString()));


            var obj01 = await _repositoryCategoriaProduto.SelecionarPorCodigoAsync(Id);
            if (obj01 == null)
                return View("NoDataFound");

            return View("Manutencao", obj01);
        }





        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Manutencao([FromForm] CategoriaProduto categoria, Opcoes operacao)
        {
            if (Opcoes.Delete == (Opcoes)operacao)
            {
               
                List<CategoriaProduto> obj = await _repositoryCategoriaProduto.ObterCategoriasPorCategoriaPai( categoria.Id);
                if(obj.Count > 0)
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (var item in obj)
                    {
                        sb.Append($"'{item.Descricao}', ");
                    }
                    TempData["msg_e"] = string.Format(Mensagem.MSG_E007, sb.ToString());
                    return RedirectToAction(nameof(Index));
                }

                List<Produto> obj02 = await _repositoryCategoriaProduto.ObterProdutosPorCategoria(categoria.Id);
                if (obj02.Count > 0)
                {

                    TempData["msg_e"] = "Existem Produtos Nesta Categoria";
                    return RedirectToAction(nameof(Index));
                }

                await _repositoryCategoriaProduto.DeletarAsync(categoria.Id);

                TempData["show_excluido"] = "Excluido";

                return RedirectToAction(nameof(Index));
            }
            else if (ModelState.IsValid)
            {
                if (Opcoes.Create == (Opcoes)operacao)
                {
                    await _repositoryCategoriaProduto.InserirAsync(categoria);

                }
                else if (Opcoes.Update == (Opcoes)operacao)
                {
                    await _repositoryCategoriaProduto.AtualizarAsync(categoria);

                }

                return RedirectToAction(nameof(Index));

            }

            ViewBag.CRUD = ConfiguraMensagem((Opcoes)operacao);

            var categorias = await _repositoryCategoriaProduto.ListarTodosRegistrosAsync();
            ViewBag.Categorias = categorias.Select(a => new SelectListItem(a.Descricao, a.Id.ToString()));

            return View();

        }
    }
}
