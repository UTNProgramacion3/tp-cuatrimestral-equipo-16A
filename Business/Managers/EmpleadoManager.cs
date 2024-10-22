using Business.Interfaces;
using DataAccess;
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
        private readonly DBManager _DBManager;
        private readonly Response<Empleado> _response;
        public EmpleadoManager(DBManager manager, Response<Empleado> response) 
        { 
            _DBManager = manager;
            _response = response;
        }

        public Empleado Crear(Empleado entity)
        {
            throw new NotImplementedException();

        }

        public bool Eliminar(int id)
        {
            throw new NotImplementedException();
        }

        public Empleado ObtenerPorId(int id)
        {
            throw new NotImplementedException();
        }

        public List<Empleado> ObtenerTodos()
        {
            throw new NotImplementedException();
        }

        public bool Update(Empleado entity)
        {
            throw new NotImplementedException();
        }
    }
}
