using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Exceptions
{
    public class ExceptionMessages
    {
        public static string RequestTimeout = "Request Timed out, Please Try again.";
        public static string NotFound = "Record Not Found.";
        public static string InternalError = "Something went wrong, please contact service administrator.";
    }
}
