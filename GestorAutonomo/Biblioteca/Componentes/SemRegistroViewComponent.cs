using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestorAutonomo.Biblioteca.Componentes
{
    public class SemRegistroViewComponent : ViewComponent
    {
        

        public async Task<IViewComponentResult> InvokeAsync()
        {
           
            return View();
        }


    }
}
