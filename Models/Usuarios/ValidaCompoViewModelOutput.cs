namespace CursoAPI.Models.Usuarios
{
    using System.Collections.Generic;
    public class ValidaCompoViewModelOutput
    {
        public IEnumerable<string> Erros { get; private set; }

        public ValidaCompoViewModelOutput(IEnumerable<string> erros){
            Erros = erros;
        }
    }
}