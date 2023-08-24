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
        }

    }
}
