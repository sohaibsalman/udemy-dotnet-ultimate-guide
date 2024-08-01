namespace Middleware.Middleware
{
  public class CustomMiddleware : IMiddleware
  {
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
      // before logic
      await next(context);
      // after logic
    }
  }

  public static class CustomMiddlewareExtension
  {
    public static IApplicationBuilder UseCustomMiddleware(this IApplicationBuilder app)
    {
      return app.UseMiddleware<CustomMiddleware>();
    }
  }
}
