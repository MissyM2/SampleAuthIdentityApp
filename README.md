# SampleAuthApp
.NET Core, .NET MAUI, API, Front End, Mobile, Community Toolkit

### Overview

This app is an .net Web API, version 7.0, with a webapp UI for the purposes of demonstrating a manual implementation of authentication, authorization, and protection of an api endpoint.

### Structure and Libraries
Solution Name: SampleAuthApp

Projects:
- SampleAuthWebApp
  - Microsoft.AspNetCore.Http.Extensions
  - Newtonsoft.Json
    
- SampleAuthWebAPI
   - Microsoft.AspNetCore.Authentication.JwtBearer
   - Microsoft.AspNetCore.OpenApi
   - Swashbuckle.AspNetCore
   - System.IdentityModel.Tokens.Jwt