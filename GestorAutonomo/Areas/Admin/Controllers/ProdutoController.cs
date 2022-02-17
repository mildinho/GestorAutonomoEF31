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

    public class ProdutoController : Controller
    {

        //--RESOLVER A DATA DE CADASTRO(FAZER QUE SEJA AUTOMATICA QUANDO NAO EXISTIR);


        private readonly IProdutoRepository _repositoryProduto;
        private readonly ICategoriaProdutoRepository _repositoryCategoria;
       

        public ProdutoController(IProdutoRepository produto, ICategoriaProdutoRepository categoria)
        {
            _repositoryProduto = produto;
            _repositoryCategoria = categoria;
          
        }


        private CRUD ConfiguraMensagem(Opcoes opcoes)
        {
            CRUD crud = new CRUD();

            if (opcoes == Opcoes.Information)
            {
                crud.Titulo = "Produto";
                crud.Descricao = "Aqui você poderá configurar seu Cadastro de Produto";
                crud.SubTitulo = "Dados do seu Produto";
                crud.Operacao = Opcoes.Information;

            }
            else if (opcoes == Opcoes.Create)
            {
                crud.Titulo = "Incluir Produto";
                crud.Descricao = "Aqui você poderá configurar seu Cadastro de Produto";
                crud.SubTitulo = "Inserir Novo Produto";
                crud.Operacao = Opcoes.Create;
            }
            else if (opcoes == Opcoes.Update)
            {
                crud.Titulo = "Alterar Produto";
                crud.Descricao = "Aqui você poderá configurar seu Cadastro de Produto";
                crud.SubTitulo = "Alterar Produto";
                crud.Operacao = Opcoes.Update;
            }
            else if (opcoes == Opcoes.Delete)
            {
                crud.Titulo = "Excluir Produto";
                crud.Descricao = "CUIDADO ao Excluir um Produto, Este processo é irreversivel";
                crud.SubTitulo = "Excluir Produto";
                crud.Operacao = Opcoes.Delete;
            }
            else if (opcoes == Opcoes.Read)
            {
                crud.Titulo = "Consultar Produto";
                crud.Descricao = "Aqui você poderá consultar seu Cadastro de Produto";
                crud.SubTitulo = "Consultar Produtos";
                crud.Operacao = Opcoes.Read;
            }

            return crud;
        }


        public async Task<IActionResult> Index(int? pagina, string pesquisa)
        {
            ViewBag.CRUD = ConfiguraMensagem(Opcoes.Information);

            var registros = await _repositoryProduto.ListarTodosRegistrosAsync(pagina, pesquisa);

            return View(registros);
        }




        [HttpGet]
        public async Task<IActionResult> Cadastrar()
        {
            ViewBag.CRUD = ConfiguraMensagem(Opcoes.Create);

            var categorias = await _repositoryCategoria.ListarTodosRegistrosAsync();
            ViewBag.Categorias = categorias.Select(a => new SelectListItem(a.Descricao, a.Id.ToString()));

            return View("Manutencao");
        }



        [HttpGet]
        public async Task<IActionResult> Editar(int Id)
        {
            ViewBag.CRUD = ConfiguraMensagem(Opcoes.Update);

            var categorias = await _repositoryCategoria.ListarTodosRegistrosAsync();
            ViewBag.Categorias = categorias.Select(a => new SelectListItem(a.Descricao, a.Id.ToString()));


            var obj01 = await _repositoryProduto.SelecionarPorCodigoAsync(Id);

            return View("Manutencao", obj01);
        }


        [HttpGet]
        public async Task<IActionResult> Consultar(int Id)
        {

            ViewBag.CRUD = ConfiguraMensagem(Opcoes.Read);

            var categorias = await _repositoryCategoria.ListarTodosRegistrosAsync();
            ViewBag.Categorias = categorias.Select(a => new SelectListItem(a.Descricao, a.Id.ToString()));

            var obj01 = await _repositoryProduto.SelecionarPorCodigoAsync(Id);

            return View("Manutencao", obj01);
        }

        //[AcceptVerbs("Get", "Post")]
        //public async Task<IActionResult> Existe_CPF_CNPJ(double CNPJ_CPF, Opcoes operacao)
        //{
        //    Parceiro obj01 = null;
        //    if (Opcoes.Create == (Opcoes)operacao)
        //        obj01 = await _repositoryParceiro.SelecionarPorCNPJ_CPFAsync(CNPJ_CPF);


        //    if (obj01 == null)
        //    {
        //        return Json(true);
        //    }
        //   else
        //    {
        //        return Json("Documento Já Existente na Base");
        //    }
        //}





        [HttpGet]
        public async Task<IActionResult> Deletar(int Id)
        {

            ViewBag.CRUD = ConfiguraMensagem(Opcoes.Delete);

            var categorias = await _repositoryCategoria.ListarTodosRegistrosAsync();
            ViewBag.Categorias = categorias.Select(a => new SelectListItem(a.Descricao, a.Id.ToString()));

            var obj01 = await _repositoryProduto.SelecionarPorCodigoAsync(Id);

            return View("Manutencao", obj01);
        }







        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Manutencao([FromForm] Produto produto, Opcoes operacao)
        {

            if (Opcoes.Delete == (Opcoes)operacao)
            {
                await _repositoryProduto.DeletarAsync(produto.Id);

                return RedirectToAction(nameof(Index));
            }
            else if (ModelState.IsValid)
            {
                if (Opcoes.Create == (Opcoes)operacao)
                {
                    produto.Data_Cadastro = DateTime.Now;
                    await _repositoryProduto.InserirAsync(produto);

                }
                else if (Opcoes.Update == (Opcoes)operacao)
                {
                    produto.Data_Alteracao = DateTime.Now;
                    await _repositoryProduto.AtualizarAsync(produto);

                }

                return RedirectToAction(nameof(Index));

            }

            ViewBag.CRUD = ConfiguraMensagem((Opcoes)operacao);

            var categorias = await _repositoryCategoria.ListarTodosRegistrosAsync();
            ViewBag.Categorias = categorias.Select(a => new SelectListItem(a.Descricao, a.Id.ToString()));

            return View();

        }
    }
}
