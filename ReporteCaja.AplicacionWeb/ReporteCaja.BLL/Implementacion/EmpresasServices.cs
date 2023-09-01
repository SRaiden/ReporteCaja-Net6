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
    public class EmpresasServices : IEmpresasServices
    {
        private readonly IGenericRepository<GeneralEmpresa> _repository;
        private readonly IGenericRepository<GeneralSucursale> _sucursalRepository;

        public EmpresasServices(IGenericRepository<GeneralEmpresa> repository, IGenericRepository<GeneralSucursale> sucursalRepository)
        {
            _repository = repository;
            _sucursalRepository = sucursalRepository;
        }

        public async Task<List<GeneralEmpresa>> Lista()
        {
            IQueryable<GeneralEmpresa> query = await _repository.Consultar();
            return query.ToList();
        }

        public async Task<GeneralEmpresa> ObtenerDatoEmpresa(int idEmpresa)
        {
            IQueryable<GeneralEmpresa> query = await _repository.Consultar();
            return query.Where(i => i.Id == idEmpresa).First();
        }

        public async Task<GeneralSucursale> ObtenerDatoSucursal(int idSucursal)
        {
            IQueryable<GeneralSucursale> query = await _sucursalRepository.Consultar();
            return query.Where(i => i.Id == idSucursal).First();
        }

        public async Task<string> ObtenerNombreEmpresa(int id)
        {
            IQueryable<GeneralEmpresa> query = await _repository.Consultar(); // obtengo todas las empresas
            return query.Where(i => i.Id == id).First().NombreEmpresa.ToString();
        }

        public async Task<int> UltimoId()
        {
            var query = await _repository.Consultar();
            return Int32.Parse(query.OrderByDescending(d => d.Id).First().Id.ToString());
        }
    }
}
