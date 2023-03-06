using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Build.Framework;
using Newtonsoft.Json.Serialization;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace MvcSistemaSalao.Models
{
    public class Schedule
    {
        public int Id { get; set; }

        [DisplayName("Nome do Cliente:")]
        [Required(ErrorMessage = "Digite o nome do Cliente")]
        public string? Name { get; set; }

        [DisplayName("Profissional:")]
        public string? Professional { get; set; }

        [DisplayName("Serviço:")]
        [Required(ErrorMessage = "Digite um Serviço")]
        public string? Service { get; set; }

        [DisplayName("Data:")]
        [Required(ErrorMessage = "Insira uma Data")]
        [DataType(DataType.Date)]
        public DateTime AppointmentDate { get; set; }

        [DisplayName("Horário Início:")]
        public string? StartTime { get; set; }
        [DisplayName("Horário Fim:")]
        public string? EndTime { get; set; }
    }

}

