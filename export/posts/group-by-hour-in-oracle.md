# Group by hour in Oracle

Author: Dumitru Maros; Created: July 2, 2024; Last Edit: July 3, 2024  
Tags: Oracle,SQL; Views: 72

## Problem

When put in a chart don't show all hours from day. I made a table from 1 to 24

## Solution

I found a simple method like this
```sql
  SELECT LEVEL - 1 AS time_hour
  FROM dual
  CONNECT BY LEVEL <= 24
```
