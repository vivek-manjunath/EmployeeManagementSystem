using System.Net;
using System;
namespace EmployeeManagementSystem.Errors
{
    public class RestException : Exception
    {
        public HttpStatusCode StatusCode { get; set; }
        public object Errors { get; set; }

        public RestException(HttpStatusCode statusCode, object errors)
        {
            this.Errors = errors;
            this.StatusCode = statusCode;
        }
    }
}