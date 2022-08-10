using GestorAutonomo.Biblioteca.CRUD;
using GestorAutonomo.Biblioteca.Filtro;
using GestorAutonomo.Biblioteca.Notification;
using GestorAutonomo.Models;
using GestorAutonomo.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace GestorAutonomo.Areas.Admin.Controllers
{
    [Area("Admin")]
    [LoginAutorizacao]

    public class UFController : Controller
    {
        private readonly IUnitOfWork _uow;

        public UFController( IUnitOfWork uow)
        {
             _uow = uow;
        }


        private Task<CRUD> ConfiguraMensagem(Opcoes opcoes)
        {
            CRUD crud = new CRUD();

            if (opcoes == Opcoes.Information)
            {
                crud.Titulo = "Unidade Federativa";
                crud.Descricao = "Aqui você poderá configurar sua Unidade Federativa";
                crud.SubTitulo = "Dados para Controlar as Unidades Federativas";
                crud.Operacao = Opcoes.Information;

            }
            else if (opcoes == Opcoes.Create)
            {
                crud.Titulo = "Incluir Unidade Federativa";
                crud.Descricao = "Aqui você poderá configurar seu Cadastro de Unidade Federativa";
                crud.SubTitulo = "Inserir Nova Unidade Federativa";
                crud.Operacao = Opcoes.Create;
            }
            else if (opcoes == Opcoes.Update)
            {
                crud.Titulo = "Alterar Unidade Federativa";
                crud.Descricao = "Aqui você poderá configurar sua Unidade Federativa";
                crud.SubTitulo = "Alterar Unidade Federativa";
                crud.Operacao = Opcoes.Update;
            }
            else if (opcoes == Opcoes.Delete)
            {
                crud.Titulo = "Excluir Unidade Federativa";
                crud.Descricao = "CUIDADO ao Excluir uma UF, Este processo é irreversivel";
                crud.SubTitulo = "Excluir Unidade Federativa";
                crud.Operacao = Opcoes.Delete;
            }
            else if (opcoes == Opcoes.Read)
            {
                crud.Titulo = "Consultar Unidade Federativa";
                crud.Descricao = "Aqui você poderá consultar sua Unidade Federativa";
                crud.SubTitulo = "Consultar UF";
                crud.Operacao = Opcoes.Read;
            }

            return Task.FromResult(crud);
        }


        public async Task<IActionResult> Index(int? pagina, string pesquisa)
        {

            ViewBag.CRUD = await ConfiguraMensagem(Opcoes.Information);

            var registros = await _uow.UF.ListarTodosRegistrosAsync(pagina, pesquisa);

            return View(registros);
        }




        [HttpGet]
        public async Task<IActionResult> Cadastrar()
        {
            ViewBag.CRUD = await ConfiguraMensagem (Opcoes.Create);

            return View("Manutencao");
        }



        [HttpGet]
        public async Task<IActionResult> Editar(Guid Id)
        {
            ViewBag.CRUD = await ConfiguraMensagem(Opcoes.Update);

            var obj01 = await _uow.UF.SelecionarPorCodigoAsync(Id);
            if (obj01 == null)
                return View("NoDataFound");

            return View("Manutencao", obj01);
        }


        [HttpGet]
        public async Task<IActionResult> Consultar(Guid Id)
        {
            ViewBag.CRUD = await ConfiguraMensagem(Opcoes.Read);

            var obj01 = await _uow.UF.SelecionarPorCodigoAsync(Id);
            if (obj01 == null)
                return View("NoDataFound");

            return View("Manutencao", obj01);
        }





        [HttpGet]
        public async Task<IActionResult> Deletar(Guid Id)
        {
            ViewBag.CRUD = await ConfiguraMensagem(Opcoes.Delete);

            var obj01 = await _uow.UF.SelecionarPorCodigoAsync(Id);

            if (obj01 == null)
                return View("NoDataFound");

            return View("Manutencao", obj01);
        }





        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Manutencao([FromForm] UF uf, Opcoes operacao)
        {
            if (Opcoes.Delete == (Opcoes)operacao)
            {
                await _uow.UF.DeletarAsync(uf.Id);

                AlertNotification.Warning("Registro Excluído");
                return RedirectToAction(nameof(Index));
            }
            else if (ModelState.IsValid)
            {
                if (Opcoes.Create == (Opcoes)operacao)
                {
                    await _uow.UF.InserirAsync(uf);

                }
                else if (Opcoes.Update == (Opcoes)operacao)
                {
                    await _uow.UF.AtualizarAsync(uf);

                }

                return RedirectToAction(nameof(Index));

            }

            ViewBag.CRUD = await ConfiguraMensagem((Opcoes)operacao);

            return View();

        }
    }
}
