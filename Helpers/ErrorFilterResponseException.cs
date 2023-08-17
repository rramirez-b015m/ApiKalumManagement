using KalumManagement.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;

namespace KalumManagement.Helpers
{
    public class ErrorFilterResponseException : IActionFilter, IOrderedFilter
    {
        public int Order => int.MaxValue - 10;

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if(context.Exception is SqlException)
            {
                ErrorResponse error = new ErrorResponse()
                {
                    TipoError = "COM",
                    HttpStatusCode = "503",
                    Mensaje = "Error al servicio legado SQL server"
                };  
                
                context.Result = new ObjectResult(503);
            }

        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            Console.WriteLine("hola");
            
        }
    }
}