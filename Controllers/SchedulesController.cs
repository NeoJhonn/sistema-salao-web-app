using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MvcSistemaSalao.Data;
using MvcSistemaSalao.Models;
using NuGet.Common;

namespace MvcSistemaSalao.Controllers
{
    public class SchedulesController : Controller
    {
        
        private readonly MvcSistemaSalaoContext _context;

        private string[] horas = { "8:00", "8:30", "9:00", "9:30", "10:00", "10:30", "11:00", "11:30", "12:00", "12:30", "13:00", "13:30", "14:00", "14:30", "15:00", "15:30", "16:00", "16:30",
                                  "17:00", "17:30", "18:00", "18:30", "19:00", "19:30", "20:00", "20:30", "21:00", "21:30", "22:00", "22:30"};

        public SchedulesController(MvcSistemaSalaoContext context)
        {
            _context = context;
        }


        ////////////////////////////////////////////////////////Métodos Agenda Jhonny////////////////////////////////////////////////////////


        // GET: Schedules
        public async Task<IActionResult> Index_Jhonny(string appointmentDate)
        {
            

           // Use LINQ to get 
           var schedules = from m in _context.Schedule
                         select m;


            if (string.IsNullOrEmpty(appointmentDate))
            {
                var ScheduleVM = new ScheduleViewModel();

                //não carregar a agenda se não tiver uma data selecionada
                return View(ScheduleVM.Schedules);

            }
            else
            {
                schedules = schedules.Where(s => s.AppointmentDate == DateTime.Parse(appointmentDate)).Where(x => x.Professional == "Jhonny");
                

                var ScheduleVM = new ScheduleViewModel
                {
                    Schedules = await schedules.ToListAsync()
                };

                return View(ScheduleVM);
            }

        }

       
        // GET: Schedules/Details/5
        public async Task<IActionResult> Details_Jhonny(int? id, string appointmentDate)
        {
            //esconder barra de navegação
            ViewBag.ShowNav = 0;

            if (id == null || _context.Schedule == null)
            {
                return NotFound();
            }

            var schedule = await _context.Schedule
                .FirstOrDefaultAsync(m => m.Id == id);
            if (schedule == null)
            {
                return NotFound();
            }

            return View(schedule);
        }

        // GET: Schedules/Create
        public IActionResult Create_Jhonny(string appointmentDate, string startTime, string endTime)
        {
            //esconder barra de navegação
            ViewBag.ShowNav = 0;

            return View();
        }

        // POST: Schedules/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create_Jhonny(string appointmentDate, [Bind("Id,Name,Professional,Service,AppointmentDate,StartTime,EndTime")] Schedule schedule)
        {
            int x = 0;
            int y = 0;
            int z = -1;
            bool hasStartHour = false;
            bool hasEndHour = false;
            var schedulesV = from m in _context.Schedule
                             select m;

            

            if (ModelState.IsValid)
            {
               
                schedulesV = schedulesV.Where(x => x.StartTime == schedule.StartTime).Where(x => x.EndTime == schedule.EndTime).
                    Where(x => x.AppointmentDate == schedule.AppointmentDate).Where(x => x.Professional == schedule.Professional);

                

                if (schedulesV.IsNullOrEmpty())
                {
                    //Para cada hora 
                    for (int i = 0; i < horas.Length; i++)
                    {

                        //Para cada Horário do bando de dados

                        if (horas[i] == schedule.StartTime)
                        {
                            //pega o index da hora de início
                            x = i;
                            z = x;
                            for (int j = 0; j < horas.Length; j++)
                            {
                                //pega o index da hora de fim
                                if (horas[j] == schedule.EndTime)
                                {
                                    if (j >= z)
                                    {
                                        y = j;

                                    }
                                }
                            }
                        }
                    }


                    //Validano se o horário início e fim foram selecionados corretamente
                    if (z <= y)
                    {
                        //Para cada index do Schedule, verifique se não há hora marcada no intervalo de horas do horário a se marcado
                        for (int k = z; k <= y; k++)
                        {

                            foreach (var item in _context.Schedule)
                            {
                                if (horas[k] == item.StartTime && item.AppointmentDate == schedule.AppointmentDate && item.Professional == schedule.Professional)
                                {
                                    hasStartHour = true;

                                }
                                else if (horas[k] == item.EndTime && item.AppointmentDate == schedule.AppointmentDate && item.Professional == schedule.Professional)
                                {
                                    hasEndHour = true;
                                }
                            }
                        }

                        if (!hasStartHour && !hasEndHour)
                        {
                            _context.Add(schedule);
                            await _context.SaveChangesAsync();
                            return RedirectToAction("Index_Jhonny", new { appointmentDate = appointmentDate });
                        }
                        else
                        {

                            ViewBag.ErrorMessage = "Já há um cliente agendado neste horário";

                        }
                    }
                    else
                    {

                        ViewBag.ErrorMessage = "Selecione um horário de início e fim Válido";

                    }

                }
                else 
                {
                   
                    ViewBag.ErrorMessage = "Já há um cliente agendado neste horário";
                    
                }
            }
               
            return View(schedule);
        }

        // GET: Schedules/Edit/5
        public async Task<IActionResult> Edit_Jhonny(int? id, string appointmentDate)
        {
            //esconder barra de navegação
            ViewBag.ShowNav = 0;

            if (id == null || _context.Schedule == null)
            {
                return NotFound();
            }

            var schedule = await _context.Schedule.FindAsync(id);
            if (schedule == null)
            {
                return NotFound();
            }
            return View(schedule);
        }

        // POST: Schedules/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit_Jhonny(int id, string appointmentDate, [Bind("Id,Name,Professional,Service,AppointmentDate,StartTime,EndTime")] Schedule schedule)
        {
            int x = 0;
            int y = 0;
            int z = -1;
            bool hasStartHour = false;
            bool hasEndHour = false;

            if (id != schedule.Id)
            {
                return NotFound();
            }

            //Para cada hora 
            for (int i = 0; i < horas.Length; i++)
            {

                //Para cada Horário do bando de dados

                if (horas[i] == schedule.StartTime)
                {
                    //pega o index da hora de início
                    x = i;
                    z = x;
                    for (int j = 0; j < horas.Length; j++)
                    {
                        //pega o index da hora de fim
                        if (horas[j] == schedule.EndTime)
                        {
                            if (j >= z)
                            {
                                y = j;

                            }
                        }
                    }
                }
            }


            //Validano se o horário início e fim foram selecionados corretamente
            if (z <= y)
            {
                //Para cada index do Schedule, verifique se não há hora marcada no intervalo de horas do horário a se marcado
                for (int k = z; k <= y; k++)
                {

                    foreach (var item in _context.Schedule.AsNoTracking())
                    {
                        if (horas[k] == item.StartTime && item.AppointmentDate == schedule.AppointmentDate && item.Professional == schedule.Professional && k != z)
                        {
                            hasStartHour = true;

                        }
                        else if (horas[k] == item.EndTime && item.AppointmentDate == schedule.AppointmentDate && item.Professional == schedule.Professional && k != z && k == y)
                        {
                            hasEndHour = true;
                        }
                    }
                }
            }
            else 
            {
                ViewBag.ErrorMessage = "Selecione um hoário de início e fim válido";
                return View(schedule);

            }

            if (ModelState.IsValid)
            {
                try
                {
                    if(!hasStartHour && !hasEndHour)
                    {
                        _context.Update(schedule);
                        await _context.SaveChangesAsync();
                        return RedirectToAction("Index_Jhonny", new { appointmentDate = appointmentDate });
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Horário reservado ou mesmo Horário fim";
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ScheduleExists(schedule.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return View(schedule);
        }

        // GET: Schedules/Delete/5
        public async Task<IActionResult> Delete_Jhonny(int? id, string appointmentDate)
        {
            //esconder barra de navegação
            ViewBag.ShowNav = 0;

            if (id == null || _context.Schedule == null)
            {
                return NotFound();
            }

            var schedule = await _context.Schedule
                .FirstOrDefaultAsync(m => m.Id == id);
            if (schedule == null)
            {
                return NotFound();
            }

            return View(schedule);
        }

        // POST: Schedules/Delete/5
        [HttpPost, ActionName("Delete_Jhonny")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed_Jhonny(int id, string appointmentDate)
        {
            if (_context.Schedule == null)
            {
                return Problem("Entity set 'MvcSistemaSalaoContext.Schedule'  is null.");
            }
            var schedule = await _context.Schedule.FindAsync(id);
            if (schedule != null)
            {
                _context.Schedule.Remove(schedule);
            }

            
            await _context.SaveChangesAsync();
            return RedirectToAction("Index_Jhonny", new { appointmentDate = appointmentDate });
        }

        ////////////////////////////////////////////////////////Métodos Agenda Mylena////////////////////////////////////////////////////////

         // GET: Schedules
        public async Task<IActionResult> Index_Mylena(string appointmentDate)
        {

            // Use LINQ to get
            var schedules = from m in _context.Schedule
                         select m;

            
            if (string.IsNullOrEmpty(appointmentDate))
            {
                var ScheduleVM = new ScheduleViewModel();

                //não carregar a agenda se não tiver uma data selecionada
                return View(ScheduleVM.Schedules);

            }
            else
            {
                schedules = schedules.Where(s => s.AppointmentDate == DateTime.Parse(appointmentDate)).Where(x => x.Professional == "Mylena");


                var ScheduleVM = new ScheduleViewModel
                {
                    Schedules = await schedules.ToListAsync()
                };

                return View(ScheduleVM);
            }

           

            

        }

        // GET: Schedules/Details/5
        public async Task<IActionResult> Details_Mylena(int? id, string appointmentDate)
        {
            //esconder barra de navegação
            ViewBag.ShowNav = 0;

            if (id == null || _context.Schedule == null)
            {
                return NotFound();
            }

            var schedule = await _context.Schedule
                .FirstOrDefaultAsync(m => m.Id == id);
            if (schedule == null)
            {
                return NotFound();
            }

            return View(schedule);
        }

        // GET: Schedules/Create
        public IActionResult Create_Mylena(string appointmentDate, string startTime, string endTime)
        {
            //esconder barra de navegação
            ViewBag.ShowNav = 0;

            return View();
        }

        // POST: Schedules/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create_Mylena(string appointmentDate, [Bind("Id,Name,Professional,Service,AppointmentDate,StartTime,EndTime")] Schedule schedule)
        {
            int x = 0;
            int y = 0;
            int z = -1;
            bool hasStartHour = false;
            bool hasEndHour = false;
            var schedulesV = from m in _context.Schedule
                             select m;

           

            if (ModelState.IsValid)
            {
               
                schedulesV = schedulesV.Where(x => x.StartTime == schedule.StartTime).Where(x => x.EndTime == schedule.EndTime).
                    Where(x => x.AppointmentDate == schedule.AppointmentDate).Where(x => x.Professional == schedule.Professional);

                

                if (schedulesV.IsNullOrEmpty())
                {
                    //Para cada hora 
                    for (int i = 0; i < horas.Length; i++)
                    {

                        //Para cada Horário do bando de dados

                        if (horas[i] == schedule.StartTime)
                        {
                            //pega o index da hora de início
                            x = i;
                            z = x;
                            for (int j = 0; j < horas.Length; j++)
                            {
                                //pega o index da hora de fim
                                if (horas[j] == schedule.EndTime)
                                {
                                    if (j >= z)
                                    {
                                        y = j;

                                    }
                                }
                            }
                        }
                    }
                     
                    
                    //Validano se o horário início e fim foram selecionados corretamente
                    if(z < y)
                    {
                        //Para cada index do Schedule, verifique se não há hora marcada no intervalo de horas do horário a se marcado
                        for (int k = z; k <= y; k++)
                        {

                            foreach (var item in _context.Schedule)
                            {
                                if (horas[k] == item.StartTime && item.AppointmentDate == schedule.AppointmentDate && item.Professional == schedule.Professional)
                                { 
                                    hasStartHour = true;
                                        
                                }
                                else if (horas[k] == item.EndTime && item.AppointmentDate == schedule.AppointmentDate && item.Professional == schedule.Professional)
                                { 
                                    hasEndHour = true;
                                }
                            }
                        }

                        if (!hasStartHour && !hasEndHour)
                        {
                            _context.Add(schedule);
                            await _context.SaveChangesAsync();
                            return RedirectToAction("Index_Mylena", new { appointmentDate = appointmentDate });
                        }
                        else
                        {

                            ViewBag.ErrorMessage = "Já há um cliente agendado neste horário";

                        }
                    }
                    else
                    {

                        ViewBag.ErrorMessage = "Selecione um horário de início e fim Válido";

                    }
                }
                else 
                {
                   
                    ViewBag.ErrorMessage = "Já há um cliente agendado neste horário";
                    
                }
            }
               
            return View(schedule);
        }

        // GET: Schedules/Edit/5
        public async Task<IActionResult> Edit_Mylena(int? id, string appointmentDate)
        {
            //esconder barra de navegação
            ViewBag.ShowNav = 0;

            if (id == null || _context.Schedule == null)
            {
                return NotFound();
            }

            var schedule = await _context.Schedule.FindAsync(id);
            if (schedule == null)
            {
                return NotFound();
            }
            return View(schedule);
        }

        // POST: Schedules/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit_Mylena(int id, string appointmentDate, [Bind("Id,Name,Professional,Service,AppointmentDate,StartTime,EndTime")] Schedule schedule)
        {
            int x = 0;
            int y = 0;
            int z = -1;
            bool hasStartHour = false;
            bool hasEndHour = false;

            if (id != schedule.Id)
            {
                return NotFound();
            }

            //Para cada hora 
            for (int i = 0; i < horas.Length; i++)
            {

                //Para cada Horário do bando de dados

                if (horas[i] == schedule.StartTime)
                {
                    //pega o index da hora de início
                    x = i;
                    z = x;
                    for (int j = 0; j < horas.Length; j++)
                    {
                        //pega o index da hora de fim
                        if (horas[j] == schedule.EndTime)
                        {
                            if (j >= z)
                            {
                                y = j;

                            }
                        }
                    }
                }
            }


            //Validano se o horário início e fim foram selecionados corretamente
            if (z <= y)
            {
                //Para cada index do Schedule, verifique se não há hora marcada no intervalo de horas do horário a se marcado
                for (int k = z; k <= y; k++)
                {

                    foreach (var item in _context.Schedule.AsNoTracking())
                    {
                        if (horas[k] == item.StartTime && item.AppointmentDate == schedule.AppointmentDate && item.Professional == schedule.Professional && k != z)
                        {
                            hasStartHour = true;

                        }
                        else if (horas[k] == item.EndTime && item.AppointmentDate == schedule.AppointmentDate && item.Professional == schedule.Professional && k != z && k == y)
                        {
                            hasEndHour = true;
                        }
                    }
                }
            }
            else
            {
                ViewBag.ErrorMessage = "Selecione um horário de início e fim válido";
                return View(schedule);

            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (!hasStartHour && !hasEndHour)
                    {
                        _context.Update(schedule);
                        await _context.SaveChangesAsync();
                        return RedirectToAction("Index_Mylena", new { appointmentDate = appointmentDate });
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Horário reservado ou mesmo Horário fim";
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ScheduleExists(schedule.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return View(schedule);
        }

        // GET: Schedules/Delete/5
        public async Task<IActionResult> Delete_Mylena(int? id, string appointmentDate)
        {
            //esconder barra de navegação
            ViewBag.ShowNav = 0;

            if (id == null || _context.Schedule == null)
            {
                return NotFound();
            }

            var schedule = await _context.Schedule
                .FirstOrDefaultAsync(m => m.Id == id);
            if (schedule == null)
            {
                return NotFound();
            }

            return View(schedule);
        }

        // POST: Schedules/Delete/5
        [HttpPost, ActionName("Delete_Mylena")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed_Mylena(int id, string appointmentDate)
        {
            if (_context.Schedule == null)
            {
                return Problem("Entity set 'MvcSistemaSalaoContext.Schedule'  is null.");
            }
            var schedule = await _context.Schedule.FindAsync(id);
            if (schedule != null)
            {
                _context.Schedule.Remove(schedule);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction("Index_Mylena", new { appointmentDate = appointmentDate });
        }



        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private bool ScheduleExists(int id)
        {
          return _context.Schedule.Any(e => e.Id == id);
        }

        
    }
}
