using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace MvcSistemaSalao.Models
{
    public class Login
    {
        [DisplayName("Login:")]
        [Required(ErrorMessage = "Digite um usuário")]
        public string? LoginID { get; set; }

        [DisplayName("Senha:")]
        [Required(ErrorMessage = "Digite sua senha")]
        public string? Password { get; set; }
    }
}
