# Visual Studio Test Explorer not running tests

Author: Dan Dumitru; Created: July 9, 2024; Last Edit: July 9, 2024  
Tags: Visual Studio,xUnit; Views: 1,338

## Problem

I have a .NET 6 project with xUnit unit tests opened in Visual Studio 2022.

I see the unit tests in the Test Explorer window and when I click to Run it builds the project but doesn't run any of them.

## Solution

There might be multiple causes for this, what worked for me was either:
- specifically clicking Build on the unit test project (building the whole solution wasn't helping), or
- running in a command prompt `dotnet test MyProject.Tests.Unit.csproj`, after navigating to the unit test project's folder, with my specific `csproj` name there.

After that I still had issues with new unit tests added, Test Explorer was either skipping them, or running old versions of them. Doing one of the two options above got the unit tests running correctly. Although it's still weird, Test Explorer should just work.
