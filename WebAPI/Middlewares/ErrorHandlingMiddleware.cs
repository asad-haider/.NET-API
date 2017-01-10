using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;
using WebAPI.Exceptions;

namespace WebApi.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            if (exception == null) return;
            await WriteExceptionAsync(context, exception).ConfigureAwait(false);
        }

        private static async Task WriteExceptionAsync(HttpContext context, Exception exception)
        {
            var response = context.Response;
            response.ContentType = "application/json";

            BaseException baseException = null;

            if (exception is NotFoundException) baseException = (NotFoundException)exception;
            else if (exception is BadRequestException) baseException = (BadRequestException)exception;

            response.StatusCode = 200;

            var jsonResponse = JsonConvert.SerializeObject(new
            {
                success = false,
                error = new
                {
                    response_message = baseException.ResponseMessage,
                    repsonse_code = baseException.ResponseCode,
                    exception_time = DateTime.Now,
                    application_name = baseException.Source,
                    stack_trace = baseException.Errors
                }
            });

            await response.WriteAsync(jsonResponse).ConfigureAwait(false);
        }
    }
}
