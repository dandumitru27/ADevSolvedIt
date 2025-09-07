# Razor Pages - Generate link rel canonical

Author: Dan Dumitru; Created: February 11, 2023; Last Edit: February 17, 2023  
Tags: Razor Pages,ASP.NET; Views: 115

## Problem

This very website, A Dev Solved It, is an ASP.NET Razor Pages application.

For a given Post page, different URLs work, as long as the first part, with the post id, is the same.

For SEO purposes, I wanted though to set a canonical URL for each post.

I looked around a bit, but I didn't find a simple answer of how to generate a link rel canonical for Razor Pages, most existing results are for ASP.NET MVC. 

## Solution

In my `Post.cshtml`, I added:

```
@{
    var scheme = HttpContext.Request.Scheme;
    var host = HttpContext.Request.Host.Value;

    var canonicalUrl = $"{scheme}://{host}/{Model.Post.Id}/{Model.Post.Slug}";
}

@section Head {
    <link rel="canonical" href="@canonicalUrl" />
}
```

, while having a `Head` section in my `_Layout.cshtml`:

```
<head>
    
    @await RenderSectionAsync("Head", required: false)
</head>
```
