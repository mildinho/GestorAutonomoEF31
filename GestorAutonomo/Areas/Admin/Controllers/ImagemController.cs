using GestorAutonomo.Biblioteca.Arquivo;
using GestorAutonomo.Biblioteca.Filtro;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestorAutonomo.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ImagemController : Controller
    {
        [HttpPost]
        public IActionResult Armazenar(IFormFile file)
        {
            var Caminho = GerenciadorArquivo.CadastrarImagemProduto(file);

            if (Caminho.Length > 0)
            {
                return Ok(new { caminho = Caminho }); //JSON -> JavaScript -> JavaScript Object Notation
            }
            else
            {
                return new StatusCodeResult(500);
            }
        }
        public IActionResult Deletar(string caminho)
        {
            if (GerenciadorArquivo.ExcluirImagemProduto(caminho))
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
