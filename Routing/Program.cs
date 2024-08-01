var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// To enable routing
app.UseRouting();

// To define endpoints with URL
app.UseEndpoints(endpoints =>
{
  endpoints.Map("file/{fileName}.{extension}", async context =>
  {
    string? fileName = Convert.ToString(context.Request.RouteValues["fileName"]);
    string? extension = Convert.ToString(context.Request.RouteValues["extension"]);

    await context.Response.WriteAsync($"In files - {fileName} - {extension}");
  });

  endpoints.Map("a/{parameter}", async context =>
  {
    await context.Response.WriteAsync("from a/{parameter}");
  });

  endpoints.Map("a/b", async context =>
  {
    await context.Response.WriteAsync("from a/b");
  });

  endpoints.Map("a/{b}/c/d", async context =>
  {
    await context.Response.WriteAsync("from a/b/c/d");
  });

  endpoints.Map("a/b/c/d", async context =>
  {
    await context.Response.WriteAsync("from a/b/c");
  });
});

app.Run(async context =>
{
  await context.Response.WriteAsync("Home");
});

app.Run();
