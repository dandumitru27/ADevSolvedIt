# Mark .NET Web API endpoint as obsolete / deprecated

Author: Dan Dumitru; Created: August 7, 2024; Last Edit: August 7, 2024  
Tags: ASP.NET; Views: 402

## Problem

How to do this, in order to let developers know not to use it?

It should be clearly visible in Swagger (the default in Web API) too.

## Solution

Just use the normal `Obsolete` attribute in C# on the endpoint method:

```c
[Obsolete("This endpoint is obsolete. Call /WeatherForecastAI instead.")]
[HttpGet(Name = "GetWeatherForecast")]
public IEnumerable<WeatherForecast> Get()
```

This will make the endpoint show up on Swagger as grayed out and with a strike-through:

![endpoint obsolete](https://i.imgur.com/W7RLpBA.png)

I've also tried to use the `Deprecated` property of `ApiVersion`, but it didn't have any effect on Swagger.

*(This is a simple thing, I posted it on A Dev just because I couldn't find something clear in my web searches.)*
