# Asp.Net Core 7 (.NET 7) | True Ultimate Guide

- [Asp.Net Core 7 (.NET 7) | True Ultimate Guide](#aspnet-core-7-net-7--true-ultimate-guide)
- [Dotnet CLI](#dotnet-cli)
- [Middleware](#middleware)
  - [Middleware Class](#middleware-class)
  - [UseWhen](#usewhen)
- [Controllers](#controllers)
  - [Controller naming convention](#controller-naming-convention)
  - [ControllerBase class](#controllerbase-class)


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
- However it is not ncessory to add **Controller** suffix. We can also add `[Controller]` attribute at the top of the class to let .NET know it is a controller.
  - example: 
  ```
  [Controller]
  class Student {} 
  ```
## ControllerBase class
  - Controller class can be derived from `ControllerBase` class
  - This allows to use additional/simplified functionality like shorthand methods