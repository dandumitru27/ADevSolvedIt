# Visual Studio 2022 / 2019 Add Web Reference missing

Author: Dan Dumitru; Created: May 7, 2020; Last Edit: January 18, 2025  
Tags: Visual Studio; Views: 2,960

## Problem

For a .NET project, I want to add a Web Reference using a WSDL file, but there is no Add Web Reference when right clicking a project in Visual Studio 2022 / 2019. Where is it?

## Solution

It's different for modern .NET and .NET Framework projects.

For modern .NET (.NET Core / .NET 5 to .NET 9):

1. Right click the project in Solution Explorer, go on **Add** and then **Connected Service**
2. Click **Add a service reference**
3. Select **WCF Web Service**, click Next
4. Add your WSDL URL in the **URI** or click **Browse** to select the WSDL file

For .NET Framework (up to 4.8):

1. Right click the project in Solution Explorer, go on **Add** and then **Service Reference**
2. Click the **Advanced** button in the lower left corner
3. There you have it, again in the lower left corner, **Add Web Reference**
