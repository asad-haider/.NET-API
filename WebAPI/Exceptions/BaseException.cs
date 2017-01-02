using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Exceptions
{
    public class BaseException : Exception
    {
        public int ResponseCode { get; set; }
        public string ResponseMessage { get; set; }
        public string ApplicationName { get; set; }
        public DateTime RequestTime { get; set; }

        public BaseException()
        {
        }

        public BaseException(string message) : base(message)
        {
            ResponseMessage = message;
        }
    }
}
