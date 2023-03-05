# Asp.Net Core 7 (.NET 7) | True Ultimate Guide

- [Asp.Net Core 7 (.NET 7) | True Ultimate Guide](#aspnet-core-7-net-7--true-ultimate-guide)
- [Dotnet CLI](#dotnet-cli)
- [Middleware](#middleware)
  - [Middleware Class](#middleware-class)
  - [UseWhen](#usewhen)
- [Controllers](#controllers)
  - [Controller naming convention](#controller-naming-convention)
  - [ControllerBase class](#controllerbase-class)
- [Model Binding](#model-binding)
  - [Force Model Binding](#force-model-binding)
  - [Model Validation](#model-validation)
  - [Input Formatters](#input-formatters)


# Dotnet CLI
To create a new solution
```
dotnet new sln --output <directory>
```

To create a project
```
dotnet new <Template> -o <ProjectName>
```
example: `dotnet new web`


# Middleware
- `app.Run` -> terminating/shortcut middleware that does not forward request to next middleware

- `Use` -> allows next middleware to execute

## Middleware Class
- should implement `IMiddleware`
- need to add this class to service as DI

## UseWhen
- to add middlewares in pipeline based on some condition


# Controllers
- need to add all controllers as a service in `Program.cs`
  - example `builder.Services.UseTransient<MyController>`
  - shortcut way: `builder.AddControllers`

## Controller naming convention
- Controller class should be suffixed by the word Controller with them.
    - example: `StudentController`
- However it is not necessary to add **Controller** suffix. We can also add `[Controller]` attribute at the top of the class to let .NET know it is a controller.
  - example: 
  ```
  [Controller]
  class Student {} 
  ```
## ControllerBase class
  - Controller class can be derived from `ControllerBase` class
  - This allows to use additional/simplified functionality like shorthand methods


# Model Binding
- Happens automatically after routing
- Allows .NET to read incoming data from http request and pass them as args to action methods
- It follows following precedence:
  1. Form Fields
  2. Request Body (e.g. JSON)
  3. Route Data
  4. Query Parameters

## Force Model Binding
- we can override the default precedence of model binding by using attributes before the argument. e.g. `FromRoute`, `FromQuery`, `FromBody`, etc.

## Model Validation
- We can add attributes to model classes for model validations. For example:
```
class Person
{
  [Required] // attribute for required validation
  public string FirstName { get; set; }
  public string LastName { get; set; }
}
```
- To check if model is valid, we use `Model.IsValid`. For example:
```
public IActionResult Create(Person person)
{
  ...
  if (!Model.IsValid)
  {
    return BadRequest("Invalid model");
  }
  ...
}
```

## Input Formatters
- By default ASP.NET core enables JSON serializer, that will automatically parse incoming json data
- To enable XML serialization `builder.Services.AddXmlSerializerFormatters()`