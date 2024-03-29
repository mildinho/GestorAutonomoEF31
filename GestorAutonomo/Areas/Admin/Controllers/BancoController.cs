﻿using GestorAutonomo.Biblioteca.CRUD;
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
    public class BancoController : Controller
    {
        private readonly IUnitOfWork _uow;
        private readonly SessaoUsuario _sessaoUsuario;
       
        public BancoController(IUnitOfWork uow, SessaoUsuario sessaoUsuario)
        {
            _uow = uow;
            _sessaoUsuario = sessaoUsuario;
          
            
        }


        private Task<CRUD> ConfiguraMensagem(Opcoes opcoes)
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

            return Task.FromResult(crud);
        }


        public async Task<IActionResult> Index(int? pagina, string pesquisa)
        {

            ViewBag.CRUD = await ConfiguraMensagem(Opcoes.Information);

            var registros = await _uow.Banco.ListarTodosRegistrosAsync(pagina, pesquisa);

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

            var obj01 = await _uow.Banco.SelecionarPorCodigoAsync(Id);
            if (obj01 == null)
                return View("NoDataFound");


            return View("Manutencao", obj01);
        }


        [HttpGet]
        public async Task<IActionResult> Consultar(Guid Id)
        {
            ViewBag.CRUD = await ConfiguraMensagem(Opcoes.Read);


            var obj01 = await _uow.Banco.SelecionarPorCodigoAsync(Id);
            if (obj01 == null)
                return View("NoDataFound");

            return View("Manutencao", obj01);
        }





        [HttpGet]
        public async Task<IActionResult> Deletar(Guid Id)
        {
            ViewBag.CRUD = await ConfiguraMensagem(Opcoes.Delete);

            var obj01 = await _uow.Banco.SelecionarPorCodigoAsync(Id);
            if (obj01 == null)
                return View("NoDataFound");

            return View("Manutencao", obj01);
        }





        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Manutencao([FromForm] Banco banco, Opcoes operacao)
        {
            banco.EmpresaId = _sessaoUsuario.GetLoginUsuario().EmpresaId;

            //try
            //{
                if (Opcoes.Delete == (Opcoes)operacao)
            {
                await _uow.Banco.DeletarAsync(banco.Id);
                await _uow.SaveAsync();

                AlertNotification.Warning(Mensagem.MSG_S002);


                return RedirectToAction(nameof(Index));
            }
            else if (ModelState.IsValid)
            {
                if (Opcoes.Create == (Opcoes)operacao)
                {
                    await _uow.Banco.InserirAsync(banco);
                    await _uow.SaveAsync();

                    AlertNotification.Warning(Mensagem.MSG_S001);

                }
                else if (Opcoes.Update == (Opcoes)operacao)
                {
                    await _uow.Banco.AtualizarAsync(banco);
                    await _uow.SaveAsync();
                    AlertNotification.Warning(Mensagem.MSG_S001);


                }

                return RedirectToAction(nameof(Index));

            }
            //}
            //catch
            //{

            //}

            ViewBag.CRUD = await ConfiguraMensagem((Opcoes)operacao);

            return View();

        }
    }
}
