using CursoAPI.Controllers;
using CursoAPI.Models.Usuarios;

namespace CursoAPI.Configuration
{
    public interface IAuthenticationService
    {
        string GerarToken(UsuarioViewModelOutput usuarioViewModelOutput);
    }
}