using GestorAutonomo.Biblioteca.CRUD;
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
        private readonly CRUD crud = new CRUD();
        private IEnumerable<UF> objUF;

        private readonly IEmpresaRepository _repositoryEmpresa;
        private readonly IUFRepository _repositoryUF;
        private readonly IUnitOfWork _uow;

        public EmpresaController(IEmpresaRepository empresa, IUFRepository uf, IUnitOfWork uow)
        {
            _repositoryEmpresa = empresa;
            _repositoryUF = uf;
            _uow = uow;


            crud.Descricao = "Aqui você poderá realizar as alterações do Cadastro da Empresa";
            crud.Titulo = "Manutenção";
            crud.SubTitulo = "Informações da sua Empresa";
            crud.Operacao = Opcoes.Update;

        }




        // GET: EmpresaController
        public async Task<IActionResult> Manutencao()
        { 
            
            ViewBag.CRUD = crud;

            objUF = await _repositoryUF.ListarTodosRegistrosAsync();
            ViewBag.UF =  objUF.Select( a => new SelectListItem(a.Descricao, a.Id.ToString()));
           

            IEnumerable <Empresa> objList = await _repositoryEmpresa.ListarTodosRegistrosAsync();
            Empresa empresa = await _repositoryEmpresa.SelecionarPorCodigoAsync( objList.First().Id );


            return View(empresa);
        }

        [HttpPost]
        public async Task<IActionResult> Atualizar([FromForm] Empresa empresa , Guid id)
        {         
            if (ModelState.IsValid)
            {
                await _repositoryEmpresa.AtualizarAsync(empresa);
                return RedirectToAction("Index", "Painel", new { area = "Admin" });

            }

            ViewBag.CRUD = crud;

            objUF = await _repositoryUF.ListarTodosRegistrosAsync();
            ViewBag.UF = objUF.Select(a => new SelectListItem(a.Descricao, a.Id.ToString()));

            Empresa obj  = await _repositoryEmpresa.SelecionarPorCodigoAsync(id);

            return View("Manutencao", obj);
        }

    }
}
