using Business.Interfaces;
using Domain.Entities;
using Domain.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Managers
{
    public class SedeManager : ISedeManager
    {
        public Response<Sede> Crear(Sede entity)
        {
            throw new NotImplementedException();
        }

        public Response<bool> Eliminar(int id)
        {
            throw new NotImplementedException();
        }

        public Response<Sede> ObtenerPorId(int id)
        {
            throw new NotImplementedException();
        }

        public Response<List<Sede>> ObtenerTodos()
        {
            throw new NotImplementedException();
        }

        public Response<bool> Update(Sede entity)
        {
            throw new NotImplementedException();
        }
    }
}
