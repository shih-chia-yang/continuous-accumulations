# require_https

- Enabling HTTPS in ASP.NET Core
- Secure Socket Layer (SSL)

Hyper Text Transfer Protocol Secure(HTTPS) is the secure version of HTTP, the protocol over which data is sent between your browser and the website that you are connected to

HTTPS is often used to protect highly confidential online transactions 

## Enable HTTPS in ASP.NET Core

- Create an API project Configure for HTTPS selected
- COnfigure HTTPS in Startup if nor configured
- To test with IIS Express. enable SSL from project Properties

## UseHttpsRedirection

add `app.UseHttpsRedirection();` middleware to the Startup's in `Configure` method

## modify iis setting

**/Properties/LaunchSettings**