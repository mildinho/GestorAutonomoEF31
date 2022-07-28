﻿using GestorAutonomo.Biblioteca.CRUD;
using GestorAutonomo.Biblioteca.Filtro;
using GestorAutonomo.Biblioteca.Notification;
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

    public class VendedorController : Controller
    {

       

        private readonly IParceiroRepository _repositoryParceiro;
        private readonly IUFRepository _repositoryUF;
        private readonly IUnitOfWork _uow;

        private IEnumerable<UF> objUF;

        public VendedorController(IParceiroRepository parceiroRepository, IUFRepository uf, IUnitOfWork uow)
        {
            _repositoryParceiro = parceiroRepository;
            _repositoryUF = uf;
            _uow = uow;
        }


        private CRUD ConfiguraMensagem(Opcoes opcoes)
        {
            CRUD crud = new CRUD();

            if (opcoes == Opcoes.Information)
            {
                crud.Titulo = "Vendedor";
                crud.Descricao = "Aqui você poderá configurar seu Cadastro de Vendedor";
                crud.SubTitulo = "Dados do seu Vendedor";
                crud.Operacao = Opcoes.Information;

            }
            else if (opcoes == Opcoes.Create)
            {
                crud.Titulo = "Incluir Vendedor";
                crud.Descricao = "Aqui você poderá configurar seu Cadastro de Vendedor";
                crud.SubTitulo = "Inserir Novo Vendedor";
                crud.Operacao = Opcoes.Create;
            }
            else if (opcoes == Opcoes.Update)
            {
                crud.Titulo = "Alterar Vendedor";
                crud.Descricao = "Aqui você poderá configurar seu Cadastro de Vendedor";
                crud.SubTitulo = "Alterar Vendedor";
                crud.Operacao = Opcoes.Update;
            }
            else if (opcoes == Opcoes.Delete)
            {
                crud.Titulo = "Excluir Vendedor";
                crud.Descricao = "CUIDADO ao Excluir um Vendedor, Este processo é irreversivel";
                crud.SubTitulo = "Excluir Vendedor";
                crud.Operacao = Opcoes.Delete;
            }
            else if (opcoes == Opcoes.Read)
            {
                crud.Titulo = "Consultar Vendedor";
                crud.Descricao = "Aqui você poderá consultar seu Cadastro de Vendedor";
                crud.SubTitulo = "Consultar Vendedor";
                crud.Operacao = Opcoes.Read;
            }

            return crud;
        }


        public async Task<IActionResult> Index(int? pagina, string pesquisa)
        {
            ViewBag.CRUD = ConfiguraMensagem(Opcoes.Information);
         
            objUF = await _repositoryUF.ListarTodosRegistrosAsync();
            ViewBag.UF = objUF.Select(a => new SelectListItem(a.Descricao, a.Id.ToString()));

            var registros = await _repositoryParceiro.ListarTodosRegistrosAsync(TipoParceiro.Vendedor,  pagina, pesquisa);

            return View(registros);
        }




        [HttpGet]
        public async Task<IActionResult> Cadastrar()
        {

            ViewBag.CRUD = ConfiguraMensagem(Opcoes.Create);

            objUF = await _repositoryUF.ListarTodosRegistrosAsync();
            ViewBag.UF = objUF.Select(a => new SelectListItem(a.Descricao, a.Id.ToString()));

            var categorias = await _repositoryParceiro.ListarTodosRegistrosAsync(TipoParceiro.Vendedor);
           

            return View("Manutencao");
        }



        [HttpGet]
        public async Task<IActionResult> Editar(int Id)
        {

            ViewBag.CRUD = ConfiguraMensagem(Opcoes.Update);

            objUF = await _repositoryUF.ListarTodosRegistrosAsync();
            ViewBag.UF = objUF.Select(a => new SelectListItem(a.Descricao, a.Id.ToString()));

            var obj01 = await _repositoryParceiro.SelecionarPorCodigoAsync(Id);
            if (obj01 == null)
                return View("NoDataFound");

            return View("Manutencao", obj01);
        }


        [HttpGet]
        public async Task<IActionResult> Consultar(int Id)
        {

            ViewBag.CRUD = ConfiguraMensagem(Opcoes.Read);

            objUF = await _repositoryUF.ListarTodosRegistrosAsync();
            ViewBag.UF = objUF.Select(a => new SelectListItem(a.Descricao, a.Id.ToString()));


            var obj01 = await _repositoryParceiro.SelecionarPorCodigoAsync(Id);
            if (obj01 == null)
                return View("NoDataFound");

            return View("Manutencao", obj01);
        }

        [AcceptVerbs("Get", "Post")]
        public async Task<IActionResult> Existe_CPF_CNPJ(double CNPJ_CPF, Opcoes operacao)
        {
            Parceiro obj01 = null;
            if (Opcoes.Create == (Opcoes)operacao)
                obj01 = await _repositoryParceiro.SelecionarPorCNPJ_CPFAsync(CNPJ_CPF);


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
        public async Task<IActionResult> Deletar(int Id)
        {

            ViewBag.CRUD = ConfiguraMensagem(Opcoes.Delete);

            objUF = await _repositoryUF.ListarTodosRegistrosAsync();
            ViewBag.UF = objUF.Select(a => new SelectListItem(a.Descricao, a.Id.ToString()));

            var obj01 = await _repositoryParceiro.SelecionarPorCodigoAsync(Id);
            if (obj01 == null)
                return View("NoDataFound");

            return View("Manutencao", obj01);
        }

        





        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Manutencao([FromForm] Parceiro parceiro, Opcoes operacao)
        {
            parceiro.Vendedor = 1;
            if (Opcoes.Delete == (Opcoes)operacao)
            {
                await _repositoryParceiro.DeletarAsync(parceiro.Id);

                AlertNotification.Warning("Registro Excluído");


                return RedirectToAction(nameof(Index));
            }
            else if (ModelState.IsValid)
            {
                if (Opcoes.Create == (Opcoes)operacao)
                {
                 
                    await _repositoryParceiro.InserirAsync(parceiro);

                }
                else if (Opcoes.Update == (Opcoes)operacao)
                {

                   
                    await _repositoryParceiro.AtualizarAsync(parceiro);

                }

                return RedirectToAction(nameof(Index));

            }

            ViewBag.CRUD = ConfiguraMensagem((Opcoes)operacao);
            
            objUF = await _repositoryUF.ListarTodosRegistrosAsync();
            ViewBag.UF = objUF.Select(a => new SelectListItem(a.Descricao, a.Id.ToString()));


            return View();

        }
    }
}
