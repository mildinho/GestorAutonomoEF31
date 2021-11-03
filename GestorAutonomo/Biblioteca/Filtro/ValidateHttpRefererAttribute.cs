using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestorAutonomo.Biblioteca.Filtro
{
    public class ValidateHttpRefererAttribute : Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            //Executado APOS passado pelo controlador
            string referer = context.HttpContext.Request.Headers["Referer"].ToString();
            if (string.IsNullOrEmpty(referer))
            {
                context.Result = new ContentResult() { Content = "Acesso Negado" };
            }
            else
            {
                Uri uri = new Uri(referer);


                string hostReferer = uri.Host;
                string hostServer = context.HttpContext.Request.Host.Host;
                
                if (hostReferer != hostServer)
                {
                    context.Result = new ContentResult() { Content = "Acesso Negado" };
                }

            }







        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            //Executado ANTES passado pelo controlador
        }

    }
}
