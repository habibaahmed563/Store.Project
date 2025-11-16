using Store.Project.Shared.ErrorModels;

namespace Store.Project.Web.Middlewares
{
    public class GlobalErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
               await _next.Invoke(context);
            }
            catch(Exception ex)
            {
                // Logic

                // 1. Set status code of Response
                context.Response.StatusCode = ex switch
                {
                    DllNotFoundException => StatusCodes.Status400BadRequest,
                    _ => StatusCodes.Status500InternalServerError
                };

                // 2. Set Content Type of Response
                context.Response.ContentType = "application/json";

                // 3. Set Body Of Response 
                var responce = new ErrorDetails()
                {
                    StatusCode = context.Response.StatusCode,
                    ErrorMessage = ex.Message
                };

                // Return response 
                await context.Response.WriteAsJsonAsync(responce);
            }
        }
    }
}
