using Microsoft.EntityFrameworkCore;
using MvcSistemaSalao.Models;
using MvcSistemaSalao.Data;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<MvcSistemaSalaoContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MvcSistemaSalaoContext") ?? throw new InvalidOperationException("Connection string 'MvcSistemaSalaoContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();


/*//Fazer seed de Login
SeedDataBase();
void SeedDataBase()
{ 
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;

        SeedLogin.Initialize(services);
    }
}*/


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}"
    );

app.Run();
