﻿using GestorAutonomo.Biblioteca.CRUD;
using GestorAutonomo.Biblioteca.Filtro;
using GestorAutonomo.Models;
using GestorAutonomo.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace GestorAutonomo.Areas.Admin.Controllers
{
    [Area("Admin")]
    [LoginAutorizacao]

    public class HabilitarParceiroController : Controller
    {
        private readonly IUnitOfWork _uow;

        public HabilitarParceiroController(IUnitOfWork uow)
        {
            _uow = uow;
        }


        private CRUD ConfiguraMensagem(Opcoes opcoes)
        {
            CRUD crud = new CRUD();

            if (opcoes == Opcoes.Information)
            {
                crud.Titulo = "Parceiro";
                crud.Descricao = "Aqui você poderá configurar seu Parceiro como Cliente, Fornecedor, Vendedor";
                crud.SubTitulo = "Tipo de Parceiro";
                crud.Operacao = Opcoes.Information;

            }

            else if (opcoes == Opcoes.Update)
            {
                crud.Titulo = "Alterar Parceiro";
                crud.Descricao = "Aqui você poderá configurar seu Parceiro como Cliente, Fornecedor, Vendedor";
                crud.SubTitulo = "Tipo de Parceiro";
                crud.Operacao = Opcoes.Update;
            }

            else if (opcoes == Opcoes.Read)
            {
                crud.Titulo = "Consultar Parceiro";
                crud.Descricao = "Aqui você poderá configurar seu Parceiro como Cliente, Fornecedor, Vendedor";
                crud.SubTitulo = "Tipo de Parceiro";
                crud.Operacao = Opcoes.Read;
            }

            return crud;
        }


        public async Task<IActionResult> Index(int? pagina, string pesquisa)
        {
            ViewBag.CRUD = ConfiguraMensagem(Opcoes.Information);

            var registros = await _uow.Parceiro.ListarTodosRegistrosAsync(TipoParceiro.Todos, pagina, pesquisa);

            return View(registros);
        }








        [HttpGet]
        public async Task<IActionResult> Consultar(Guid Id)
        {

            ViewBag.CRUD = ConfiguraMensagem(Opcoes.Read);

            var obj01 = await _uow.Parceiro.SelecionarPorCodigoAsync(Id);
            if (obj01 == null)
                return View("NoDataFound");

            return View("Manutencao", obj01);
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Manutencao(Guid Id, int tipo, int LigadoDesligado)
        {

            var parceiro = await _uow.Parceiro.SelecionarPorCodigoAsync(Id);

            if (parceiro != null && tipo <= 3) 
            {
                if (tipo == 1)
                {
                    parceiro.Cliente = LigadoDesligado;
                } else if (tipo == 2)
                {
                    parceiro.Fornecedor = LigadoDesligado;
                } else if (tipo == 3)
                {
                    parceiro.Vendedor = LigadoDesligado;
                }


                await _uow.Parceiro.AtualizarAsync(parceiro);
            }

            return RedirectToAction(nameof(Index));

        }
    }
}
