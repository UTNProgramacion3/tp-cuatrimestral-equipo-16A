using Business.Interfaces;
using DataAccess;
using DataAccess.Extensions;
using Domain.Entities;
using Domain.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Managers
{
    public class EmpleadoManager : IEmpleadoManager
    {
        #region Private properties
        private readonly DBManager _DBManager;
        private readonly Response<Empleado> _response;
        private readonly IUsuarioManager _usuarioManager;
        #endregion

        #region Builder
        public EmpleadoManager(DBManager manager, Response<Empleado> response, IUsuarioManager usuarioManager) 
        { 
            _DBManager = manager;
            _response = response;
            _usuarioManager = usuarioManager;
        }
        #endregion

        #region Public methods
        public Response<Empleado> Crear(Empleado entity)
        {
            Usuario usuario = new Usuario
            {
                Email = entity.CrearEmailCorporativo(),

            };
            var usuario = _usuarioManager.Crear(entity);
        }

        public Response<bool> Eliminar(int id)
        {
            throw new NotImplementedException();
        }

        public Response<Empleado> ObtenerPorId(int id)
        {
            throw new NotImplementedException();
        }

        public Response<List<Empleado>> ObtenerTodos()
        {
            throw new NotImplementedException();
        }

        public Response<bool> Update(Empleado entity)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Private methods
       
        #endregion
    }
}
