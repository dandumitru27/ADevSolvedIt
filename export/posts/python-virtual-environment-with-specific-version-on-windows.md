# Python virtual environment with specific version on Windows

Author: Dan Dumitru; Created: December 10, 2024; Last Edit: December 10, 2024  
Tags: Python; Views: 95

## Problem

I've set up a few Python projects recently and I needed to create each time a virtual environment with a specific Python version on Windows.

## Solution

Each time I ended up googling and getting to this Stack Overflow question: [https://stackoverflow.com/questions/1534210/use-different-python-version-with-virtualenv](https://stackoverflow.com/questions/1534210/use-different-python-version-with-virtualenv)

The answer for Windows is there somewhere in comments, but it's hard to find, so here it is in plain sight:

```
py -3.12 -m venv my_env_name
```
