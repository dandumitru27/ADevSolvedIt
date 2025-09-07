# Use CommonMark Markdown in Angular 2+

Author: Dan Dumitru; Created: April 20, 2020; Last Edit: September 19, 2020  
Tags: Markdown,Angular; Views: 57

## Problem

CommonMark is a strongly defined variant of Markdown, highly compatible, aimed at resolving rendering ambiguities. It has a reference JavaScript implementation at [https://github.com/commonmark/commonmark.js](https://github.com/commonmark/commonmark.js)

How exactly can the JavaScript library be used in Angular?

## Solution

As the docs say, first install it with `npm`:

    npm install commonmark

In the TypeScript file, import what classes you need from the library, for example:

    import { HtmlRenderer, Parser } from 'commonmark';

Then to use it:

```typescript
const reader = new Parser();
const writer = new HtmlRenderer();
const parsed = reader.parse('Hello *world*');
const result = writer.render(parsed);
````

The key is here to use `Parser` instead of the `commonmark.Parser` found in the docs.
