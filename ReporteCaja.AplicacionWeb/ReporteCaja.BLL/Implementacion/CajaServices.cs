using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using ReporteCaja.BLL.Interfaces;
using ReporteCaja.DAL.Interfaces;
using ReporteCaja.Entity;
using System.Collections;

namespace ReporteCaja.BLL.Implementacion
{
    public class CajaServices : IPedidosServices
    {
        private readonly IGenericRepository<CajaTicketsDiario> _ticketsServices;
        private readonly IGenericRepository<CajaTicketsNoCf> _ticketsNoCFServices;
        private readonly IGenericRepository<CajaVentaInsumo> _ticketsVentaInsumo;
        private readonly IGenericRepository<CajaCierresz> _cajaCierresZServices;
        private readonly IGenericRepository<CajaCierresCaja> _cajaCierreCajaServices;
        private readonly IGenericRepository<GeneralSucursale> _sucursalRepository;

        public CajaServices(IGenericRepository<CajaTicketsDiario> ticketsServices, IGenericRepository<CajaTicketsNoCf> ticketsNoCFServices,
                            IGenericRepository<CajaVentaInsumo> ticketsVentaInsumo, IGenericRepository<CajaCierresz> cajaCierresZServices,
                            IGenericRepository<CajaCierresCaja> cajaCierreCajaServices, IGenericRepository<GeneralSucursale> sucursalRepository) { 
            _ticketsNoCFServices = ticketsNoCFServices;
            _ticketsServices = ticketsServices;
            _ticketsVentaInsumo = ticketsVentaInsumo;
            _cajaCierreCajaServices = cajaCierreCajaServices;
            _cajaCierresZServices = cajaCierresZServices;
            _sucursalRepository = sucursalRepository;
        }

        public Task<List<CajaReporteVenta>> ObtenerPedidos(int iduser, int idSucursal, int idEmpresa, string tipo, string fechaDesde, string fechaHasta)
        {
            throw new NotImplementedException();
        }
    }
}
