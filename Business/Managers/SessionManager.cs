using System;
using System.Runtime.InteropServices.ComTypes;
using System.Web;

namespace Business.Managers
{
    internal class SessionManager
    {
        public void SetSessionValue(string key, object value)
        {
            if(HttpContext.Current != null )
            {
                HttpContext.Current.Session[key] = value;
            }
        }

        public object GetSessionValue(string key)
        {
            if (HttpContext.Current != null)
            {
                return HttpContext.Current.Session[key];
            }
            return null;
        }


        public void RemoveSessionValue(string key)
        {
            if (HttpContext.Current != null)
            {
                HttpContext.Current.Session.Remove(key);
            }
        }

        public void ClearSession()
        {
            if (HttpContext.Current != null)
            {
                HttpContext.Current.Session.Clear();
            }
        }
    }
}
