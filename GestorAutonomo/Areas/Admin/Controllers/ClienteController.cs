using GestorAutonomo.Biblioteca.CRUD;
using GestorAutonomo.Biblioteca.Filtro;
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

    public class ClienteController : Controller
    {
        private readonly IParceiroRepository _repositoryParceiro;

        public ClienteController(IParceiroRepository parceiroRepository)
        {
            _repositoryParceiro = parceiroRepository;
        }


        private CRUD ConfiguraMensagem(Opcoes opcoes)
        {
            CRUD crud = new CRUD();

            if (opcoes == Opcoes.Information)
            {
                crud.Titulo = "Cliente";
                crud.Descricao = "Aqui você poderá configurar seu Cadastro de Cliente";
                crud.SubTitulo = "Dados do seu Cliente";
                crud.Operacao = Opcoes.Information;

            }
            else if (opcoes == Opcoes.Create)
            {
                crud.Titulo = "Incluir Cliente";
                crud.Descricao = "Aqui você poderá configurar seu Cadastro de Cliente";
                crud.SubTitulo = "Inserir Novo Cliente";
                crud.Operacao = Opcoes.Create;
            }
            else if (opcoes == Opcoes.Update)
            {
                crud.Titulo = "Alterar Cliente";
                crud.Descricao = "Aqui você poderá configurar seu Cadastro de Cliente";
                crud.SubTitulo = "Alterar Cliente";
                crud.Operacao = Opcoes.Update;
            }
            else if (opcoes == Opcoes.Delete)
            {
                crud.Titulo = "Excluir Cliente";
                crud.Descricao = "CUIDADO ao Excluir um Cliente, Este processo é irreversivel";
                crud.SubTitulo = "Excluir Cliente";
                crud.Operacao = Opcoes.Delete;
            }
            else if (opcoes == Opcoes.Read)
            {
                crud.Titulo = "Consultar Cliente";
                crud.Descricao = "Aqui você poderá consultar seu Cadastro de Cliente";
                crud.SubTitulo = "Consultar Clientes";
                crud.Operacao = Opcoes.Read;
            }

            return crud;
        }


        public async Task<IActionResult> Index(int? pagina, string pesquisa)
        {

            ViewBag.CRUD = ConfiguraMensagem(Opcoes.Information);

            var registros = await _repositoryParceiro.ListarTodosRegistrosAsync(pagina, pesquisa);

            return View(registros);
        }




        [HttpGet]
        public async Task<IActionResult> Cadastrar()
        {

            ViewBag.CRUD = ConfiguraMensagem(Opcoes.Create);

            var categorias = await _repositoryParceiro.ListarTodosRegistrosAsync();
           

            return View("Manutencao");
        }



        [HttpGet]
        public async Task<IActionResult> Editar(int Id)
        {

            ViewBag.CRUD = ConfiguraMensagem(Opcoes.Update);


            var objCategoria = await _repositoryParceiro.SelecionarPorCodigoAsync(Id);

            return View("Manutencao", objCategoria);
        }


        [HttpGet]
        public async Task<IActionResult> Consultar(int Id)
        {

            ViewBag.CRUD = ConfiguraMensagem(Opcoes.Read);

           

            var objCategoria = await _repositoryParceiro.SelecionarPorCodigoAsync(Id);

            return View("Manutencao", objCategoria);
        }





        [HttpGet]
        public async Task<IActionResult> Deletar(int Id)
        {

            ViewBag.CRUD = ConfiguraMensagem(Opcoes.Delete);

          


            var objCategoria = await _repositoryParceiro.SelecionarPorCodigoAsync(Id);

            return View("Manutencao", objCategoria);
        }





        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Manutencao([FromForm] Parceiro parceiro, Opcoes operacao)
        {
            if (Opcoes.Delete == (Opcoes)operacao)
            {
                await _repositoryParceiro.DeletarAsync(parceiro.Id);
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

         

            return View();

        }
    }
}
