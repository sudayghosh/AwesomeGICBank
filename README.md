## The following architecture, patterns and packages are used -
- Domain Driven Architecture (DD) and Clean Architecture
- CQRS Pattern
- MediatR package
- AutoMapper for mapping
- Model First approach
- RestSharp package to call api
- Database SQL Server

## Instructions, how to run projects
- First make sure, you have installed SQL Server in your PC
- Update Connection String from appsettings.Development.json in AwesomeGIC.Bank.Web.Api project
- Now run database migration by update-database command from Package Manager console, Visual Studio
- Start (F5) web api project (AwesomeGIC.Bank.Web.Api) and
- Then run AwesomeGIC.Bank.UI project
