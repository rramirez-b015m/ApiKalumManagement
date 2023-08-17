using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using KalumManagement.Models;

namespace KalumManagement.Helpers
{
    public class ErrorFilterException : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            ErrorResponse error = new ErrorResponse(){TipoError = "Error en el servicio legado",HttpStatusCode = "503",Mensaje = context.Exception.Message};
            context.Result = new JsonResult(error);
            base.OnException(context);
        }

    }
}