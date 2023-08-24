using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ReporteCaja.BLL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Net;
using ReporteCaja.DAL.Interfaces;
using ReporteCaja.Entity;
using System.Security.AccessControl;


namespace ReporteCaja.BLL.Implementacion
{
    public class CajaUsuariosServices : ICajaUsuariosServices
    {
        private readonly IGenericRepository<CajaUsuario> _repository;
        private readonly IUtilidadesServices _utilidadesServices;
        private readonly ICorreoServices _correoServices;

        public CajaUsuariosServices(IGenericRepository<CajaUsuario> repository,
                IUtilidadesServices utilidadesServices, ICorreoServices correoServices)
        {
            _repository = repository;
            _utilidadesServices = utilidadesServices;
            _correoServices = correoServices;
        }

        public async Task<CajaUsuario> ObtenerCredenciales(string correo, string clave)
        {
            CajaUsuario usuarioEncontrado = await _repository.Obtener(u => u.Email.Equals(correo) && u.Contraseña.Equals(clave));

            return usuarioEncontrado;
        }

        public async Task<bool> RestablecerClave(string CorreoDestino, string URLPlantillaCorreo)
        {
            try
            {
                CajaUsuario usuarioEncontrado = await _repository.Obtener(u => u.Email == CorreoDestino);

                if (usuarioEncontrado == null)
                    throw new TaskCanceledException("No existe un usuario con este correo");

                string claveGenerada = _utilidadesServices.GenerarClave();
                usuarioEncontrado.Contraseña = claveGenerada;

                //---//
                URLPlantillaCorreo = URLPlantillaCorreo.Replace("[clave]", claveGenerada);
                string htmlCorreo = "";
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URLPlantillaCorreo);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                // Se pudo conectar?
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    using (Stream dataStream = response.GetResponseStream())
                    {
                        StreamReader readStream = null;

                        // Usa caracteres especiales??
                        if (response.CharacterSet == null)
                            readStream = new StreamReader(dataStream);
                        else
                            readStream = new StreamReader(dataStream, Encoding.GetEncoding(response.CharacterSet));

                        // Limpiar memoria
                        htmlCorreo = readStream.ReadToEnd();
                        response.Close();
                        readStream.Close();
                    }
                }

                bool correoEnviado = false;

                if (htmlCorreo != "")
                    correoEnviado = await _correoServices.EnviarCorreo(CorreoDestino, "Contraseña Restablecida", htmlCorreo);

                if (!correoEnviado)
                    throw new TaskCanceledException("No se pudo enviar el correo. Intente mas tarde");

                bool respuesta = await _repository.Editar(usuarioEncontrado);
                return respuesta;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<CajaUsuario> DatoUsuario(int idUsuario)
        {
            IQueryable<CajaUsuario> query = await _repository.Consultar();
            return query.Where(u => u.Id == idUsuario).First();
        }
    }
}
