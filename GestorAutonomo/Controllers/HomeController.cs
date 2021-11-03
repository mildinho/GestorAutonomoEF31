using GestorAutonomo.Biblioteca.Filtro;
using GestorAutonomo.Models;
using GestorAutonomo.Services;
using GestorAutonomo.Session;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace GestorAutonomo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SessaoUsuario _loginusuario;
        private readonly Login _login;
        private readonly LoginService _loginService;

        public HomeController(ILogger<HomeController> logger, SessaoUsuario loginUsuario, Login login, LoginService loginService)
        {
            _logger = logger;
            _loginusuario = loginUsuario;
            _login = login;
            _loginService = loginService;
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
        public IActionResult Login([FromForm] Login login)
        {

            Login obj = _loginService.Pesquisar(login.EMail, login.Password);


            if (obj != null)
            {
                _loginusuario.Login(obj);

                
                return View();
            }
            else
            {
                ViewData["msg_e"] = "Usuário e Senha Inválidos! Por favor verifique.";
                //return RedirectToAction(nameof(Autenticar));
               return View("Autenticar");
            }
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
