using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace WebAPI.Exceptions
{
    public class NotFoundException : BaseException
    {
        public NotFoundException()
        {
            ResponseMessage = ExceptionMessages.NotFound;
            ResponseCode = (int) HttpStatusCode.NotFound;
        }

        public NotFoundException(string message) : base(message)
        { }
    }
}
