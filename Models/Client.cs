using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MvcSistemaSalao.Models
{
    public class Client
    {
        public int Id { get; set; }

        [DisplayName("Nome do Cliente:")]
        [Required(ErrorMessage = "Digite o nome do Cliente")]
        public string? Name { get; set; }

        [DisplayName("Data de Nascimento:")]
        [Required(ErrorMessage = "Insira a data de nascimento")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [DisplayName("Serviço realizado:")]
        [Required(ErrorMessage = "Digite o Serviço realizado")]
        public string? Email { get; set; }

        [DisplayName("Telefone:")]
        [Required(ErrorMessage = "Digite um número de telefone")]
        public string? PhoneNumber { get; set; }

        [DisplayName("Profissional:")]
        [Required(ErrorMessage = "Informe o profissional")]
        public string? Professional { get; set; }
    }
}
