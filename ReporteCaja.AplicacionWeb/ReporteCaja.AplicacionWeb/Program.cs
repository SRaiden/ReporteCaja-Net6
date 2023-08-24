using ReporteCaja.AplicacionWeb.Utilidades.AutoMapper;
using ReporteCaja.IOC;

using ReporteCaja.AplicacionWeb.Utilidades.Extensiones;
using DinkToPdf;
using DinkToPdf.Contracts;

using Microsoft.AspNetCore.Authentication.Cookies; // LOGIN

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(option => {
    option.LoginPath = "/Acceso/Login";
    option.ExpireTimeSpan = TimeSpan.FromMinutes(60); // Cerrar Sesion cada 1 hora
}); // SERVICIO DE LOGUEO

builder.Services.InyectarDependencia(builder.Configuration); // Llamamos al metodo de inyectar dependencia que se encuentra en la capa ReporteCaja.IOC

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));// Ejecutar AUTOMAPPER

var context = new CustomAssemblyLoadContext();
//context.LoadUnmanagedLibrary(Path.Combine(Directory.GetCurrentDirectory(), "Utilidades/LibreriaPDF/32bits/libwkhtmltox.dll")); // PARA EL SERVIDOR
context.LoadUnmanagedLibrary(Path.Combine(Directory.GetCurrentDirectory(), "Utilidades/LibreriaPDF/64bits/libwkhtmltox.dll")); //  PARA NOSOTROS
builder.Services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools())); // AGREGA LIBRERIA PDF
builder.Services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools())); // AGREGAR LIBRERIA PDF

var app = builder.Build();

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

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Acceso}/{action=Login}/{id?}");

app.Run();
