using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using ReporteCaja.DAL.DBContext;

using ReporteCaja.DAL.Interfaces;
using ReporteCaja.DAL.Implementacion;
using ReporteCaja.BLL.Interfaces;
using ReporteCaja.BLL.Implementacion;


namespace ReporteCaja.IOC
{
    public static class Dependencia
    {
        public static void InyectarDependencia(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ReporteCajaContext>(options => {
                options.UseSqlServer(configuration.GetConnectionString("CadenaSQL"));
            });

            // Conecto las interfaces con sus repositorios respectivos
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>)); // ABM generico

            services.AddScoped<ICorreoServices, CorreoServices>(); // Servicio Correo
            services.AddScoped<IUtilidadesServices, UtilidadesServices>(); // Generar y Encriptar Claves de login
            services.AddScoped<ICajaUsuariosServices, CajaUsuariosServices>(); // Caja_Usuarios
            services.AddScoped<ICajaReporteServices, CajaReporteServices>(); // Caja
            services.AddScoped<IEmpresasServices, EmpresasServices>(); // Empresa
            services.AddScoped<ISucursalesServices, SucursalServices>(); // Sucursal
        }
    }
}
