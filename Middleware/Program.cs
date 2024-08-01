using Middleware.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<CustomMiddleware>();

var app = builder.Build();

app.Use(async (HttpContext context, RequestDelegate next) =>
{
  await context.Response.WriteAsync("Hello from middleware 1");
  await next(context);
});

//app.UseMiddleware<CustomMiddleware>();
app.UseCustomMiddleware();

app.Run(async (HttpContext context) =>
{
  await context.Response.WriteAsync("Hello from middleware 2");
});


app.Run();
