using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ReporteCaja.BLL.Interfaces;
using ReporteCaja.DAL.Interfaces;
using ReporteCaja.Entity;

namespace ReporteCaja.BLL.Implementacion
{
    public class SucursalServices : ISucursalesServices
    {
        private readonly IGenericRepository<GeneralSucursale> _repository;

        public SucursalServices(IGenericRepository<GeneralSucursale> repository)
        {
            _repository = repository;
        }

        public async Task<List<GeneralSucursale>> Lista(int idEmpresa)
        {
            IQueryable<GeneralSucursale> query = await _repository.Consultar();
            return query.Where(i => i.EmpresaId == idEmpresa).ToList();
        }

        public async Task<GeneralSucursale> ObtenerDatosSucursal(int id)
        {
            IQueryable<GeneralSucursale> query = await _repository.Consultar(); // obtengo todas las sucursales
            return query.Where(i => i.Id == id).First();
        }

        public async Task<string> ObtenerNombreSucursal(int id)
        {
            IQueryable<GeneralSucursale> query = await _repository.Consultar(); // obtengo todas las sucursales
            return query.Where(i => i.Id == id).First().NombreSucursal.ToString();
        }
    }
}
