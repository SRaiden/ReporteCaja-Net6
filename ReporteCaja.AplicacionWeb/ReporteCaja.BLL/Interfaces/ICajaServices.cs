using ReporteCaja.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReporteCaja.BLL.Interfaces
{
    public interface IPedidosServices
    {
        Task<List<FabricaPedido>> ObtenerDatos(int iduser, int idSucursal, int idEmpresa, string tipo, string fechaDesde, string fechaHasta);
       
        Task<List<FabricaPedidosDetalle>> VerDetallePedido(int idPedido);
        Task<List<FabricaPedidosDetalle>> ObtenerCodigoRealProducto(int idPedido);
        Task<string> ObtenerNombreCategoriaProducto(int codigoProducto, string tipoProducto, int idEmpresa);
        Task<FabricaPedido> VerCabeceraPedido(int idPedido);
        Task<bool> Eliminar(int idPedido);
    }
}
