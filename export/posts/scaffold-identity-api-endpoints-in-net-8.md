# Scaffold Identity Api Endpoints in .NET 8

Author: Cristi Jugaru; Created: July 27, 2024; Last Edit: July 27, 2024  
Tags: ASP.NET; Views: 160

## Problem

The Identity implementation in .NET8 adds a new functionality called  MapIdentityApi, which allows the developer to use a set of preconfigured endpoints for authorization management. 


The problem is that we might want to customize the functionality of those endpoints, or even remove the ones which we are not planning to use.

## Solution

We can scaffold the implementation into our solution from the official [AspNetCore repository on Github](https://github.com/dotnet/aspnetcore/blob/main/src/Identity/Core/src/IdentityApiEndpointRouteBuilderExtensions.cs) by simply copying the implementation into our own project, then renaming the class to something different from MapIdentityApi (eg. MapCustomizedIdentityApi) in order to avoid naming conflicts. 

After that we call our extension method inbetween building the web application and running it:
```
var app = builder.Build();

app.MapCustomizedIdentityApi<IdentityUser>();

app.Run();
```


Now we have full control over the implementation of those endpoints.
