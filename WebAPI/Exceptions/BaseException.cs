using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Exceptions
{
    public class BaseException : Exception
    {
        public int response_code { get; set; }
        public string response_message { get; set; }
        public string application_name { get; set; }
        public DateTime request_time { get; set; }

        public BaseException()
        {
        }

        public BaseException(string message) : base(message)
        {
            response_message = message;
        }
    }
}
