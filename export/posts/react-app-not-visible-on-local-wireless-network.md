# React app not visible on local wireless network

Author: Dan Dumitru; Created: November 7, 2022; Last Edit: November 7, 2022  
Tags: React; Views: 340

## Problem

I created a React app and I want to see it on my mobile phone, using the local network that has a wireless router.

When I `npm start` the app, it says:

```
You can now view the-most-awesome-app in the browser.       

  Local:            http://localhost:3000        
  On Your Network:  http://172.**.***.1:3000    
```
*(I masked the IP here with stars out of probably unnecessary precaution)*

But when I try to use `http://172.**.***.1:3000` on my phone it doesn't work.

This is the IP address shown in `ipconfig` too.

I tried creating an Inbound rule in the Firewall, even disabling the Firewall, nothing worked.

## Solution

Turns out I was using the wrong IP, and `npm start` was also showing me the wrong one.

Doing `ipconfig` in a command prompt showed this, among other entries:

```txt
C:\Users\Dan>ipconfig

Ethernet adapter vEthernet (Default Switch):

   Connection-specific DNS Suffix  . :
   Link-local IPv6 Address . . . . . : fe80::****:****:c027:522e%47
   IPv4 Address. . . . . . . . . . . : 172.**.***.1
   Subnet Mask . . . . . . . . . . . : 255.255.240.0
   Default Gateway . . . . . . . . . :

[...]

Wireless LAN adapter WiFi:

   Connection-specific DNS Suffix  . :
   Link-local IPv6 Address . . . . . : fe80::****:****:d468:a03e%16
   IPv4 Address. . . . . . . . . . . : 192.168.0.194
   Subnet Mask . . . . . . . . . . . : 255.255.255.0
   Default Gateway . . . . . . . . . : 192.168.0.1
```
It turns out that the correct IP to use is NOT the one under the section **Ethernet adapter vEthernet (Default Switch)**, but the one under **Wireless LAN adapter WiFi**.

So, in my case, `http://192.168.0.194:3000` worked on my mobile phone.

![React local IP](https://i.imgur.com/UrbE75b.png)
