# SampleAuthIdentityWebApp
.NET Core, WebAPI, frontend

### Overview

This app is an .net Web API, version 7.0, with a webapp UI for the purposes of demonstrating a manual implementation of authentication, authorization, and protection of an api endpoint.

### Structure and Libraries
Solution Name: SampleAuthIdentityWebApp

Projects/Libraries:
- SampleAuthIdentityWebApp
  - UI functions: Identity.UI : covers the UI elements of Identity; utilizes all scaffolded interfaces:  NOT USED HERE because this is a demonstration purpose.  In most circumstances, you will use this library
  - Functionalities: Identity : covers the functionalities of the auth/auth process
  - DataStore functions: Identity.EntityFrameworkCore : covers the data store, which is the in-memory representation of the database; stores all the db schema objects and c# classes
  - SqlServer: EntityFrameworkCore.SqlServer
  - Migrations: In order to take in-memory representation and create corresponding objects in the db
       - EntityFrameworkCore.Design
       - Migrations: EntityFrameworkCore.Tools
    
- SampleAuthIdentityWebAPI
   - Microsoft.AspNetCore.Authentication.JwtBearer
   - Microsoft.AspNetCore.OpenApi
   - Swashbuckle.AspNetCore
   - System.IdentityModel.Tokens.Jwt
# SampleAuthIdentityApp
