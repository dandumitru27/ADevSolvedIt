# Angular 2+ rewrite old AngularJS links, fragment problems

Author: Dan Dumitru; Created: October 22, 2020; Last Edit: October 22, 2020  
Tags: Angular,AngularJS; Views: 81

## Problem

Many projects are being migrated from AngularJS to Angular 2+. While doing such a migration, I had to make sure that links to the older AngularJS app are correctly redirected / rewritten to the new structure of the Angular 10 app.

So I needed to redirect URLs like:

```css
http://example.com/oldapp/#/createTeams
```

to a new:
```css
http://example.com/newapp/teams/create
```

The URLs for both apps had a base start, `oldapp` and `newapp` are not the actual ones, I'm using them here just as examples.

As we were using IIS to host our apps, the first idea was to do the rewriting in IIS, as rewrite rules in web.config using the URL Rewriter module.

The problem with that is that everything in the URL after the hash (#), the fragment, is not sent to the server, so from `http://example.com/oldapp/#/createTeams` the server sees only `http://example.com/oldapp/`, thus you can't access the rest of the URL to do the proper URL rewriting.

## Solution

The only solution was to make the bulk of the rewriting on the client side.

If you do a redirect on the server, the fragment after the hash is still kept on the client.

So we added a redirect rule on the server to make the requests to the old app go to the new app, on a specific page, with a rule in IIS ([documentation](https://docs.microsoft.com/en-us/iis/extensions/url-rewrite-module/creating-rewrite-rules-for-the-url-rewrite-module)) like:

    <rule name="Redirect old AngularJS URLs">
      <match url="^oldapp" />
      <action type="Redirect" url="newapp/redirect" />
    </rule>

With this in place, the URL `http://example.com/oldapp/#/createTeams` is now redirected to `http://example.com/newapp/redirect#/createTeams`, so it's now hitting the new Angular 10 app and it includes the fragment information (what's after the hash).

Now, in the Angular app, on the redirect page, you can access the fragment with:

```typescript
constructor(private route: ActivatedRoute) { }

ngOnInit(): void {
    const fragment = this.route.snapshot.fragment;
    // this gives you '/createTeams' in this example
}
```

, and do the necessary redirect logic based on your needs.
