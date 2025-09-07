# ASP.NET Load user secrets.json when environment is not Development

Author: Dan Dumitru; Created: July 27, 2022; Last Edit: July 27, 2022  
Tags: ASP.NET,C#; Views: 630

## Problem

In an ASP.NET project on .NET 6 the user secrets are only loaded when the environment used is `Development`.

From [https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-6.0&tabs=windows](https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-6.0&tabs=windows):

"*`WebApplication.CreateBuilder` initializes a new instance of the `WebApplicationBuilder` class with preconfigured defaults. The initialized `WebApplicationBuilder` (builder) provides default configuration and calls `AddUserSecret`s when the `EnvironmentName` is `Development`.*"

Sometimes you need to run the project locally using a different environment, like `Production`, or anything different from `Development`. By default, the user secrets are not loaded in this situation. What should you do to load them?

## Solution

You can explicitly build the configuration again, including the `appsettings` file for your environment, before the code that needs the values from it, and call the `AddUserSecrets` method. This is how the code would look like for the `Production` environment:

```
var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddJsonFile("appsettings.Production.json")
    .AddUserSecrets(Assembly.GetExecutingAssembly())
    .Build();

var azureKey = configuration["AzureKey"];
```
