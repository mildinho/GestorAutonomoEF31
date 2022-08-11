using GestorAutonomo.Biblioteca.Arquivo;
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
using System.Threading.Tasks;

namespace GestorAutonomo.Areas.Admin.Controllers
{
    [Area("Admin")]
    [LoginAutorizacao]

    public class ProdutoController : Controller
    {
        private readonly IImagemRepository _repositoryImagem;
        private readonly IUnitOfWork _uow;
        private readonly SessaoUsuario _sessaoUsuario;

        public ProdutoController(IImagemRepository imagem, IUnitOfWork uow, SessaoUsuario sessaoUsuario)
        {
            _repositoryImagem = imagem;
            _uow = uow;
            _sessaoUsuario = sessaoUsuario;
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

            var registros = await _uow.Produto.ListarTodosRegistrosAsync(pagina, pesquisa);

            return View(registros);
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


            var obj01 = await _uow.Produto.SelecionarPorCodigoAsync(Id);
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

            var obj01 = await _uow.Produto.SelecionarPorCodigoAsync(Id);
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

            var obj01 = await _uow.Produto.SelecionarPorCodigoAsync(Id);
            if (obj01 == null)
                return View("NoDataFound");

            return View("Manutencao", obj01);
        }







        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Manutencao([FromForm] Produto produto, Opcoes operacao)
        {
            produto.EmpresaId = _sessaoUsuario.GetLoginUsuario().EmpresaId;
            
            if (Opcoes.Delete == (Opcoes)operacao)
            {
                List<ProdutoSaldo> obj01 = await _uow.ProdutoSaldo.ObterPontosPorProduto(produto.Id, true);
                if (obj01.Count > 0)
                {

                    TempData["msg_e"] = "Existem Produtos Cadastrados no Ponto de Estoque";
                    return RedirectToAction(nameof(Index));
                }

                if (produto.Imagens != null)
                {
                    GerenciadorArquivo.ExcluirImagensProduto(produto.Imagens.ToList());
                }
                _repositoryImagem.ExcluirImagensProduto(produto.Id);

                await _uow.Produto.DeletarAsync(produto.Id);
                await _uow.SaveAsync();

                AlertNotification.Warning(Mensagem.MSG_S002);


                return RedirectToAction(nameof(Index));
            }
            else if (ModelState.IsValid)
            {
                if (Opcoes.Create == (Opcoes)operacao)
                {

                    await _uow.Produto.InserirAsync(produto);
                    await _uow.SaveAsync();

                    AlertNotification.Warning(Mensagem.MSG_S001);

                }
                else if (Opcoes.Update == (Opcoes)operacao)
                {

                    await _uow.Produto.AtualizarAsync(produto);
                    await _uow.SaveAsync();

                    AlertNotification.Warning(Mensagem.MSG_S001);

                }
                List<Imagem> ListaImagens = GerenciadorArquivo.MoverImagensProduto(new List<string>(Request.Form["imagem"]), produto.Id);

                _repositoryImagem.CadastrarImagens(ListaImagens, produto.Id);

                return RedirectToAction(nameof(Index));

            }

            ViewBag.CRUD = ConfiguraMensagem((Opcoes)operacao);

            var categorias = await _uow.CategoriaProduto.ListarTodosRegistrosAsync();
            ViewBag.Categorias = categorias.Select(a => new SelectListItem(a.Descricao, a.Id.ToString()));

            return View();

        }
    }
}
