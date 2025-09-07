# Razor Pages, fetch with POST from JavaScript, no page reload

Author: Dan Dumitru; Created: November 16, 2023; Last Edit: November 17, 2023  
Tags: Razor Pages,JavaScript; Views: 627

## Problem

In my ASP.NET Razor Pages project I want to do a POST request from JavaScript and use the response, without a page reload.

In my page model I have a handler similar to:

```
public async Task<IActionResult> OnPostAddThanks()
{
    // actual work ommited

    var thanksCount = 42; // dummy value

    return new JsonResult(thanksCount);
}
```
I'm trying to call it from JavaScript with the fetch API doing:

```
async function sendThanks() {
    const response = await fetch('?handler=AddThanks', {
        method: 'POST'
    })

    return response.json()
}
```

This is pretty standard and straightforward, but it doesn't work, I get a status code 400, Bad Request, with no hint on what the problem is.

I spent several hours trying different things to get past this.

## Solution

The issue was that I needed to send in the request the anti-forgery token that was automatically generated for Razor Pages.

This page explains it pretty well, follow it to see all the details: [https://www.talkingdotnet.com/handle-ajax-requests-in-asp-net-core-razor-pages/](https://www.talkingdotnet.com/handle-ajax-requests-in-asp-net-core-razor-pages/)

Basically you need to add this in Program.cs / Startup.cs:

```cs
services.AddAntiforgery(o => o.HeaderName = "XSRF-TOKEN");
```

, and then send the "XSRF-TOKEN" in the request headers. There is an example there with jQuery AJAX, here is my version using the fetch API:

```
async function sendThanks() {
    const response = await fetch('?handler=AddThanks', {
        method: 'POST',
        headers: {
            "XSRF-TOKEN": document.querySelector("[name='__RequestVerificationToken']").value
        }
    })

    return response.json()
}
```

