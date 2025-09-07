# JavaScript number to short format like YouTube view counts, with magnitude suffix

Author: Dan Dumitru; Created: April 20, 2020; Last Edit: May 14, 2020  
Tags: JavaScript,Angular; Views: 117

## Problem

I needed to format numbers to a short format, like '12K',  '7.2M', the same way YouTube view counts are formatted. So a magnitude suffix should be added: K, M, or B, for thousands, millions, and billions, respectively.

For numbers that will get to a single digit, one digit after the decimal point should be specified, if applicable.

This is the desired end result:
- 12 -> 12
- 123 -> 123
- 1,234 -> 1.2K
- 12,345 -> 12K
- 123,456 -> 123K
- 1,234,567 -> 1,2M
- 12,345,678 -> 12M
- 123,456,789 -> 123M
- 1,234,567,890 -> 1,2B
- 1,023,456,789 -> 1B

## Solution

I used a `ranges` array for the magnitude levels, and this is how the code turned out to be:

```javascript
const ranges = [{
    divider: 1E3,
    suffix: 'K'
}, {
    divider: 1E6,
    suffix: 'M'
}, {
    divider: 1E9,
    suffix: 'B'
}];

function formatNumber(input) {
    for (let index = ranges.length - 1; index >= 0; index--) {
        if (input > ranges[index].divider) {
            let quotient = input / ranges[index].divider;

            if (quotient < 10) {
                quotient = Math.floor(quotient * 10) / 10;
            } else {
                quotient = Math.floor(quotient);
            }

            return quotient.toString() + ranges[index].suffix;
        }
    }

    return input.toString();
}
```

And this is how it looks like as an Angular pipe:

```typescript
import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
    name: 'formatNumberShortSuffix'
})
export class FormatNumberShortSuffixPipe implements PipeTransform {
    readonly ranges = [
        { divider: 1E3, suffix: 'K' },
        { divider: 1E6, suffix: 'M' },
        { divider: 1E9, suffix: 'B' }
    ];

    transform(input: number): string {
        for (let index = this.ranges.length - 1; index >= 0; index--) {
            if (input > this.ranges[index].divider) {
                let quotient = input / this.ranges[index].divider;

                if (quotient < 10) {
                    quotient = Math.floor(quotient * 10) / 10;
                } else {
                    quotient = Math.floor(quotient);
                }

                return quotient.toString() + this.ranges[index].suffix;
            }
        }

        return input.toString();
    }
}
```
