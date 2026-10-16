# RaceDay API — Part 2

A sample ASP.NET Core API project for the RaceDay application. The code is organised into focused folders to make the application easier to explore and extend.

> **Important:** This project is a learning resource, not a drop-in replacement for your existing solution. Review the guidance below before copying or applying changes.

## Getting Started

1. Open your existing RaceDay solution in Visual Studio.
2. Back up your work or commit your current changes to Git.
3. Review the sample files and compare them with your project before making changes.
4. Confirm that namespaces, package versions, and database settings match your environment.
5. Build the solution and resolve any errors before running the API or tests.

## Project Structure

| Folder | Purpose |
|---|---|
| `Models` | Data models for organisers, participants, events, categories, enrolments, and results. |
| `Data` | Entity Framework Core database context and related configuration. |
| `DTOs` | Data transfer objects used to define API request and response data. |
| `Controllers` | API endpoints for authentication, events, profiles, enrolments, and results. |
| `Services` | Helper services used by the application. |
| `Tests` | Automated tests for application behaviour. |
| `.github/workflows` | GitHub Actions workflows for building and testing the project. |

## Configuration and Safety

- **Do not replace your existing `Program.cs` with the sample file.** Preserve your current configuration and merge only the registrations or middleware you need.
- Check that namespaces and configuration keys match your project. The sample refers to `RaceDayApi` and `RaceDayConnection`; your project may use different names.
- Use your own database connection string. Never commit passwords, API keys, or other secrets to GitHub.
- Review your database and migrations before applying changes. Do not run commands or scripts that drop tables if you need to preserve existing data.
- Confirm that Entity Framework Core packages use compatible versions across the API and test projects.

## Recommended Workflow

1. Review the model classes and update them to fit your application.
2. Review `RaceDayContext` and confirm that it uses the correct namespace and database configuration.
3. Check `appsettings.json` and configure your local database connection securely.
4. Compare the sample `Program.cs` with your existing file, then add only the required configuration.
5. Build the solution.
6. Run the API and verify its endpoints in Swagger.
7. Run the automated tests and confirm they pass.

## Testing

The test project must reference the API project and include the required test framework packages. If tests use an in-memory database, install `Microsoft.EntityFrameworkCore.InMemory` in the **test project**. Its major version should match the Entity Framework Core version used by the API project.

Run the tests from the solution directory:

```bash
dotnet test
