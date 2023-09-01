using ReporteCaja.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReporteCaja.BLL.Interfaces
{
    public interface ICajaReporteServices
    {
        // CAJA
        Task<List<CajaCierresCaja>> ObtenerCabeceraCierreCaja(int idEmpresa, int idSucursal, string fechaDesde, string fechaHasta);

        Task<List<CajaDetalleCierreCaja>> ObtenerDetallesCierreCaja(int idCabecera);

        // CAJA
        Task<List<CajaReporteVenta>> ObtenerProductosPeriodo(int idEmpresa, int idSucursal, string fechaDesde, string fechaHasta);

        // TICKETS DIARIOS
        Task<List<CajaTicketsDiario>> ObtenerTicketsDiarios(int idEmpresa, int idSucursal, string fechaDesde, string fechaHasta);

        // FORMAS PAGO
        Task<List<CajaDetalleCierreCaja>> ObtenerFormasPago(int idEmpresa, int idSucursal, string fechaDesde, string fechaHasta);

        // CIERRE Z
        Task<List<CajaCierresz>> ObtenerCierreZ(int idEmpresa, int idSucursal, string fechaDesde, string fechaHasta);


    }
}
