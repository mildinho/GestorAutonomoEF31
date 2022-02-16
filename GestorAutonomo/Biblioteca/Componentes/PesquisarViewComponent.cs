using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestorAutonomo.Biblioteca.Componentes
{
    public class PesquisarViewComponent : ViewComponent
    {


        public IViewComponentResult Invoke(string placeholder, string aspaction)
        {
            ViewBag._placeholder = placeholder;
            ViewBag._aspaction = aspaction;



            return View();
        }


    }
}
