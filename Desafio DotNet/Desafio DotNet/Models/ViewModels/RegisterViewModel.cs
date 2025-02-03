using System.ComponentModel.DataAnnotations;

namespace Desafio_DotNet.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "Digite um e-mail válido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória.")]
        [StringLength(100, ErrorMessage = "A senha deve ter no mínimo {2} caracteres.", MinimumLength = 8)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
            ErrorMessage = "A senha deve ter pelo menos 8 caracteres, uma letra maiúscula, uma minúscula, um número e um caractere especial.")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "As senhas não coincidem.")]
        public string ConfirmPassword { get; set; }
    }
}
