# React Oryx build fail with TS2307: Cannot find module

Author: Dan Dumitru; Created: April 2, 2023; Last Edit: April 2, 2023  
Tags: React,Azure; Views: 164

## Problem

I was trying to deploy my React app in Azure as a Static Web App.

I have the code hosted in GitHub, so when I created the Static Web App, Azure offered to set up GitHub Actions for me and run the pipeline to deploy the code.

Unfortunately, although the code was building fine on my machine, when it was getting build in the GitHub Action it was using a build system named Oryx that found some issues that were not flagged during local build.

The last issue that it signaled was this one:

```
TS2307: Cannot find module '../models/Puzzle' or its corresponding type declarations.
  > 1 | import Puzzle from "../models/Puzzle";

...

---End of Oryx build logs---
Oryx has failed to build the solution.
```

, which was weird, as I checked locally and I had the `Puzzle.ts` file where it was importing it from.

I spent a couple of hours debugging this.

## Solution

It turned out that in Git (on GitHub) the file was tracked with lowercase letters, so it was `puzzle.ts` instead of `Puzzle.ts`, even though locally I had it capitalized. It seems Git didn't pick up the change when I modified the name of the file.

That was the reason this was failing:

```
import Puzzle from "../models/Puzzle";
```
, as the case mattered. I had in my `tsconfig.json` the default configuration stating: `"forceConsistentCasingInFileNames": true`

So the solution was changing the import to:

```
import Puzzle from "../models/puzzle";
```

Alternatively, I could have forced Git to take my capitalized filename, maybe deleting the file and re-adding it, but I didn't bother.
