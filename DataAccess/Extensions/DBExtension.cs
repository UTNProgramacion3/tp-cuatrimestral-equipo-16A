using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Extensions
{
    public static class DBExtension
    {
        /// <summary>
        /// Obtiene el id desde DataTable
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int GetId(this DataTable data, bool isNewId=false)
        {
            return (data != null && data.Rows.Count > 0) ?
                 Convert.ToInt32(data.Rows[0][isNewId ? "NewId" : "Id"]) : -1;
        }

        /// <summary>
        /// Obtiene los datos de la entidad
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns>La entidad T</returns>
        public static T GetEntity<T>(this DataTable data, bool create=false) where T : class
        {
            if (data == null ||(create == false &&  data.Rows.Count == 0))
                return null;

            var item = Activator.CreateInstance<T>();

            var properties = typeof(T).GetProperties();

            foreach (var prop in properties)
            {
                if (data.Columns.Contains(prop.Name))
                {
                    var value = data.Rows[0][prop.Name];

                    if (value != DBNull.Value)
                    {
                        prop.SetValue(item, Convert.ChangeType(value, prop.PropertyType));
                    }
                }
            }

            return item;
        }
    }

}
