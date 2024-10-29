using Business.Interfaces;
using DataAccess;
using DataAccess.Extensions;
using Domain.Entities;
using Domain.Enums;
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
            var emailCorporativo = entity.CrearEmailCorporativo(); //Añadir validación de email existente.
            Usuario usuario = new Usuario
            {
                Email = emailCorporativo,
                Passwordhash = "123456", // Generar una pass por defecto, y enviar por mail a casilla personal
                FechaCreacion = DateTime.Now,
                RolId =(int)RolesEnum.Empleado,
                    

            };
            var usuarioCreado = _usuarioManager.Crear(usuario);
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
