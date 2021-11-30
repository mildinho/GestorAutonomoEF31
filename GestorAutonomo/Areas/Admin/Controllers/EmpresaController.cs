﻿using GestorAutonomo.Biblioteca.CRUD;
using GestorAutonomo.Biblioteca.Filtro;
using GestorAutonomo.Biblioteca.Lang;
using GestorAutonomo.Models;
using GestorAutonomo.Repositories.Interface;
using GestorAutonomo.Session;
using Microsoft.AspNetCore.Http;
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
        private CRUD crud = new CRUD();

        private readonly IEmpresaRepository _repositoryEmpresa;
        private readonly IUFRepository _repositoryUF;
        private readonly SessaoUsuario _sessaoUsuario;



        public EmpresaController(IEmpresaRepository empresa, IUFRepository uf, SessaoUsuario sessao)
        {
            _repositoryEmpresa = empresa;
            _repositoryUF = uf;
            _sessaoUsuario = sessao;

            crud.Descricao = "Aqui você poderá realizar as alterações do Cadastro da Empresa";
            crud.Titulo = "Manutenção";
            crud.SubTitulo = "Informações da sua Empresa";
            crud.Operacao = Opcoes.Update;


        }




        // GET: EmpresaController
        public async Task<IActionResult> Manutencao()
        { 
            
            ViewBag.CRUD = crud;
            IEnumerable<UF> objUF = await _repositoryUF.ListarTodosRegistrosAsync();
            ViewBag.UF =  objUF.Select( a => new SelectListItem(a.Sigla +" -" + a.Descricao, a.Id.ToString()));


           

            IEnumerable <Empresa> objList = await _repositoryEmpresa.ListarTodosRegistrosAsync();

            Empresa empresa = await _repositoryEmpresa.SelecionarPorCodigoAsync( objList.First().Id );


            return View(empresa);
        }

        [HttpPost]
        public async Task<IActionResult> Atualizar([FromForm] Empresa empresa , int id)
        {
         
            ViewBag.CRUD = crud;
            if (ModelState.IsValid)
            {
                await _repositoryEmpresa.AtualizarAsync(empresa);
         
                return RedirectToAction("Index", "Painel", new { area = "Admin" });


            }

           

            Empresa obj  = await _repositoryEmpresa.SelecionarPorCodigoAsync(id);

            return View("Manutencao", obj);
        }

    }
}
