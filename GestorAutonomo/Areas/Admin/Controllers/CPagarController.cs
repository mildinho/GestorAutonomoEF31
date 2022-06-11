﻿using GestorAutonomo.Biblioteca.CRUD;
using GestorAutonomo.Biblioteca.Filtro;
using GestorAutonomo.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestorAutonomo.Areas.Admin.Controllers
{

    [Area("Admin")]
    [LoginAutorizacao]
    public class CPagarController : Controller
    {
        private readonly IParceiroRepository _repositoryParceiro;
        private readonly IDuplicataRepository _repositoryDuplicata;


        public CPagarController(IParceiroRepository parceiroRepository, IDuplicataRepository duplicataRepository)
        {
            _repositoryParceiro = parceiroRepository;
            _repositoryDuplicata = duplicataRepository;

        }

        private CRUD ConfiguraMensagem(Opcoes opcoes)
        {
            CRUD crud = new CRUD();

            if (opcoes == Opcoes.Information)
            {
                crud.Titulo = "Contas a Pagar";
                crud.Descricao = "Aqui você poderá Lancar, Alterar e Baixar Títulos do seu Contas a Pagar";
                crud.SubTitulo = "Gestão do Contas a Pagar";
                crud.Operacao = Opcoes.Information;

            }

            else if (opcoes == Opcoes.Update)
            {
                crud.Titulo = "Alteração - Contas a Pagar";
                crud.Descricao = "Aqui você poderá Lancar, Alterar e Baixar Títulos do seu Contas a Pagar";
                crud.SubTitulo = "Gestão do Contas a Pagar";
                crud.Operacao = Opcoes.Update;
            }

            else if (opcoes == Opcoes.Read)
            {
                crud.Titulo = "Consulta - Contas a Pagar";
                crud.Descricao = "Aqui você poderá Lancar, Alterar e Baixar Títulos do seu Contas a Pagar";
                crud.SubTitulo = "Gestão do Contas a Pagar";
                crud.Operacao = Opcoes.Read;
            }

            return crud;
        }


        public async Task<IActionResult> Index(int? pagina, string pesquisa)
        {
             ViewBag.CRUD = ConfiguraMensagem(Opcoes.Information);

            var registros = await _repositoryParceiro.ListarTodosRegistrosAsync(TipoParceiro.Todos, pagina, pesquisa);

            return View(registros);
        }


        public async Task<IActionResult> Historico(int IdParceiro)
        {
            ViewBag.CRUD = ConfiguraMensagem(Opcoes.Information);

            var registros = await _repositoryDuplicata.ListarTodosRegistrosAsync(TipoDuplicata.Pagar,IdParceiro);

            return View(registros);
        }

    }
}
