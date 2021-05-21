using System.ComponentModel.DataAnnotations;
namespace CursoAPI.Models.Usuarios
{
    public class RegistroViewModelInput
    {
        [Required(ErrorMessage = "O Login é obrigatório")]
        public string Login { get; set; }

        [Required(ErrorMessage = "O E-mail é obrigatorio")]
        public string Email{get; set;}

        [Required(ErrorMessage = "A Senha é obrigatório")]
        public string Senha { get; set; }
    }
}