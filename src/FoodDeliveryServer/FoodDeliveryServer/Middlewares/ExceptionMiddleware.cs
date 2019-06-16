using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace GameStore.API.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                string response = JsonConvert.SerializeObject(
                    new
                    {
                        ExceptionType = ex.GetType().Name,
                        ExceptionMessage = ex.Message,
                        InnerExceptionType = ex.InnerException?.GetType().Name,
                        InnerExceptionMessage = ex.InnerException?.Message
                    });

                httpContext.Response.ContentType = "application/json";
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await httpContext.Response.WriteAsync(response);
            }
        }

    }
}
