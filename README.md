# RaceDay Part 2 - Student Guide

This folder contains example code for building the RaceDay API in ASP.NET Core.
The code is organised into small folders so it is easier to find each part.

## Important before you start

- Make a copy of your own project before changing anything.
- **Do not replace your existing Program.cs with the Program.cs in this folder.** Keep the code your lecturer or you have already written. Compare the files and add only the missing registrations/middleware in the correct places.
- This is a starting point, not proof that the project builds on your computer. Build and test it in Visual Studio and fix any errors based on your own project settings.
- Check your namespace and database connection string. This sample uses `RaceDayApi` and `RaceDayConnection`.
- Never share real passwords or database credentials in GitHub.

## What the folders are for

- `Models`: C# classes that represent Organiser, Participant, Event, Category, Enrolment and Result.
- `Data`: `RaceDayContext` connects the models to SQL Server using Entity Framework Core.
- `DTOs`: classes used to receive information sent to API endpoints.
- `Controllers`: the API routes, such as login, events, profile, enrolments and results.
- `Services`: small helper classes used by the controllers.
- `Tests`: an example automated test project.
- `.github/workflows`: a basic GitHub Actions workflow for building/testing.

## Suggested order

1. Open your existing `RaceDayApi` solution in Visual Studio.
2. Back up your solution or commit your current work to Git.
3. Check that the EF Core SQL Server, Design and Tools packages are installed.
4. Add or compare the model classes first.
5. Add `RaceDayContext` and check its namespace.
6. Compare `appsettings.json` with your own file and use your own SQL Server connection string.
7. Merge the required lines into your existing `Program.cs`; do not delete your existing setup.
8. Build the project. Fix errors before moving on.
9. Create/apply migrations only after checking the database connection and existing database. Do not run scripts that drop tables if you need to keep the data.
10. Run the API and test endpoints in Swagger. Then run the test project.

## Understanding the main files

- `Program.cs` starts the application and registers services like controllers, the database and sessions.
- `RaceDayContext.cs` tells Entity Framework which tables exist and how they relate.
- Model classes describe the data stored for each table.
- DTO classes describe the information an endpoint accepts from a request.
- Controller methods handle requests and return HTTP responses.


