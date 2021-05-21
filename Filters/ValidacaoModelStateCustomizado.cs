using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;

using CursoAPI.Models.Usuarios;
using Microsoft.AspNetCore.Mvc;

namespace CursoAPI.Filters
{
    public class ValidacaoModelStateCustomizado : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid){
                var validaCompoViewModelOutput = new ValidaCompoViewModelOutput(context.ModelState.SelectMany(sm => sm.Value.Errors).Select(s => s.ErrorMessage));
                context.Result = new BadRequestObjectResult(validaCompoViewModelOutput);
            }
        }
    }
}