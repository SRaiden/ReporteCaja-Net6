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
    public class CajaReporteServices : ICajaReporteServices
    {
        private readonly ICajaUsuariosServices _cajaUsuarioServices;

        private readonly IGenericRepository<CajaTicketsDiario> _ticketsServices;
        private readonly IGenericRepository<CajaReporteVenta> _cajaReporteServices;
        private readonly IGenericRepository<CajaTicketsNoCf> _ticketsNoCFServices;
        private readonly IGenericRepository<CajaVentaInsumo> _ticketsVentaInsumo;
        private readonly IGenericRepository<CajaCierresz> _cajaCierresZServices;
        private readonly IGenericRepository<CajaCierresCaja> _cajaCierreCajaServices;
        private readonly IGenericRepository<CajaDetalleCierreCaja> _cajaDetalleCierreCajaServices;
        private readonly IGenericRepository<GeneralSucursale> _sucursalRepository;

        public CajaReporteServices(IGenericRepository<CajaTicketsDiario> ticketsServices, IGenericRepository<CajaTicketsNoCf> ticketsNoCFServices,
                            IGenericRepository<CajaVentaInsumo> ticketsVentaInsumo, IGenericRepository<CajaCierresz> cajaCierresZServices,
                            IGenericRepository<CajaCierresCaja> cajaCierreCajaServices, IGenericRepository<GeneralSucursale> sucursalRepository,
                            ICajaUsuariosServices cajaUsuarioServices, IGenericRepository<CajaDetalleCierreCaja> cajaDetalleCierreCajaServices,
                            IGenericRepository<CajaReporteVenta> cajaReporteServices) { 

            _ticketsNoCFServices = ticketsNoCFServices;
            _ticketsServices = ticketsServices;
            _ticketsVentaInsumo = ticketsVentaInsumo;
            _cajaCierreCajaServices = cajaCierreCajaServices;
            _cajaCierresZServices = cajaCierresZServices;
            _sucursalRepository = sucursalRepository;
            _cajaUsuarioServices = cajaUsuarioServices;
            _cajaDetalleCierreCajaServices = cajaDetalleCierreCajaServices;
            _cajaReporteServices = cajaReporteServices;
        }

        public async Task<List<CajaCierresCaja>> ObtenerCabeceraCierreCaja(int idEmpresa, int idSucursal, string fechaDesde, string fechaHasta)
        {
            //idEmpresa = 16;
            //idSucursal = 70;
            DateTime fd = DateTime.Parse(fechaDesde);
            DateTime fh = DateTime.Parse(fechaHasta);

            IQueryable<CajaCierresCaja> query = await _cajaCierreCajaServices.Consultar();
            List<CajaCierresCaja> elementos;
            if(idSucursal == 0)
                elementos = query.Where(i => i.EmpresaId == idEmpresa && (i.Fecha >= fd && i.Fecha <= fh)).ToList();
            else
                elementos = query.Where(i => i.EmpresaId == idEmpresa && i.SucursalId == idSucursal && (i.Fecha >= fd && i.Fecha <= fh)).ToList();

            return elementos;
        }

        public async Task<List<CajaDetalleCierreCaja>> ObtenerDetallesCierreCaja(int nroCierre)
        {
            IQueryable<CajaDetalleCierreCaja> query = await _cajaDetalleCierreCajaServices.Consultar();
            return query.Where(i => i.NroCierre == nroCierre).ToList();
        }

        //----------------------------------------------------------------------------------------------//

        public async Task<List<CajaReporteVenta>> ObtenerProductosPeriodo(int idEmpresa, int idSucursal, string fechaDesde, string fechaHasta)
        {
            //idEmpresa = 16;
            //idSucursal = 70;
            DateTime fd = DateTime.Parse(fechaDesde);
            DateTime fh = DateTime.Parse(fechaHasta);

            IQueryable<CajaReporteVenta> query = await _cajaReporteServices.Consultar();
            List<CajaReporteVenta> elementos;
            if (idSucursal == 0)
                elementos = query.Where(i => i.EmpresaId == idEmpresa && (i.Fecha >= fd && i.Fecha <= fh)).ToList();
            else
                elementos = query.Where(i => i.EmpresaId == idEmpresa && i.SucursalId == idSucursal && (i.Fecha >= fd && i.Fecha <= fh)).ToList();

            return elementos;
        }

        //----------------------------------------------------------------------------------------------//

        public async Task<List<CajaTicketsDiario>> ObtenerTicketsDiarios(int idEmpresa, int idSucursal, string fechaDesde, string fechaHasta)
        {
            //idEmpresa = 16;
            //idSucursal = 70;
            DateTime fd = DateTime.Parse(fechaDesde);
            DateTime fh = DateTime.Parse(fechaHasta);

            IQueryable<CajaTicketsDiario> query = await _ticketsServices.Consultar();
            List<CajaTicketsDiario> elementos;
            if (idSucursal == 0)
                elementos = query.Where(i => i.EmpresaId == idEmpresa && (i.Fecha >= fd && i.Fecha <= fh)).ToList();
            else
                elementos = query.Where(i => i.EmpresaId == idEmpresa && i.SucursalId == idSucursal && (i.Fecha >= fd && i.Fecha <= fh)).ToList();

            return elementos;
        }

        //----------------------------------------------------------------------------------------------//

        public async Task<List<CajaDetalleCierreCaja>> ObtenerFormasPago(int idEmpresa, int idSucursal, string fechaDesde, string fechaHasta)
        {
            //idEmpresa = 16;
            //idSucursal = 70;
            DateTime fd = DateTime.Parse(fechaDesde);
            DateTime fh = DateTime.Parse(fechaHasta);

            IQueryable<CajaDetalleCierreCaja> query = await _cajaDetalleCierreCajaServices.Consultar();
            List<CajaDetalleCierreCaja> elementos;
            if (idSucursal == 0)
                elementos = query.Where(i => i.EmpresaId == idEmpresa && (i.Fecha >= fd && i.Fecha <= fh)).ToList();
            else
                elementos = query.Where(i => i.EmpresaId == idEmpresa && i.SucursalId == idSucursal && (i.Fecha >= fd && i.Fecha <= fh)).ToList();

            return elementos;
        }

        //----------------------------------------------------------------------------------------------//

        public async Task<List<CajaCierresz>> ObtenerCierreZ(int idEmpresa, int idSucursal, string fechaDesde, string fechaHasta)
        {
            //idEmpresa = 16;
            //idSucursal = 138;
            DateTime fd = DateTime.Parse(fechaDesde);
            DateTime fh = DateTime.Parse(fechaHasta);

            IQueryable<CajaCierresz> query = await _cajaCierresZServices.Consultar();
            List<CajaCierresz> elementos;
            if (idSucursal == 0)
                elementos = query.Where(i => i.EmpresaId == idEmpresa && (i.Fecha >= fd && i.Fecha <= fh)).ToList();
            else
                elementos = query.Where(i => i.EmpresaId == idEmpresa && i.SucursalId == idSucursal && (i.Fecha >= fd && i.Fecha <= fh)).ToList();

            return elementos;
        }

    }

}
