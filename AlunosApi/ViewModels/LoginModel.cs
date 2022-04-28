using System.ComponentModel.DataAnnotations;

namespace AlunosApi.ViewModels
{   //modal do banco que vai fazer o login vai fazer login 
    public class LoginModel
    {
        [Required(ErrorMessage = "Email é obrigatório")] //faz validações
        [EmailAddress(ErrorMessage = "Formato de email inválido")]//faz validações

        public string Email { get; set; } // model

        [Required(ErrorMessage = "A senha é obrigatório")]//faz validações
        [StringLength(20,ErrorMessage = "A {0} deve ter no minimo {2} e no máximo {1} caracteres." ,MinimumLength = 10)]  //faz validações
        [DataType(DataType.Password)]//faz validações

        public string Password { get; set; } //model

    }
}
