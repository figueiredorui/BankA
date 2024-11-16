# BankA
Web App to import, categorize and analyse bank transactions

## Description
BankA is the initial version of a web application that allow to see multiple bank accounts in one place by importing statement files. Transacations are automatically categorised by Categories and Merchants providing an overview of your income and expenses.

### Demo
[BankA](https://banka.azurewebsites.net)

Technologies & Libraries:
1. [ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/release-notes/aspnetcore-8.0?view=aspnetcore-8.0)
2. [Entity Framework Core](https://learn.microsoft.com/en-us/ef/core/what-is-new/ef-core-8.0/whatsnew)
3. [MediatR](https://github.com/jbogard/MediatR)
4. [Serilog](https://serilog.net/)
5. [CsvHelper](https://serilog.net/)

6. [Angular](https://angular.io/)
7. [Material Angular](https://material.angular.io/)

Databases:
1. [Entity Framework Core In-Memory Database](https://learn.microsoft.com/en-us/ef/core/providers/in-memory/?tabs=dotnet-core-cli)
2. [Sql Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (coming soon)

The project is configured to use the Entity Framework In Memory database if no connection string is defined on the appsettings.json file and ConnectionStrings:DefaultConnection section.

To use SQL Server database:
1. Create an empty database
2. Update the connection string on the appsettings.json file and ConnectionStrings:DefaultConnection section


## How to run the application?
 1. Check out source code from this repository.
 2. Check out source code from [DotNetLib9](https://github.com/figueiredorui/DotNetLib9) repository.
 3. Using Visual Studio run **BankA.WebApp** project.
 4. Open **BankA.Webpp/ClientApp** and run 'npm install' 'npm run start'
 
## Project Structure

The project implements a clean architecture. Clean architecture is a set of design principles that divides software components/modules into onion ring layers. The main idea is that code dependencies are supposed to only go from the outer layers to the inner ones. [Clean architecture](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html)

**Domain**

The domain layer contains domain entities and business rules.

**Application (Use cases)**

The application layer implements the use cases / features of the application.

**Infrastructure**

The infrastructure layer implements data access, authentication and interfaces with external 3rd party libraries and APIS

**WebApp**

This is the entry point to the application and implements the REST APIs and the UI using Angular.

