# Highlight.js in Angular 2+ on dynamic content

Author: Dan Dumitru; Created: May 14, 2020; Last Edit: September 30, 2020  
Tags: Angular; Views: 128

## Problem

A good option to do syntax highlighting on code is the [highlight.js](https://highlightjs.org/) library, especially because it can automatically detect the programming language used, so you don't need to specify the language on each code block.

The simplest way to use the library is to call the [initHighlightingOnLoad()](https://highlightjs.readthedocs.io/en/latest/api.html#inithighlightingonload) method, which does syntax highlighting on all the code on the page -- that is, code inside `<pre><code>` tags.

Unfortunately, that doesn't work on Angular, as it expects all the code to already be from the start in the HTML of the page.

A work-around for Angular is to call the [highlightBlock(block)](https://highlightjs.readthedocs.io/en/latest/api.html#highlightblock-block) method on each code block, like described in [this Stack Overflow post](https://stackoverflow.com/questions/37307943/highlight-js-does-not-work-with-angular-2).

That works OK for code blocks that are in the static HTML in the template.

You might need it though for HTML content dynamically added to the page, like HTML content retrieved from the database and rendered inside the template. That was also my scenario, so I needed a solution.

## Solution

I managed to get it to work using the [highlightAuto(code, languageSubset)](https://highlightjs.readthedocs.io/en/latest/api.html#highlightauto-code-languagesubset) method on all the code blocks in the HTML content. I created a pipe that finds all the `<pre><code>` blocks, applies the highlighting on them, and then adds them in the HTML content as a replacement. Here is the simplest implementation:

```
import { Pipe, PipeTransform } from '@angular/core';
import hljs from 'highlight.js';

@Pipe({
  name: 'highlightCode'
})
export class HighlightCodePipe implements PipeTransform {
  transform(htmlMarkup: string): string {
    const preCodeRegex = /<pre><code>([\s\S]*?)<\/code><\/pre>/g;

    return htmlMarkup.replace(preCodeRegex, function (_match, p1) {
      const codeBlockHighlighted = hljs.highlightAuto(p1).value;
      return '<pre><code class="hljs">' + codeBlockHighlighted + '</pre></code>';
    });
  }
}
```

With the obvious usage:

```
<div [innerHtml]="post.problemHtml | highlightCode"></div>
```

This works OK for code inside simple `<pre><code>` blocks. You might also have blocks of code that indicate a specific language to be used when highlighting, for example `<pre><code class="language-javascript">`, pretty usual in HTML rendered from Markdown. So in case the language has been specified like that, I extended the code the following way:

```typescript
@Pipe({
  name: 'highlightCode'
})
export class HighlightCodePipe implements PipeTransform {

  transform(htmlMarkup: string): string {
    const preCodeRegex = /<pre><code(?: class="language-(.*)")?>([\s\S]*?)<\/code><\/pre>/g;

    return htmlMarkup.replace(preCodeRegex, function (_match, languageName, codeBlock) {
      let codeBlockHighlighted: string;

      if (!languageName) {
        codeBlockHighlighted = hljs.highlightAuto(codeBlock).value;
      } else {
        codeBlockHighlighted = hljs.highlight(languageName, codeBlock, true).value;
      }

      return '<pre><code class="hljs">' + codeBlockHighlighted + '</pre></code>';
    });
  }
}
```
