using DataAccess;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public class ActionLog
    {
        private readonly DBManager _dbManager;

        public ActionLog()
        {
            _dbManager = new DBManager();   
        }

        public bool RegistrarAccion(int usuarioId, string accion, string detalles)
        {
            string query = @"INSERT INTO Logs (UsuarioId, Accion, Detalles)
                            VALUES (@UsuarioId, @Accion, @Detalles)";

            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@UsuarioId", usuarioId),
                new SqlParameter("@Accion", accion),
                new SqlParameter("@detalles", detalles)
            };

            var res = _dbManager.ExecuteNonQuery(query, parameters);

            return Convert.ToBoolean(res);
        }
    }
}
