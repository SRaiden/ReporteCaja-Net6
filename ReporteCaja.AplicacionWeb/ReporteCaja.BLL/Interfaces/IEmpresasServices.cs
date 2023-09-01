using ReporteCaja.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReporteCaja.BLL.Interfaces
{
    public interface IEmpresasServices
    {
        Task<List<GeneralEmpresa>> Lista();
        Task<int> UltimoId();
        Task<GeneralSucursale> ObtenerDatoSucursal(int idSucursal);
        Task<GeneralEmpresa> ObtenerDatoEmpresa(int idEmpresa);
        Task<string> ObtenerNombreEmpresa(int id);
    }
}
