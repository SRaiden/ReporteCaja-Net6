using ReporteCaja.AplicacionWeb.Models.ViewModels;
using ReporteCaja.Entity;
using System.Globalization;
using AutoMapper;
using Azure;

namespace ReporteCaja.AplicacionWeb.Utilidades.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() {

            #region GeneralEmpresa
            CreateMap<GeneralEmpresa, VMGeneralEmpresa>()
                    .ForMember(destino =>
                    destino.Confirmar,
                    opt => opt.MapFrom(origen => origen.Confirmar == true ? 1 : 0)
                    );

            CreateMap<VMGeneralEmpresa, GeneralEmpresa>()
                .ForMember(destino =>
                destino.Confirmar,
                opt => opt.MapFrom(origen => origen.Confirmar == 1 ? true : false)
                );
            #endregion

            #region GeneralSucursales
            CreateMap<GeneralSucursale, VMGeneralSucursales>()
                    .ForMember(destino =>
                    destino.Activa,
                    opt => opt.MapFrom(origen => origen.Activa == true ? 1 : 0)
                    );

            CreateMap<VMGeneralSucursales, GeneralSucursale>()
                .ForMember(destino =>
                destino.Activa,
                opt => opt.MapFrom(origen => origen.Activa == 1 ? true : false)
                );
            #endregion

            #region Caja_Usuarios
            CreateMap<CajaUsuario, VMCajaUsuario>()
                        .ForMember(destino =>
                        destino.Activo,
                        opt => opt.MapFrom(origen => origen.Activo == true ? 1 : 0)
                        );

            CreateMap<VMCajaUsuario, CajaUsuario>()
                .ForMember(destino =>
                destino.Activo,
                opt => opt.MapFrom(origen => origen.Activo == 1 ? true : false)
                );
            #endregion

            //---------------------------//

            #region Caja_Cierre_Caja
            CreateMap<CajaCierresCaja, VMCajaCierreCaja>()
                    .ForMember(destino =>
                    destino.Fecha,
                    opt => opt.MapFrom(origen => origen.Fecha.Value.ToString("dd-MM-yyyy"))
                    ).ForMember(destino =>
                    destino.Hora,
                    opt => opt.MapFrom(origen => origen.Hora.Value.ToString("dd-MM-yyyy"))
                    );

            CreateMap<VMCajaCierreCaja, CajaCierresCaja>()
                    .ForMember(destino =>
                    destino.Fecha,
                    opt => opt.MapFrom(origen => DateTime.Parse(origen.Fecha))
                    ).ForMember(destino =>
                    destino.Hora,
                    opt => opt.MapFrom(origen => DateTime.Parse(origen.Hora))
                    );
            #endregion

            #region Caja_Cierre_Caja_Detalle
            CreateMap<CajaDetalleCierreCaja, VMCajaDetalleCaja>()
                    .ForMember(destino =>
                    destino.Fecha,
                    opt => opt.MapFrom(origen => origen.Fecha.Value.ToString("dd-MM-yyyy"))
                    );

            CreateMap<VMCajaDetalleCaja, CajaDetalleCierreCaja>()
                    .ForMember(destino =>
                    destino.Fecha,
                    opt => opt.MapFrom(origen => DateTime.Parse(origen.Fecha))
                    );
            #endregion

            #region ProductosPeriodo
            CreateMap<CajaReporteVenta, VMProductosPeriodo>()
                    .ForMember(destino =>
                    destino.Fecha,
                    opt => opt.MapFrom(origen => origen.Fecha.Value.ToString("dd-MM-yyyy"))
                    );

            CreateMap<VMProductosPeriodo, CajaReporteVenta>()
                    .ForMember(destino =>
                    destino.Fecha,
                    opt => opt.MapFrom(origen => DateTime.Parse(origen.Fecha))
                    );
            #endregion

            #region TicketsDiarios
            CreateMap<CajaTicketsDiario, VMCajaTicketsDiarios>()
                    .ForMember(destino =>
                    destino.Fecha,
                    opt => opt.MapFrom(origen => origen.Fecha.Value.ToString("dd-MM-yyyy"))
                    );

            CreateMap<VMCajaTicketsDiarios, CajaTicketsDiario>()
                    .ForMember(destino =>
                    destino.Fecha,
                    opt => opt.MapFrom(origen => DateTime.Parse(origen.Fecha))
                    );
            #endregion

            #region CierreZ
            CreateMap<CajaCierresz, VMCierreZ>()
                    .ForMember(destino =>
                    destino.Fecha,
                    opt => opt.MapFrom(origen => origen.Fecha.Value.ToString("dd-MM-yyyy"))
                    );

            CreateMap<VMCierreZ, CajaCierresz>()
                    .ForMember(destino =>
                    destino.Fecha,
                    opt => opt.MapFrom(origen => DateTime.Parse(origen.Fecha))
                    );
            #endregion

        }

    }
}
