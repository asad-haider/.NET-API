using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace WebAPI.Exceptions
{
    public class BadRequestException : BaseException
    {
        public BadRequestException(List<KeyValuePair<string, ModelStateEntry>> list)
        {
            ResponseMessage = ExceptionMessages.InvalidRequest;
            ResponseCode = (int) HttpStatusCode.BadRequest;

            var jsonArray = new List<Error>();

            foreach (var item in list)
            {

                List<string> errorArray = new List<string>();

                foreach (var error in item.Value.Errors)
                {
                    errorArray.Add(error.ErrorMessage);
                }

                jsonArray.Add(new Error {
                    Property = item.Key,
                    Errors = errorArray
                });
                                    
            }

            Errors = jsonArray;
        }

        public BadRequestException(string message) : base(message)
        { }
    }

    public class Error
    {
        public string Property;
        public List<string> Errors;
    }
}
