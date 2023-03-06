using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace MvcSistemaSalao.Models
{
    public class ClientViewModel
    {
        public List<Client>? Clients { get; set; }
        public string? Professional{ get; set; }
        public Client? Client { get; set; }
    }
}
