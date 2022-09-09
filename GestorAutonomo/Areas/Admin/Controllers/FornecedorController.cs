using GestorAutonomo.Biblioteca.CRUD;
using GestorAutonomo.Biblioteca.Filtro;
using GestorAutonomo.Biblioteca.Notification;
using GestorAutonomo.Domain.Biblioteca.Lang;
using GestorAutonomo.Domain.Entities;
using GestorAutonomo.Domain.Interfaces;
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
    public class FornecedorController : Controller
    {
        private readonly IUnitOfWork _uow;
        private readonly SessaoUsuario _sessaoUsuario;

        private IEnumerable<UF> objUF;

        public FornecedorController(IUnitOfWork uow, SessaoUsuario sessaoUsuario)
        {
            _uow = uow;
            _sessaoUsuario = sessaoUsuario;
        }


        private CRUD ConfiguraMensagem(Opcoes opcoes)
        {
            CRUD crud = new CRUD();

            if (opcoes == Opcoes.Information)
            {
                crud.Titulo = "Fornecedor";
                crud.Descricao = "Aqui você poderá configurar seu Cadastro de Fornecedor";
                crud.SubTitulo = "Dados do seu Fornecedor";
                crud.Operacao = Opcoes.Information;

            }
            else if (opcoes == Opcoes.Create)
            {
                crud.Titulo = "Incluir Fornecedor";
                crud.Descricao = "Aqui você poderá configurar seu Cadastro de Fornecedor";
                crud.SubTitulo = "Inserir Novo Fornecedor";
                crud.Operacao = Opcoes.Create;
            }
            else if (opcoes == Opcoes.Update)
            {
                crud.Titulo = "Alterar Fornecedor";
                crud.Descricao = "Aqui você poderá configurar seu Cadastro de Fornecedor";
                crud.SubTitulo = "Alterar Fornecedor";
                crud.Operacao = Opcoes.Update;
            }
            else if (opcoes == Opcoes.Delete)
            {
                crud.Titulo = "Excluir Fornecedor";
                crud.Descricao = "CUIDADO ao Excluir um Fornecedor, Este processo é irreversivel";
                crud.SubTitulo = "Excluir Fornecedor";
                crud.Operacao = Opcoes.Delete;
            }
            else if (opcoes == Opcoes.Read)
            {
                crud.Titulo = "Consultar Fornecedor";
                crud.Descricao = "Aqui você poderá consultar seu Cadastro de Fornecedor";
                crud.SubTitulo = "Consultar Fornecedor";
                crud.Operacao = Opcoes.Read;
            }

            return crud;
        }


        public async Task<IActionResult> Index(int? pagina, string pesquisa)
        {
            ViewBag.CRUD = ConfiguraMensagem(Opcoes.Information);
         
            objUF = await _uow.UF.ListarTodosRegistrosAsync();
            ViewBag.UF = objUF.Select(a => new SelectListItem(a.Descricao, a.Id.ToString()));

            var registros = await _uow.Parceiro.ListarTodosRegistrosAsync(TipoParceiro.Fornecedor,  pagina, pesquisa);

            return View(registros);
        }




        [HttpGet]
        public async Task<IActionResult> Cadastrar()
        {

            ViewBag.CRUD = ConfiguraMensagem(Opcoes.Create);

            objUF = await _uow.UF.ListarTodosRegistrosAsync();
            ViewBag.UF = objUF.Select(a => new SelectListItem(a.Descricao, a.Id.ToString()));

            var categorias = await _uow.Parceiro.ListarTodosRegistrosAsync(TipoParceiro.Fornecedor);
           

            return View("Manutencao");
        }



        [HttpGet]
        public async Task<IActionResult> Editar(Guid Id)
        {

            ViewBag.CRUD = ConfiguraMensagem(Opcoes.Update);

            objUF = await _uow.UF.ListarTodosRegistrosAsync();
            ViewBag.UF = objUF.Select(a => new SelectListItem(a.Descricao, a.Id.ToString()));

            var obj01 = await _uow.Parceiro.SelecionarPorCodigoAsync(Id);
            if (obj01 == null)
                return View("NoDataFound");

            return View("Manutencao", obj01);
        }


        [HttpGet]
        public async Task<IActionResult> Consultar(Guid Id)
        {

            ViewBag.CRUD = ConfiguraMensagem(Opcoes.Read);

            objUF = await _uow.UF.ListarTodosRegistrosAsync();
            ViewBag.UF = objUF.Select(a => new SelectListItem(a.Descricao, a.Id.ToString()));


            var obj01 = await _uow.Parceiro.SelecionarPorCodigoAsync(Id);
            if (obj01 == null)
                return View("NoDataFound");

            return View("Manutencao", obj01);
        }

        [AcceptVerbs("Get", "Post")]
        public async Task<IActionResult> Existe_CPF_CNPJ(double CNPJ_CPF, Opcoes operacao)
        {
            Parceiro obj01 = null;
            if (Opcoes.Create == (Opcoes)operacao)
                obj01 = await _uow.Parceiro.SelecionarPorCNPJ_CPFAsync(CNPJ_CPF);


            if (obj01 == null)
            {
                return Json(true);
            }
           else
            {
                return Json("Documento Já Existente na Base");
            }
        }





        [HttpGet]
        public async Task<IActionResult> Deletar(Guid Id)
        {

            ViewBag.CRUD = ConfiguraMensagem(Opcoes.Delete);

            objUF = await _uow.UF.ListarTodosRegistrosAsync();
            ViewBag.UF = objUF.Select(a => new SelectListItem(a.Descricao, a.Id.ToString()));

            var obj01 = await _uow.Parceiro.SelecionarPorCodigoAsync(Id);
            if (obj01 == null)
                return View("NoDataFound");

            return View("Manutencao", obj01);
        }

        





        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Manutencao([FromForm] Parceiro parceiro, Opcoes operacao)
        {
            parceiro.EmpresaId = _sessaoUsuario.GetLoginUsuario().EmpresaId;
            parceiro.Fornecedor = 1;
            if (Opcoes.Delete == (Opcoes)operacao)
            {
                await _uow.Parceiro.DeletarAsync(parceiro.Id);
                await _uow.SaveAsync();

                AlertNotification.Warning(Mensagem.MSG_S002);

                return RedirectToAction(nameof(Index));
            }
            else if (ModelState.IsValid)
            {
                if (Opcoes.Create == (Opcoes)operacao)
                {
                  
                    await _uow.Parceiro.InserirAsync(parceiro);
                    await _uow.SaveAsync();

                    AlertNotification.Warning(Mensagem.MSG_S001);
                }
                else if (Opcoes.Update == (Opcoes)operacao)
                {

                   
                    await _uow.Parceiro.AtualizarAsync(parceiro);
                    await _uow.SaveAsync();

                    AlertNotification.Warning(Mensagem.MSG_S001);
                }

                return RedirectToAction(nameof(Index));

            }

            ViewBag.CRUD = ConfiguraMensagem((Opcoes)operacao);
            
            objUF = await _uow.UF.ListarTodosRegistrosAsync();
            ViewBag.UF = objUF.Select(a => new SelectListItem(a.Descricao, a.Id.ToString()));


            return View();

        }
    }
}
