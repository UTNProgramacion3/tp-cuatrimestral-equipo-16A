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
        Response<List<Rol>> GetRolesConPermisos();
        void InhabilitarToken(string token);
        //void VerificarTokensVencidos();

        Response<List<Modulo>> GetModulos();

        bool AgregarPermiso(string permiso, int moduloId);

        bool EditarPermiso(int permisoId, string nombre);

        Response<List<Permiso>> GetPermisos();

        bool AsignarPermisoARol(int permisoId, int rolId);

        bool EliminarPermisoDeRol(int permisoId, int rolId);    

    }
}
