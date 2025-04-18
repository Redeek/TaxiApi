﻿using System.Diagnostics;
using System.Net;

namespace TaxiApi.Exceptions
{
    public class ErrorHandlingMiddleware: IMiddleware
    {
        private readonly ILogger _logger;

        public ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (NotFoundException notFoundException)
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync(notFoundException.Message);
            }
            catch (BadRequestException badRequestException)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync(badRequestException.Message);
            }
            catch (UserInvalidOperationException invalidOperationException)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync(invalidOperationException.Message);
            }
            catch(Exception e)
            {
                _logger.LogError(e, e.Message);


                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("Something went wrong");
            }
        }
    }
}
