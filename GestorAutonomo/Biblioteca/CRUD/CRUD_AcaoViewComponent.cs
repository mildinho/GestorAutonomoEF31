using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestorAutonomo.Biblioteca.CRUD
{
    public class CRUD_AcaoViewComponent : ViewComponent
    {


        public IViewComponentResult Invoke()
        {
            return View();
        }


    }
}
