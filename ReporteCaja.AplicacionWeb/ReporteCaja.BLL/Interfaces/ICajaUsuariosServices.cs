using ReporteCaja.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReporteCaja.BLL.Interfaces
{
    public interface ICajaUsuariosServices
    {
        Task<CajaUsuario> ObtenerCredenciales(string correo, string clave); // consultar para su login

        Task<bool> RestablecerClave(string CorreoDestino, string URLPlantillaCorreo);

        Task<CajaUsuario> DatoUsuario(int idUsuario);
    }
}
