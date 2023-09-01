﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ReporteCaja.AplicacionWeb.Models.ViewModels;
using ReporteCaja.BLL.Interfaces;

namespace ReporteCaja.AplicacionWeb.Controllers
{
    public class PlantillaController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IEmpresasServices _empresasServices;
        private readonly ICajaReporteServices _CajaServices;

        public PlantillaController(IMapper mapper, ICajaReporteServices CajaServices, IEmpresasServices empresasServices)
        {
            _mapper = mapper;
            _CajaServices = CajaServices;
            _empresasServices = empresasServices;
        }


        //public async Task<IActionResult> PDFCaja(int idPedido)
        //{
        //    // saber que empresa pertenece la sucursal que pidio el producto
        //    VMFabricaPedido pedidoCabecera = _mapper.Map<VMFabricaPedido>(await _pedidosServices.VerCabeceraPedido(idPedido));
        //    VMGeneralSucursales datoSucursal = _mapper.Map<VMGeneralSucursales>(await _empresasServices.ObtenerDatoEmpresa(Int32.Parse(pedidoCabecera.IdSucursal.ToString())));

        //    // obtener codigo y cantidad
        //    List<VMFabricaPedidoDetalle> vmLista = _mapper.Map<List<VMFabricaPedidoDetalle>>(await _pedidosServices.VerDetallePedido(idPedido));

        //    // obtener nombre y categoria - al estar en otras tablas, obtenemos los resultado
        //    List<VMPedido> vmPedido = new List<VMPedido>();
        //    for (int a = 0; a < vmLista.Count; a++)
        //    {
        //        VMPedido vm = new VMPedido();
        //        string resultado = await _pedidosServices.ObtenerNombreCategoriaProducto(Int32.Parse(vmLista[a].Codigo.ToString()), pedidoCabecera.Tipo, Int32.Parse(datoSucursal.EmpresaId.ToString()));
        //        string[] separar = resultado.Split('@');

        //        vm.CodigoDetalle = vmLista[a].Codigo;
        //        vm.CantidadDetalle = vmLista[a].Cantidad;
        //        vm.DescripcionDetalle = separar[0];
        //        vm.CategoriaDetalle = separar[1];
        //        vmPedido.Add(vm);
        //    }

        //    VMPDFPedido modelo = new VMPDFPedido();
        //    modelo.NumeroPedido = pedidoCabecera.NumeroPedido.ToString();
        //    modelo.Estado = pedidoCabecera.Estado;
        //    modelo.Pedido = vmPedido;

        //    return View(modelo);
        //}

    }
}
