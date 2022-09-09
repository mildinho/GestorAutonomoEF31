using GestorAutonomo.Biblioteca.Filtro;
using GestorAutonomo.Session;
using Microsoft.AspNetCore.Mvc;

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
