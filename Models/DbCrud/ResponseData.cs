using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DbCrud
{
    public class ResponseData<T>
    {
        public T? Data { get; set; }
        public Response? Response { get; set; }
    }

    public class Response
    {
        public string Message { get; set; } = string.Empty;
        public bool Success { get; set; } = false;
    }
}
