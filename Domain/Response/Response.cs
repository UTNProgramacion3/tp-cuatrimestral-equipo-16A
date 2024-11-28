using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Response
{
    public class Response<T> where T : new()
    {
        public T Data { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }

        public Response()
        {
            Data = new T();
            Success = false;
            Message = string.Empty;
        }

        public void Ok(T data, string mensaje = null)
        {
            Data = data;
            Success = true;
            Message = mensaje ?? "Operación exitosa";
        }

        public void NotOk(string mensaje)
        {
            Success = false;
            Message = mensaje;
        }
    }

}
