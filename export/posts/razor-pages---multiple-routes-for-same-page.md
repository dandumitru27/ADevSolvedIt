# Razor Pages - multiple routes for same page

Author: Dan Dumitru; Created: January 20, 2024; Last Edit: January 21, 2024  
Tags: Razor Pages,ASP.NET; Views: 401

## Problem

In my ASP.NET Razor Pages project I have for my Post entity an Edit page in the Pages folder named `PostEdit.cshtml`.

I want to get to it using the URL `"/posts/edit/{id?}"`, so I have at the top of the page:
```
@page "/posts/edit/{id?}"
```
To avoid code duplication, I want to use the same page also for adding a new Post, having a different route presented to the user, `"/posts/add"`.

But I cannot add multiple routes to a single Razor page, adding a second `@page` directive gives an error.

## Solution

After a lot of googling and trying different things I was able to use a feature that seems to be named Friendly Routes, using `AddPageRoute` from `Conventions` when setting up the Razor Pages in the `Startup.cs` or the `Program.cs` file.


    services.AddRazorPages(options =>
    {
        options.Conventions.AddPageRoute("/PostEdit", "/posts/add");
    });
