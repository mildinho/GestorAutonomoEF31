using GestorAutonomo.Models;
using GestorAutonomo.Session;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestorAutonomo.Biblioteca.Filtro
{

    public class LoginAutorizacaoAttribute : Attribute, IAuthorizationFilter

    {

        SessaoUsuario _loginUsuario;
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            _loginUsuario = (SessaoUsuario)context.HttpContext.RequestServices.GetService(typeof(SessaoUsuario));

            Login obj = _loginUsuario.GetLoginUsuario();
            if (obj == null)
            {
                
                context.Result = new RedirectToActionResult("Autenticar", "Home", null);
            }
            
        }
    }
}
