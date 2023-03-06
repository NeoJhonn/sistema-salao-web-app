using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace MvcSistemaSalao.Models
{
    public class ScheduleViewModel
    {
        public List<Schedule>? Schedules { get; set; }
        public Schedule? Schedule { get; set; }
        public string? Professional{ get; set; }
        public string? AppointmentDate { get; set; }
        public string? StartTime { get; set; }

    }
}
