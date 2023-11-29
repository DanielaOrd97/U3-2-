using BurgerU3.Models.Entities;
using BurgerU3.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<Repository<Clasificacion>>();
builder.Services.AddTransient<MenuRepository>();
builder.Services.AddTransient<ClasifRepository>();

builder.Services.AddDbContext<NeatContext>(x =>
                            x.UseMySql("server=localhost;user=root;password=root;database=Neat", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.28-mysql")));

builder.Services.AddMvc();
var app = builder.Build();
app.UseStaticFiles();
app.UseFileServer();

app.MapControllerRoute(
            name: "areas",
            pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
          );     //Ruteo de areas

app.MapDefaultControllerRoute();

app.Run();
