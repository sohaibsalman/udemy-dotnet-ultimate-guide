using Microsoft.Extensions.Primitives;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
var app = builder.Build();

app.MapControllers();

app.Run(async (HttpContext context) =>
{
  StreamReader reader = new StreamReader(context.Request.Body);
  string body = await reader.ReadToEndAsync();

  // Converting string to query string
  Dictionary<string, StringValues> queryDict = Microsoft.AspNetCore.WebUtilities.QueryHelpers.ParseQuery(body);
  string firstName = queryDict["firstName"][0]; //same key can have multiple values
});

app.Run();
