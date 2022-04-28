using System.ComponentModel.DataAnnotations;

namespace AlunosApi.ViewModels
{
    public class RegisterModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]//faz validações
        public string Password { get; set; } //Model

        [DataType(DataType.Password)]//faz validações
        [Display(Name = "Confirma senha")]//faz validações
        [Compare("Password", ErrorMessage = "Senhas não conferem")]//faz validações

        public string ConfirmPassword { get; set; } //Model
    }
}
