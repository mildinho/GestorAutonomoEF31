using GestorAutonomo.Biblioteca.Filtro;
using GestorAutonomo.Repositories.Interface;
using GestorAutonomo.Session;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestorAutonomo.Areas.Admin.Controllers
{
    [Area("Admin")]
    [LoginAutorizacao]
    public class PainelController : Controller
    {
        private readonly SessaoUsuario _sessaoUsuario;
       


        public PainelController(SessaoUsuario sessaoUsuario)
        {
            _sessaoUsuario = sessaoUsuario;
        }

        public IActionResult Index()
        {
            return View();
        }


        [ValidateHttpReferer]
        public IActionResult Logout()
        {
            _sessaoUsuario.Logout();

            
            return RedirectToAction("Login", "Home", new { area = "" });


        }
    }
}
