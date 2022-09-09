using GestorAutonomo.Biblioteca.CRUD;
using GestorAutonomo.Biblioteca.Filtro;
using GestorAutonomo.Biblioteca.Notification;
using GestorAutonomo.Domain.Biblioteca.Lang;
using GestorAutonomo.Domain.Entities;
using GestorAutonomo.Domain.Interfaces;
using GestorAutonomo.Session;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace GestorAutonomo.Areas.Admin.Controllers
{
    [Area("Admin")]
    [LoginAutorizacao]
    public class PontosEstoqueController : Controller
    {

        private readonly IUnitOfWork _uow;
        private readonly SessaoUsuario _sessaoUsuario;

        public PontosEstoqueController( IUnitOfWork uow, SessaoUsuario sessaoUsuario)
        {
            _uow = uow;
            _sessaoUsuario = sessaoUsuario;
        }


        private Task<CRUD> ConfiguraMensagem(Opcoes opcoes)
        {
            CRUD crud = new CRUD();

            if (opcoes == Opcoes.Information)
            {
                crud.Titulo = "Pontos de Estoque";
                crud.Descricao = "Aqui você poderá configurar seu Cadastro de Pontos de Estoque";
                crud.SubTitulo = "Dados do seu Ponto de Estoque";
                crud.Operacao = Opcoes.Information;

            }
            else if (opcoes == Opcoes.Create)
            {
                crud.Titulo = "Incluir Ponto de Estoque";
                crud.Descricao = "Aqui você poderá configurar seu Cadastro de Ponto de Estoque";
                crud.SubTitulo = "Inserir Novo Ponto de Estoque";
                crud.Operacao = Opcoes.Create;
            }
            else if (opcoes == Opcoes.Update)
            {
                crud.Titulo = "Alterar Ponto de Estoque";
                crud.Descricao = "Aqui você poderá configurar seu Cadastro de Ponto de Estoque";
                crud.SubTitulo = "Alterar Ponto de Estoque";
                crud.Operacao = Opcoes.Update;
            }
            else if (opcoes == Opcoes.Delete)
            {
                crud.Titulo = "Excluir Ponto de Estoque";
                crud.Descricao = "CUIDADO ao Excluir um Ponto de Estoque, Este processo é irreversivel";
                crud.SubTitulo = "Excluir Ponto de Estoque";
                crud.Operacao = Opcoes.Delete;
            }
            else if (opcoes == Opcoes.Read)
            {
                crud.Titulo = "Consultar Ponto de Estoque";
                crud.Descricao = "Aqui você poderá consultar seu Cadastro de Ponto de Estoque";
                crud.SubTitulo = "Consultar Ponto de Estoque";
                crud.Operacao = Opcoes.Read;
            }

            return Task.FromResult(crud);
        }


        public async Task<IActionResult> Index(int? pagina, string pesquisa)
        {
            ViewBag.CRUD = await ConfiguraMensagem(Opcoes.Information);


            var registros = await _uow.PontosEstoque.ListarTodosRegistrosAsync(pagina, pesquisa);

            return View(registros);
        }




        [HttpGet]
        public async Task<IActionResult> Cadastrar()
        {
            ViewBag.CRUD = await ConfiguraMensagem(Opcoes.Create);
            return View("Manutencao");
        }



        [HttpGet]
        public async Task<IActionResult> Editar(Guid Id)
        {

            ViewBag.CRUD = await ConfiguraMensagem(Opcoes.Update);

            var obj01 = await _uow.PontosEstoque.SelecionarPorCodigoAsync(Id);

            if (obj01 == null)
                return View("NoDataFound");

            return View("Manutencao", obj01);
        }


        [HttpGet]
        public async Task<IActionResult> Consultar(Guid Id)
        {

            ViewBag.CRUD = await ConfiguraMensagem(Opcoes.Read);

            var obj01 = await _uow.PontosEstoque.SelecionarPorCodigoAsync(Id);
            if (obj01 == null)
                return View("NoDataFound");


            return View("Manutencao", obj01);
        }


        [HttpGet]
        public async Task<IActionResult> Deletar(Guid Id)
        {

            ViewBag.CRUD = await ConfiguraMensagem(Opcoes.Delete);

            var obj01 = await _uow.PontosEstoque.SelecionarPorCodigoAsync(Id);
            if (obj01 == null)
                return View("NoDataFound");

            return View("Manutencao", obj01);
        }







        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Manutencao([FromForm] PontosEstoque parceiro, Opcoes operacao)
        {

            parceiro.EmpresaId = _sessaoUsuario.GetLoginUsuario().EmpresaId;

            if (Opcoes.Delete == (Opcoes)operacao)
            {
                await _uow.PontosEstoque.DeletarAsync(parceiro.Id);
                await _uow.SaveAsync();

                AlertNotification.Warning(Mensagem.MSG_S002);
                
                return RedirectToAction(nameof(Index));
            }
            else if (ModelState.IsValid)
            {
                if (Opcoes.Create == (Opcoes)operacao)
                {
                    await _uow.PontosEstoque.InserirAsync(parceiro);
                    await _uow.SaveAsync();

                    AlertNotification.Warning(Mensagem.MSG_S001);
                }
                else if (Opcoes.Update == (Opcoes)operacao)
                {
                    await _uow.PontosEstoque.AtualizarAsync(parceiro);
                    await _uow.SaveAsync();

                    AlertNotification.Warning(Mensagem.MSG_S001);
                }

                return RedirectToAction(nameof(Index));

            }

            ViewBag.CRUD = await ConfiguraMensagem((Opcoes)operacao);

            return View();

        }




    }



}
