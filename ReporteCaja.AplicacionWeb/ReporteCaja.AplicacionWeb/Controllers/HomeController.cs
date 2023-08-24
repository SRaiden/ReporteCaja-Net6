using AutoMapper;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Mvc;
using ReporteCaja.AplicacionWeb.Models;
using ReporteCaja.BLL.Implementacion;
using ReporteCaja.BLL.Interfaces;
using ReporteCaja.Entity;
using System.Diagnostics;
using System.Security.Claims;

namespace ReporteCaja.AplicacionWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ICajaUsuariosServices _cajaUsuariosServices;
        private readonly IConverter _converter;
        private readonly ICajaServices _cajaServices;

        public HomeController(IMapper mapper, ICajaUsuariosServices cajaUsuariosServices, IConverter converter, ICajaServices cajaServices)
        {
            _mapper = mapper;
            _cajaUsuariosServices = cajaUsuariosServices;
            _converter = converter;
            _cajaServices = cajaServices;
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
    }
}