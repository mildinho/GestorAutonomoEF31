using GestorAutonomo.Domain.Entities;
using GestorAutonomo.Domain.Interfaces;
using GestorAutonomo.Models;
using GestorAutonomo.Session;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading.Tasks;

namespace GestorAutonomo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SessaoUsuario _sessaoUsuario;
        private readonly IUnitOfWork _uow;

        public HomeController(ILogger<HomeController> logger, SessaoUsuario sessaoUsuario, IUnitOfWork uow)
        {
            _logger = logger;
            _sessaoUsuario = sessaoUsuario;
            _uow = uow;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Autenticar()
        {

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        public async Task<IActionResult> Login([FromForm] Login login)
        {

            Login obj = await _uow.Login.SelecionarPorEmailSenhaAsync(login.Empresa.CNPJ_CPF, login.EMail, login.Password);


            if (obj != null)
            {
                _sessaoUsuario.Login(obj);


                return RedirectToAction("Index", "Painel", new { area = "Admin" });

            }
            else
            {
                ViewData["msg_e"] = "Usuário e Senha Inválidos! Por favor verifique.";
               return View("Autenticar");
            }
        }



        [Route("/PageNotFound")]
        public IActionResult PageNotFound()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
