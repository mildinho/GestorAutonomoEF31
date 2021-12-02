using GestorAutonomo.Biblioteca.CRUD;
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

    public class CategoriaProdutoController : Controller
    {

        private CRUD crud = new CRUD();
        private readonly ICategoriaProdutoRepository _repositoryCategoriaProduto;

        public CategoriaProdutoController(ICategoriaProdutoRepository categoriaProdutoRepository)
        {

            crud.Descricao = "Aqui você poderá realizar a gestão de CATEGORIA dos PRODUTOS";
            crud.Titulo = "Listagem";
            crud.SubTitulo = "Gestão de Categoria";
            crud.Operacao = Opcoes.Information;

            _repositoryCategoriaProduto = categoriaProdutoRepository;
        }
       
        public IActionResult Index()
        {
            return View();
        }
    }
}
