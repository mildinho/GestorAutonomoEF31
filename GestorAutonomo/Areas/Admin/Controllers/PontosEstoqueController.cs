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

    public class PontosEstoqueController : Controller
    {



        private readonly IPontosEstoqueRepository _repositoryPontoEstoque;

        public PontosEstoqueController(IPontosEstoqueRepository pontosEstoqueRepository)
        {
            _repositoryPontoEstoque = pontosEstoqueRepository;

        }


        private CRUD ConfiguraMensagem(Opcoes opcoes)
        {
            CRUD crud = new CRUD();

            if (opcoes == Opcoes.Information)
            {
                crud.Titulo = "Pontos de Estoque";
                crud.Descricao = "Aqui você poderá configurar seu Cadastro de Pontos de Estoque";
                crud.SubTitulo = "Dados do seu Ponto de Estoque";
                crud.Operacao = Opcoes.Information;

            }
            else if (opcoes == Opcoes.Create)
            {
                crud.Titulo = "Incluir Ponto de Estoque";
                crud.Descricao = "Aqui você poderá configurar seu Cadastro de Ponto de Estoque";
                crud.SubTitulo = "Inserir Novo Ponto de Estoque";
                crud.Operacao = Opcoes.Create;
            }
            else if (opcoes == Opcoes.Update)
            {
                crud.Titulo = "Alterar Ponto de Estoque";
                crud.Descricao = "Aqui você poderá configurar seu Cadastro de Ponto de Estoque";
                crud.SubTitulo = "Alterar Ponto de Estoque";
                crud.Operacao = Opcoes.Update;
            }
            else if (opcoes == Opcoes.Delete)
            {
                crud.Titulo = "Excluir Ponto de Estoque";
                crud.Descricao = "CUIDADO ao Excluir um Ponto de Estoque, Este processo é irreversivel";
                crud.SubTitulo = "Excluir Ponto de Estoque";
                crud.Operacao = Opcoes.Delete;
            }
            else if (opcoes == Opcoes.Read)
            {
                crud.Titulo = "Consultar Ponto de Estoque";
                crud.Descricao = "Aqui você poderá consultar seu Cadastro de Ponto de Estoque";
                crud.SubTitulo = "Consultar Ponto de Estoque";
                crud.Operacao = Opcoes.Read;
            }

            return crud;
        }


        public async Task<IActionResult> Index(int? pagina, string pesquisa)
        {
            ViewBag.CRUD = ConfiguraMensagem(Opcoes.Information);


            var registros = await _repositoryPontoEstoque.ListarTodosRegistrosAsync(pagina, pesquisa);

            return View(registros);
        }




        [HttpGet]
        public async Task<IActionResult> Cadastrar()
        {

            ViewBag.CRUD = ConfiguraMensagem(Opcoes.Create);

        
            return View("Manutencao");
        }



        [HttpGet]
        public async Task<IActionResult> Editar(int Id)
        {

            ViewBag.CRUD = ConfiguraMensagem(Opcoes.Update);

            var obj01 = await _repositoryPontoEstoque.SelecionarPorCodigoAsync(Id);

            return View("Manutencao", obj01);
        }


        [HttpGet]
        public async Task<IActionResult> Consultar(int Id)
        {

            ViewBag.CRUD = ConfiguraMensagem(Opcoes.Read);

            var obj01 = await _repositoryPontoEstoque.SelecionarPorCodigoAsync(Id);

            return View("Manutencao", obj01);
        }

      
        [HttpGet]
        public async Task<IActionResult> Deletar(int Id)
        {

            ViewBag.CRUD = ConfiguraMensagem(Opcoes.Delete);
    
            var obj01 = await _repositoryPontoEstoque.SelecionarPorCodigoAsync(Id);

            return View("Manutencao", obj01);
        }







        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Manutencao([FromForm] PontosEstoque parceiro, Opcoes operacao)
        {
     
            if (Opcoes.Delete == (Opcoes)operacao)
            {
                await _repositoryPontoEstoque.DeletarAsync(parceiro.Id);
                return RedirectToAction(nameof(Index));
            }
            else if (ModelState.IsValid)
            {
                if (Opcoes.Create == (Opcoes)operacao)
                {
                    await _repositoryPontoEstoque.InserirAsync(parceiro);

                }
                else if (Opcoes.Update == (Opcoes)operacao)
                {


                    await _repositoryPontoEstoque.AtualizarAsync(parceiro);

                }

                return RedirectToAction(nameof(Index));

            }

            ViewBag.CRUD = ConfiguraMensagem((Opcoes)operacao);

           return View();

        }
    }
}
