# React i18next get language in utility function

Author: Dan Dumitru; Created: March 12, 2023; Last Edit: March 12, 2023  
Tags: React; Views: 178

## Problem

I'm using react-i18next in a React app.

As in the Quick Start guide, I can easily get the current language in a function component, with the `useTranslation` hook:

```
import { useTranslation } from 'react-i18next';

...

const { t, i18n } = useTranslation();
const language = i18n.language;
```

But how to get it in a utility function, where I can't use the hook?

## Solution

You can get it by importing `i18next`:

```
import i18next from 'i18next';

export function getMessage(): string {
  const language = i18next.language;
  ...
}
```
