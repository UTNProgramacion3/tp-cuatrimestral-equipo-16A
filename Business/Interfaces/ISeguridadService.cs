using Domain.Entities;
using Domain.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface ISeguridadService
    {
        bool TienePermiso(int rolId, string permisoId);
        Response<List<Permiso>> GetPermisosPorRol(int rolId);
        Response<List<Permiso>> GetPermisosPorModulo(int moduloId);
        void InhabilitarToken(string token);
        void VerificarTokensVencidos();

    }
}
