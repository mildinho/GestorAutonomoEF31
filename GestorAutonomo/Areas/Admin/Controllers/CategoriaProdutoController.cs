using GestorAutonomo.Biblioteca.CRUD;
using GestorAutonomo.Biblioteca.Filtro;
using GestorAutonomo.Biblioteca.Lang;
using GestorAutonomo.Biblioteca.Notification;
using GestorAutonomo.Models;
using GestorAutonomo.Repositories.Interface;
using GestorAutonomo.Session;
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
        private readonly IUnitOfWork _uow;
        private readonly SessaoUsuario _sessaoUsuario;

        public CategoriaProdutoController(IUnitOfWork uow, SessaoUsuario sessaoUsuario)
        {
            _uow = uow;
            _sessaoUsuario = sessaoUsuario;
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

            var categorias = await _uow.CategoriaProduto.ListarTodosRegistrosAsync(pagina, pesquisa);

            return View(categorias);
        }




        [HttpGet]
        public async Task<IActionResult> Cadastrar()
        {

            ViewBag.CRUD = ConfiguraMensagem(Opcoes.Create);

            var categorias = await _uow.CategoriaProduto.ListarTodosRegistrosAsync();
            ViewBag.Categorias = categorias.Select(a => new SelectListItem(a.Descricao, a.Id.ToString()));

            return View("Manutencao");
        }



        [HttpGet]
        public async Task<IActionResult> Editar(Guid Id)
        {

            ViewBag.CRUD = ConfiguraMensagem(Opcoes.Update);

            var categorias = await _uow.CategoriaProduto.ListarTodosRegistrosAsync();
            ViewBag.Categorias = categorias.Select(a => new SelectListItem(a.Descricao, a.Id.ToString()));


            var obj01 = await _uow.CategoriaProduto.SelecionarPorCodigoAsync(Id);
            if (obj01 == null)
                return View("NoDataFound");


            return View("Manutencao", obj01);
        }


        [HttpGet]
        public async Task<IActionResult> Consultar(Guid Id)
        {

            ViewBag.CRUD = ConfiguraMensagem(Opcoes.Read);

            var categorias = await _uow.CategoriaProduto.ListarTodosRegistrosAsync();
            ViewBag.Categorias = categorias.Select(a => new SelectListItem(a.Descricao, a.Id.ToString()));


            var obj01 = await _uow.CategoriaProduto.SelecionarPorCodigoAsync(Id);
            if (obj01 == null)
                return View("NoDataFound");

            return View("Manutencao", obj01);
        }





        [HttpGet]
        public async Task<IActionResult> Deletar(Guid Id)
        {

            ViewBag.CRUD = ConfiguraMensagem(Opcoes.Delete);

            var categorias = await _uow.CategoriaProduto.ListarTodosRegistrosAsync();
            ViewBag.Categorias = categorias.Select(a => new SelectListItem(a.Descricao, a.Id.ToString()));


            var obj01 = await _uow.CategoriaProduto.SelecionarPorCodigoAsync(Id);
            if (obj01 == null)
                return View("NoDataFound");

            return View("Manutencao", obj01);
        }





        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Manutencao([FromForm] CategoriaProduto categoria, Opcoes operacao)
        {
            categoria.EmpresaId = _sessaoUsuario.GetLoginUsuario().EmpresaId;
            if (Opcoes.Delete == (Opcoes)operacao)
            {

                List<CategoriaProduto> obj = await _uow.CategoriaProduto.ObterCategoriasPorCategoriaPai(categoria.Id);
                if (obj.Count > 0)
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (var item in obj)
                    {
                        sb.Append($"'{item.Descricao}', ");
                    }
                    TempData["msg_e"] = string.Format(Mensagem.MSG_E007, sb.ToString());
                    return RedirectToAction(nameof(Index));
                }

                List<Produto> obj02 = await _uow.CategoriaProduto.ObterProdutosPorCategoria(categoria.Id);
                if (obj02.Count > 0)
                {

                    TempData["msg_e"] = "Existem Produtos Nesta Categoria";
                    return RedirectToAction(nameof(Index));
                }

                await _uow.CategoriaProduto.DeletarAsync(categoria.Id);
                await _uow.SaveAsync();

                AlertNotification.Warning(Mensagem.MSG_S002);

                return RedirectToAction(nameof(Index));
            }
            else if (ModelState.IsValid)
            {
                if (Opcoes.Create == (Opcoes)operacao)
                {
                    await _uow.CategoriaProduto.InserirAsync(categoria);
                    await _uow.SaveAsync();

                    AlertNotification.Warning(Mensagem.MSG_S001);

                }
                else if (Opcoes.Update == (Opcoes)operacao)
                {
                    await _uow.CategoriaProduto.AtualizarAsync(categoria);
                    await _uow.SaveAsync();

                    AlertNotification.Warning(Mensagem.MSG_S001);
                }

                return RedirectToAction(nameof(Index));

            }

            ViewBag.CRUD = ConfiguraMensagem((Opcoes)operacao);

            var categorias = await _uow.CategoriaProduto.ListarTodosRegistrosAsync();
            ViewBag.Categorias = categorias.Select(a => new SelectListItem(a.Descricao, a.Id.ToString()));

            return View();

        }
    }
}
