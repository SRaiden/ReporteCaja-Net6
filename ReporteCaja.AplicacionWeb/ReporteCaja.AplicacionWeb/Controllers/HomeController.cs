using AutoMapper;
using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReporteCaja.AplicacionWeb.Models;
using ReporteCaja.AplicacionWeb.Models.ViewModels;
using ReporteCaja.AplicacionWeb.Models.ViewModels.SendForm.Caja;
using ReporteCaja.AplicacionWeb.Models.ViewModels.SendForm.CierreZ;
using ReporteCaja.AplicacionWeb.Models.ViewModels.SendForm.FormasPago;
using ReporteCaja.AplicacionWeb.Models.ViewModels.SendForm.Productos;
using ReporteCaja.AplicacionWeb.Models.ViewModels.SendForm.TicketsDiarios;
using ReporteCaja.BLL.Implementacion;
using ReporteCaja.BLL.Interfaces;
using ReporteCaja.Entity;
using System.Diagnostics;
using System.Security.Claims;
using System.Xml.Linq;

namespace ReporteCaja.AplicacionWeb.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ICajaUsuariosServices _cajaUsuariosServices;
        private readonly IConverter _converter;
        private readonly ICajaReporteServices _cajaReporteServices;
        private readonly IEmpresasServices _empresasServices;
        private readonly ISucursalesServices _sucursalServices;

        public HomeController(IMapper mapper, ICajaUsuariosServices cajaUsuariosServices, IConverter converter,
                            ICajaReporteServices cajaServices, IEmpresasServices empresasServices, ISucursalesServices sucursalServices)
        {
            _mapper = mapper;
            _cajaUsuariosServices = cajaUsuariosServices;
            _converter = converter;
            _cajaReporteServices = cajaServices;
            _empresasServices = empresasServices;
            _sucursalServices = sucursalServices;
        }

        public async Task<IActionResult> Index()
        {
            // PARA OPCIONES DE QUE MOSTRAR EN EL LOGIN
            ClaimsPrincipal claimPrin = HttpContext.User;
            string idUser = "";
            string nombreUser = "";
            string IdSucursal = "";
            string idEmpresa = "";
            string rol = "";

            if (claimPrin.Identity.IsAuthenticated) // se logeo??
            {

                // OBTENGO ID USER AL LOGEAR POR MEDIO DEL CLAIMPS
                idUser = claimPrin.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).Select(c => c.Value).SingleOrDefault();

                // OBTENGO SU NOMBRE
                nombreUser = claimPrin.Claims.Where(c => c.Type == ClaimTypes.Name).Select(c => c.Value).SingleOrDefault();

                // OBTENGO IDSUCURSAL
                IdSucursal = claimPrin.Claims.Where(c => c.Type == ClaimTypes.Surname).Select(c => c.Value).SingleOrDefault();

                // OBTENGO ROL (0 -> ADMIN, 1 -> USER)
                rol = claimPrin.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).SingleOrDefault();

                // Busco el IDEMPRESA (ESTO ES PARA SUPERADMIN)
                CajaUsuario datoUsuario = await _cajaUsuariosServices.DatoUsuario(Int32.Parse(idUser));
                idEmpresa = datoUsuario.EmpresaId.ToString();
            }

            ViewBag.Elegir = false;
            ViewBag.user = idUser;
            ViewBag.nombreUser = nombreUser;
            ViewBag.idSucursalLogin = IdSucursal;
            ViewBag.idEmpresaLogin = idEmpresa;
            ViewBag.rolLogin = rol;

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodasEmpresas()
        {
            List<VMGeneralEmpresa> vmListaTipoDocumento = _mapper.Map<List<VMGeneralEmpresa>>(await _empresasServices.Lista());
            return StatusCode(StatusCodes.Status200OK, vmListaTipoDocumento);
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerSucursalEmpresa(int idEmpresa)
        {
            List<VMGeneralSucursales> vmListaTipoDocumento = _mapper.Map<List<VMGeneralSucursales>>(await _sucursalServices.Lista(idEmpresa));
            return StatusCode(StatusCodes.Status200OK, vmListaTipoDocumento);
        }

        [HttpPost]
        public async Task<IActionResult> ObtenerTabla([FromBody] VMPedidos modelo)
        {
            ClaimsPrincipal claimPrin = HttpContext.User;
            string idUser = "";

            if (claimPrin.Identity.IsAuthenticated) // se logeo??
            {
                // OBTENGO ID USER AL LOGEAR POR MEDIO DEL CLAIMPS
                idUser = claimPrin.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).Select(c => c.Value).SingleOrDefault();
            }

            if (modelo.tipo == "caja")
            {
                // Obtener Cabecera
                List<VMCajaCierreCaja> vmCajaCierreCaja = null;
                vmCajaCierreCaja = _mapper.Map<List<VMCajaCierreCaja>>(await _cajaReporteServices.ObtenerCabeceraCierreCaja(
                                                    Int32.Parse(modelo.idEmpresa.ToString()), Int32.Parse(modelo.idSucursal.ToString()), modelo.FechaDesde, modelo.FechaHasta));

                // Obtener Detalles
                List<VMCajaDetalleCaja> vmCajaDetalleCaja = new List<VMCajaDetalleCaja>();
                foreach (var i in vmCajaCierreCaja) {
                    List<VMCajaDetalleCaja> temp = _mapper.Map<List<VMCajaDetalleCaja>>(await _cajaReporteServices.ObtenerDetallesCierreCaja(Int32.Parse(i.NroCierre.ToString())));
                    foreach (var b in temp) {  // POR CADA ITEM LE AGREGAMOS LA CABECERA
                        b.VMCajaCierreCaja = i;
                        b.VMCajaCierreCaja.Tipo = "CAJA";
                    }
                    vmCajaDetalleCaja.AddRange(temp);
                }

                // OPERACIONES
                List<SFCajaCierreCaja> sfccc = await OperacionCaja(vmCajaDetalleCaja, Int32.Parse(modelo.idSucursal.ToString()));

                // enviar datos al front-end
                return StatusCode(StatusCodes.Status200OK, sfccc);

            }
            else if (modelo.tipo == "productos") {

                // OBTENER DATOS PRODUCTOS PERIODO
                List<VMProductosPeriodo> vmProductosPeriodo = null;
                vmProductosPeriodo = _mapper.Map<List<VMProductosPeriodo>>(await _cajaReporteServices.ObtenerProductosPeriodo(
                                                    Int32.Parse(modelo.idEmpresa.ToString()), Int32.Parse(modelo.idSucursal.ToString()), modelo.FechaDesde, modelo.FechaHasta));

                // OPERACION PRODUCTO PERIODO
                SFListaProductos sfp = await OperacionProductos(vmProductosPeriodo, Int32.Parse(modelo.idSucursal.ToString()));

                // enviar datos al front-end
                return StatusCode(StatusCodes.Status200OK, sfp);

            }
            else if (modelo.tipo == "tickets") {

                // OBTENER DATOS TICKETS DIARIOS
                List<VMCajaTicketsDiarios> vmCajaTicketsDiarios = null;
                vmCajaTicketsDiarios = _mapper.Map<List<VMCajaTicketsDiarios>>(await _cajaReporteServices.ObtenerTicketsDiarios(
                                                    Int32.Parse(modelo.idEmpresa.ToString()), Int32.Parse(modelo.idSucursal.ToString()), modelo.FechaDesde, modelo.FechaHasta));

                // OPERACION TICKETS DIARIOS
                SFTicketsDiarios sft = await OperacionTicketsDiarios(vmCajaTicketsDiarios, Int32.Parse(modelo.idSucursal.ToString()));

                // enviar datos al front-end
                return StatusCode(StatusCodes.Status200OK, sft);

            }
            else if (modelo.tipo == "formasPago") {

                // OBTENER DATOS CAJA DETALLE FORMAS PAGO
                List<VMCajaDetalleCaja> vmCajaFormasPago = null;
                vmCajaFormasPago = _mapper.Map<List<VMCajaDetalleCaja>>(await _cajaReporteServices.ObtenerFormasPago(
                                                    Int32.Parse(modelo.idEmpresa.ToString()), Int32.Parse(modelo.idSucursal.ToString()), modelo.FechaDesde, modelo.FechaHasta));

                // OPERACION FORMAS PAGO
                SFFormasPago sft = await OperacionFormasPago(vmCajaFormasPago, Int32.Parse(modelo.idSucursal.ToString()));

                // enviar datos al front-end
                return StatusCode(StatusCodes.Status200OK, sft);
            }
            else {

                // OBTENER DATOS CIERREZ
                List<VMCierreZ> vmCierreZ = null;
                vmCierreZ = _mapper.Map<List<VMCierreZ>>(await _cajaReporteServices.ObtenerCierreZ(
                                                    Int32.Parse(modelo.idEmpresa.ToString()), Int32.Parse(modelo.idSucursal.ToString()), modelo.FechaDesde, modelo.FechaHasta));

                // OPERACIONES CIERREZ
                SFListaCierreZ sfcz = await OperacionCierreZ(vmCierreZ, Int32.Parse(modelo.idSucursal.ToString()));

                // enviar datos al front-end
                return StatusCode(StatusCodes.Status200OK, sfcz);
            }
        }

        //-------------------------------------- PDF -------------------------------------------------//

        public IActionResult MostrarPDFPedido(int elemento, string model)
        {
            string urlPlantillaVista = "";
            if (elemento == 1)
                urlPlantillaVista = $"{this.Request.Scheme}://{this.Request.Host}/Plantilla/PDFCaja?model={model}";
            else if(elemento == 2)
                urlPlantillaVista = $"{this.Request.Scheme}://{this.Request.Host}/Plantilla/PDFProductos?model={model}";
            else if(elemento == 3)
                urlPlantillaVista = $"{this.Request.Scheme}://{this.Request.Host}/Plantilla/PDFTickets?model={model}";
            else if(elemento == 4)
                urlPlantillaVista = $"{this.Request.Scheme}://{this.Request.Host}/Plantilla/PDFFormasPago?model={model}";
            else
                urlPlantillaVista = $"{this.Request.Scheme}://{this.Request.Host}/Plantilla/PDFCierreZ?model={model}";

            var pdf = new HtmlToPdfDocument()
            {
                GlobalSettings = new GlobalSettings()
                {
                    PaperSize = PaperKind.A4,
                    Orientation = Orientation.Portrait
                },
                Objects = {
                    new ObjectSettings(){
                        Page = urlPlantillaVista
                    }
                }
            };

            var archivoPDF = _converter.Convert(pdf);
            return File(archivoPDF, "application/pdf");

        }

        //---------------------------------------------------------------------------------------//

        private async Task<List<SFCajaCierreCaja>> OperacionCaja(List<VMCajaDetalleCaja> vmCajaDetalleCaja, int idSucursal) {

            decimal TotalFinalCaja = 0;
            List<SFCajaCierreCaja> sfccc = new List<SFCajaCierreCaja>();
            foreach (var caja in vmCajaDetalleCaja)
            {
                SFCajaCabeceraCaja cabecera = new SFCajaCabeceraCaja();
                SFCajaCierreCaja element = new SFCajaCierreCaja();

                // cabecera
                cabecera.FechaCabecera = caja.VMCajaCierreCaja.Fecha.ToString();
                cabecera.HoraCabecera = caja.VMCajaCierreCaja.Hora.ToString();
                cabecera.NroCierreCabecera = Int32.Parse(caja.VMCajaCierreCaja.NroCierre.ToString());


                // detalles
                List<SFCajaDetalleCaja> sfcdc = new List<SFCajaDetalleCaja>();
                decimal totCierre = 0;
                foreach (var det in vmCajaDetalleCaja)
                {
                    if (det.NroCierre == cabecera.NroCierreCabecera)
                    {
                        SFCajaDetalleCaja sf = new SFCajaDetalleCaja();
                        sf.DescripcionDetalle = det.Descripcion;
                        sf.NroCierreDetalle = Int32.Parse(det.NroCierre.ToString());
                        sf.TotalDetalle = decimal.Parse(det.Total.ToString());
                        totCierre += decimal.Parse(det.Total.ToString());
                        TotalFinalCaja += decimal.Parse(det.Total.ToString());
                        sfcdc.Add(sf);
                    }
                }
                cabecera.TotalXCierreDetalle = totCierre;

                GeneralSucursale suc = _mapper.Map<GeneralSucursale>(await _sucursalServices.ObtenerDatosSucursal(idSucursal));
                element.nombreSucursal = suc.NombreSucursal.Substring(0, suc.NombreSucursal.Length -3);

                // UNIR AL MASTER
                if (sfcdc != null) element.sfCajaDetalleCaja = sfcdc;
                element.sfCajaCabeceraCaja = cabecera;
                sfccc.Add(element);
            }

            if (sfccc.Count != 0) {
                sfccc[0].TotalFinalCaja = TotalFinalCaja;
            }
            

            return sfccc;
        }

        private async Task<SFListaProductos> OperacionProductos(List<VMProductosPeriodo> vmProductosPeriodo, int idSucursal)
        {
            int cantidadTotal = 0;
            decimal total = 0;
            SFListaProductos lista = new SFListaProductos();

            // OPERACIONES
            List<SFProductos> sfp = new List<SFProductos>();
            foreach (var it in vmProductosPeriodo)
            {
                SFProductos element = new SFProductos();
                if (sfp.Count == 0)
                {
                    element.Codigo = Int32.Parse(it.Codigo.ToString());
                    element.Nombre = it.Nombre;

                    string[] cortar = it.Cantidad.ToString().Split(',');
                    element.Cantidad = Int32.Parse(cortar[0]); // quien guarda una cantidad en formato decimal?? lpm
                    element.SubTotal = decimal.Parse(it.Total.ToString());
                }
                else
                {
                    foreach (var q in sfp)
                    {
                        if (q.Codigo == it.Codigo)
                        {
                            element.Codigo = Int32.Parse(it.Codigo.ToString());
                            element.Nombre = it.Nombre;

                            string[] cortar = it.Cantidad.ToString().Split(',');
                            element.Cantidad = Int32.Parse(cortar[0]) + q.Cantidad;
                            element.SubTotal = decimal.Parse(it.Total.ToString()) + q.SubTotal;

                            sfp.Remove(q);
                            break;
                        }
                        else
                        {
                            element.Codigo = Int32.Parse(it.Codigo.ToString());
                            element.Nombre = it.Nombre;

                            string[] cortar = it.Cantidad.ToString().Split(',');
                            element.Cantidad = Int32.Parse(cortar[0]);
                            element.SubTotal = decimal.Parse(it.Total.ToString());
                        }
                    }
                }


                sfp.Add(element);
            }
            sfp.OrderBy(d => d.Codigo).ToList();

            //----------------------------------------------------------//
            GeneralSucursale suc = _mapper.Map<GeneralSucursale>(await _sucursalServices.ObtenerDatosSucursal(idSucursal));
            lista.NombreSucursal = suc.NombreSucursal.Substring(0, suc.NombreSucursal.Length - 3);

            foreach (var element in sfp) { total += element.SubTotal; cantidadTotal += element.Cantidad; }
            lista.CantidadTotal = cantidadTotal;
            lista.Total = total;
            lista.Productos = sfp;
            //----------------------------------------------------------//

            return lista;
        }

        private async Task<SFTicketsDiarios> OperacionTicketsDiarios(List<VMCajaTicketsDiarios> vmCajaTicketsDiarios, int idSucursal)
        {
            SFTicketsDiarios sft = new SFTicketsDiarios();

            List<SFDetallesTicketsDiarios> sfdt = new List<SFDetallesTicketsDiarios>();
            foreach (var ticket in vmCajaTicketsDiarios)
            {
                SFDetallesTicketsDiarios sfTicket = new SFDetallesTicketsDiarios();
                sfTicket.NroCierre = Int32.Parse(ticket.NroCierre.ToString());
                sfTicket.Fecha = ticket.Fecha.ToString().Substring(0, 10);
                sfTicket.Cantidad = Int32.Parse(ticket.Cantidad.ToString());
                sfTicket.Total = decimal.Parse(ticket.Total.ToString());

                sfTicket.CantidadMostrador = Int32.Parse(ticket.CantidadMostrador.ToString());
                sfTicket.TotalMostrador = decimal.Parse(ticket.TotalMostrador.ToString());

                sfTicket.CantidadDelivery = Int32.Parse(ticket.CantidadDelivery.ToString());
                sfTicket.TotalDelivery = decimal.Parse(ticket.TotalDelivery.ToString());
                sfdt.Add(sfTicket);
            }

            foreach (var iterador in vmCajaTicketsDiarios)
            {
                sft.CantidadDeliveryTotal += Int32.Parse(iterador.CantidadDelivery.ToString());
                sft.TotalDeliveryTotal += decimal.Parse(iterador.TotalDelivery.ToString());

                sft.CantidadMostradorTotal += Int32.Parse(iterador.CantidadMostrador.ToString());
                sft.TotalMostradorTotal += decimal.Parse(iterador.TotalMostrador.ToString());

                sft.CantidadTotal += Int32.Parse(iterador.Cantidad.ToString());
                sft.TotalTotal += decimal.Parse(iterador.Total.ToString());
            }

            //------------------------------------------------------//
            GeneralSucursale suc = _mapper.Map<GeneralSucursale>(await _sucursalServices.ObtenerDatosSucursal(idSucursal));
            sft.NombreSucursal = suc.NombreSucursal.Substring(0, suc.NombreSucursal.Length - 3);
            //------------------------------------------------------//

            sft.SFDetallesTicketsDiarios = sfdt;
            return sft;
        }

        private async Task<SFFormasPago> OperacionFormasPago(List<VMCajaDetalleCaja> vmCajaFormasPago, int idSucursal)
        {
            SFFormasPago sffp = new SFFormasPago();

            List<SFCajaDetalleFormaPago> sfcdc = new List<SFCajaDetalleFormaPago>();
            foreach(var ite in vmCajaFormasPago)
            {
                SFCajaDetalleFormaPago sfcd = new SFCajaDetalleFormaPago();
                bool crearElemento = false;

                foreach (var rec in sfcdc)
                {
                    if(rec.Descripcion == ite.Descripcion)
                    {
                        sfcd.Descripcion += rec.Descripcion;
                        sfcd.Cantidad = Int32.Parse(rec.Cantidad.ToString()) + Int32.Parse(ite.Cantidad.ToString());
                        sfcd.Total = decimal.Parse(rec.Total.ToString()) + decimal.Parse(ite.Total.ToString());

                        sfcdc.Add(sfcd);
                        sfcdc.Remove(rec);
                        crearElemento = false;
                        break;
                    }
                    else
                    {
                        crearElemento = true;
                    }
                }

                if (crearElemento && ite.Descripcion.Contains("VENTAS") || sfcdc.Count == 0 && ite.Descripcion.Contains("VENTAS"))
                {
                    sfcd.Descripcion = ite.Descripcion;
                    sfcd.Cantidad = Int32.Parse(ite.Cantidad.ToString());
                    sfcd.Total = decimal.Parse(ite.Total.ToString());
                    sfcdc.Add(sfcd);
                }
            }

            // CALCULAR CANTIDAD TOTAL Y TOTAL
            foreach (var it in sfcdc)
            {
                sffp.Cantidad += it.Cantidad;
                sffp.Total += it.Total;
            }

            //------------------------------------------------------//
            GeneralSucursale suc = _mapper.Map<GeneralSucursale>(await _sucursalServices.ObtenerDatosSucursal(idSucursal));
            sffp.NombreSucursal = suc.NombreSucursal.Substring(0, suc.NombreSucursal.Length - 3);
            //------------------------------------------------------//

            sffp.SFCajaDetalleFormaPago = sfcdc;
            return sffp;

        }

        private async Task<SFListaCierreZ> OperacionCierreZ(List<VMCierreZ> vmCierreZ, int idSucursal)
        {
            SFListaCierreZ sfLista = new SFListaCierreZ();

            List<SFCierreZ> sfcz = new List<SFCierreZ>();
            foreach (var cierreZ in vmCierreZ)
            {
                SFCierreZ sfc = new SFCierreZ();
                sfc.Numero = Int32.Parse(cierreZ.Numero.ToString());
                sfc.Fecha = cierreZ.Fecha.ToString().Substring(0, 10);

                sfc.T_Emitido = cierreZ.TicketsCantidadEmitidos.ToString();
                sfc.T_Cancelado = cierreZ.TicketsCantidadCancelados.ToString();
                sfc.T_Gravado = decimal.Parse(cierreZ.TicketsTotalGravado.ToString());
                sfc.T_IVA = decimal.Parse(cierreZ.TicketsTotalIva.ToString());
                sfc.T_NoGrabado = decimal.Parse(cierreZ.TicketsTotalNoGravado.ToString());
                sfc.T_TotalTributos = decimal.Parse(cierreZ.TicketsTotalTributos.ToString());
                sfc.T_Total = decimal.Parse(cierreZ.TicketsTotal.ToString());
                sfc.T_Exento = decimal.Parse(cierreZ.TicketsTotalExcento.ToString());

                sfc.NC_Emitido = cierreZ.NcCantidadEmitidas.ToString();
                sfc.NC_Cancelado = cierreZ.NcCantidadCanceladas.ToString();
                sfc.NC_Gravado = decimal.Parse(cierreZ.NcTotalGravado.ToString());
                sfc.NC_IVA = decimal.Parse(cierreZ.NcTotalIva.ToString());
                sfc.NC_NoGrabado = decimal.Parse(cierreZ.NcTotalNoGravado.ToString());
                sfc.NC_TotalTributos = decimal.Parse(cierreZ.NcTotalTributos.ToString());
                sfc.NC_Total = decimal.Parse(cierreZ.NcTotal.ToString());
                sfc.NC_Exento = decimal.Parse(cierreZ.NcTotalExcento.ToString());

                sfc.Tipo = "CIERREZ";

                sfcz.Add(sfc);
            }

            //------------------------------------------------------//
            GeneralSucursale suc = _mapper.Map<GeneralSucursale>(await _sucursalServices.ObtenerDatosSucursal(idSucursal));
            sfLista.NombreSucursal = suc.NombreSucursal.Substring(0, suc.NombreSucursal.Length - 3);
            sfLista.sfListaCierreZ = sfcz;
            //------------------------------------------------------//

            return sfLista;
        }
    }
}