Welcome to Mjm AuthServer

How to customize account pages:
You must have aspnetcore generator installed:
dotnet tool install -g dotnet-aspnet-codegenerator

List overridable pages using:
dotnet aspnet-codegenerator identity -lf

Choose the page you want to cstomize and run:
dotnet aspnet-codegenerator identity  --files "Account.Register"
this will create necessary pages and imports.
Reset program.cs if it is modified by the codegenerator. 