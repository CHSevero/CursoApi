using System.ComponentModel.DataAnnotations;

namespace CursoAPI.Models.Usuarios
{
    public class LoginViewModelInput
    {
        
        [Required(ErrorMessage = "O Login é obrigatório")]
        public string Login { get; set; }

        [Required(ErrorMessage = "A Senha é obrigatório")]
        public string Senha { get; set; }
    }
}