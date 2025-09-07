# Angular 2+ routing - parameter inside URL segment

Author: Dan Dumitru; Created: June 1, 2020; Last Edit: August 26, 2020  
Tags: Angular; Views: 44

## Problem

You can easily have parameters in Angular routes if they are a full segment in the URL, so you can have for the following URLs:

- /music/german
- /music/french

the route:

```
{ path: 'music/:language', component: MusicComponent }
```

, which passes `language` as a parameter to the `MusicComponent`.

How do you handle though parameters that are inside a URL segment, so matching for example the following URLs and getting the `language` parameter from them:

- /learn-german-with-music
- /learn-french-with-music

Doing this DOESN'T work:

```
{ path: 'learn-:language-with-music', component: MusicComponent }
```

## Solution

You can use the Angular UrlMatcher for this - [https://angular.io/api/router/UrlMatcher](https://angular.io/api/router/UrlMatcher)

Basically you write a custom function to match your desired URLs and extract from them the parameter, and then use the function when defining the routes.

For my above example, the routes can end up looking like:

    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { matcher: isMusicRoute, component: MusicComponent }
    ])

, where `isMusicRoute` is my custom function:

```
export function isMusicRoute(urlSegments: UrlSegment[]) {
  if (urlSegments.length !== 1) {
    return null;
  }

  const matches = urlSegments[0].path.match(/learn-(.*)-with-music/);
  if (!matches || matches.length < 2) {
    return null;
  }

  return ({
    consumed: urlSegments,
    posParams: { language: new UrlSegment(matches[1], undefined) }
  });
}
```

The function can be added in the same file as the routing module, before starting the `@NgModule` declaration.
