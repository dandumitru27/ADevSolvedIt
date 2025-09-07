# C# Console format number as you type

Author: Dan Dumitru; Created: February 12, 2022; Last Edit: March 13, 2022  
Tags: C#; Views: 46

## Problem

In a Console Application, the user is asked to enter a potentially long number. To make things easy for him, I'd like to format the number as he types it, to transform for example `1563124500` into `1,563,124,500`.

I'd like to do it as he types the number, overwriting his unformatted input. So not displaying the formatted number after his input, or on the next line.

And I'd also want the Backspace button to function as expected.

## Solution

It turns out that writing `\r` to the console will move the cursor to the beginning of the line the user is typing into. So as he types I can store the entered number so far, and overwrite what he's typing with the formatted number.

This could cause problems when he presses Backspace, so in that situation I just print many spaces to erase what was previously entered, and display the new number again.

The method returns when the user presses Enter, and only if he had entered at least one digit.

```cs
public static long ReadNumberAndFormat()
{
    var numberString = "";
    long number = 0;

    while (true)
    {
        var keyInfo = Console.ReadKey();
            
        if (keyInfo.Key == ConsoleKey.Enter)
        {
            if (numberString.Length > 0)
            {
                break;
            }
            else
            {
                continue;
            }
        }

        if (keyInfo.Key == ConsoleKey.Backspace && numberString.Length > 0)
        {
            numberString = numberString.Remove(numberString.Length - 1);
            Console.Write("\r               ");
        }
        else
        {
            numberString += keyInfo.KeyChar;
        }

        if (long.TryParse(numberString, out number))
        {
            Console.Write('\r' + number.ToString("n0"));
        }
    }

    return number;
}
```
