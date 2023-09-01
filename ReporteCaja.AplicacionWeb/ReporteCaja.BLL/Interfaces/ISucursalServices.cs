using ReporteCaja.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReporteCaja.BLL.Interfaces
{
    public interface ISucursalesServices
    {
        Task<List<GeneralSucursale>> Lista(int idEmpresa);

        Task<GeneralSucursale> ObtenerDatosSucursal(int id);

        Task<string> ObtenerNombreSucursal(int id);
    }
}
