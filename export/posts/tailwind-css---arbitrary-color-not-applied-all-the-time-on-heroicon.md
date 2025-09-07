# Tailwind CSS - Arbitrary color not applied all the time on Heroicon

Author: Dan Dumitru; Created: January 29, 2023; Last Edit: January 29, 2023  
Tags: React,Tailwind CSS; Views: 75

## Problem

I'm using Tailwind CSS in my React app.

Tailwind has classes like `"text-blue-600"` to set the text color, and you can also use colors that are not predefined using something like `"text-[#50d71e]"`.

For icons I use the Heroicons library.

At one point I have some logic to change the color of one of the icons. Something like:

```
let colorClass = 'text-black'

if (isCorrect) {
  colorClass = 'text-[#F8D000]';
}

const icon = <TrophyIcon className={colorClass} />;
```

This worked on my first tests, but after playing with the app for some time I noticed the custom color is not applied all the time, sometimes it behaves like it doesn't find it.

## Solution

I didn't find a proper solution for this, what follows is just an awkward work-around.

I noticed that after the arbitrary color was used once on the page, then it would consistently be applied where needed.

So I created an invisible div on the page with my arbitrary color, and this fixed the issue.

```
{/* The div below is hidden, it's just to overcome a bug 
    where the custom color is not displayed properly */}
<div className='text-[#F8D000]'></div>
```
