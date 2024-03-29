﻿using GestorAutonomo.Biblioteca.CRUD;
using GestorAutonomo.Biblioteca.Filtro;
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
    public class EmpresaController : Controller
    {
        private readonly CRUD crud = new CRUD();
        private IEnumerable<UF> objUF;

        private readonly IUnitOfWork _uow;
        private readonly SessaoUsuario _sessaoUsuario;

        public EmpresaController(IUnitOfWork uow, SessaoUsuario sessaoUsuario)
        {
            _uow = uow;
            _sessaoUsuario = sessaoUsuario;

            crud.Descricao = "Aqui você poderá realizar as alterações do Cadastro da Empresa";
            crud.Titulo = "Manutenção";
            crud.SubTitulo = "Informações da sua Empresa";
            crud.Operacao = Opcoes.Update;

        }




        // GET: EmpresaController
        public async Task<IActionResult> Manutencao()
        { 
            
            ViewBag.CRUD = crud;

            objUF = await _uow.UF.ListarTodosRegistrosAsync();
            ViewBag.UF =  objUF.Select( a => new SelectListItem(a.Descricao, a.Id.ToString()));
           

            IEnumerable <Empresa> objList = await _uow.Empresa.ListarTodosRegistrosAsync();
            Empresa empresa = await _uow.Empresa.SelecionarPorCodigoAsync( objList.First().Id );


            return View(empresa);
        }

        [HttpPost]
        public async Task<IActionResult> Atualizar([FromForm] Empresa empresa , Guid id)
        {         
            if (ModelState.IsValid)
            {
                await _uow.Empresa.AtualizarAsync(empresa);
                return RedirectToAction("Index", "Painel", new { area = "Admin" });

            }

            ViewBag.CRUD = crud;

            objUF = await _uow.UF.ListarTodosRegistrosAsync();
            ViewBag.UF = objUF.Select(a => new SelectListItem(a.Descricao, a.Id.ToString()));

            Empresa obj  = await _uow.Empresa.SelecionarPorCodigoAsync(id);

            return View("Manutencao", obj);
        }

    }
}
