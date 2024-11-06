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
        public static int GetId(this DataTable data)
        {
            if(data != null)
            {
                return data.Rows[0].Field<int>("Id");
            }
            return -1;
        }
        
        /// <summary>
        /// Obtiene los datos de la entidad
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns>La entidad T</returns>
        public static T GetEntity<T>(this DataTable data) where T : class
        {
            if (data == null || data.Rows.Count == 0)
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
