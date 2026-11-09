# RaceDay API – Part 2

## 1. Project Overview

RaceDay is a web-based event management application designed to make organising, managing and participating in sporting events easier.

The RaceDay API forms part of the backend of the application. It handles communication between the database and the frontend application by processing requests, managing information and providing data through API endpoints.

The API is developed using ASP.NET Core Web API and Entity Framework Core, with Microsoft SQL Server used for database storage.

This project forms part of the RaceDay application development and focuses on creating a structured, maintainable and functional backend.

## 2. Project Objectives

The main objectives of the RaceDay API are to:

- Provide a connection between the application and the database.
- Allow users to register and log in.
- Manage participant and organiser information.
- Manage sporting events and event categories.
- Support event enrolments.
- Store and retrieve event results.
- Provide structured API endpoints for communication with the MVC application.
- Maintain data accuracy and consistency.

## 3. Technologies Used

The project uses the following technologies:

| Technology | Purpose |
|---|---|
| C# | Main programming language |
| ASP.NET Core Web API | Backend application development |
| .NET 8 | Application framework |
| Entity Framework Core | Database operations and object-relational mapping |
| Microsoft SQL Server | Database management |
| SQL Server Management Studio (SSMS) | Database creation and management |
| Visual Studio 2022 | Development environment |
| Swagger / OpenAPI | API documentation and testing |
| HTML, CSS and Bootstrap | Frontend presentation in the MVC application |

## 4. Project Structure

The project follows a structured folder organisation to make the code easier to maintain and understand.

```text
RaceDayApi/
│
├── Controllers/
│   ├── AuthController.cs
│   ├── ParticipantController.cs
│   ├── OrganiserController.cs
│   ├── EventController.cs
│   ├── CategoryController.cs
│   ├── EnrolmentController.cs
│   └── ResultController.cs
│
├── Data/
│   └── RaceDayContext.cs
│
├── DTOs/
│   ├── LoginRequest.cs
│   ├── RegisterRequest.cs
│   └── Other request and response DTOs
│
├── Models/
│   ├── Participant.cs
│   ├── Organiser.cs
│   ├── Event.cs
│   ├── Category.cs
│   ├── Enrolment.cs
│   └── Result.cs
│
├── Services/
│   └── Application services
│
├── Properties/
│
├── appsettings.json
├── Program.cs
└── RaceDayApi.csproj
```

*Note: The structure above represents the intended organisation. Only include files and folders that actually exist in your solution.*

### Folder descriptions

- **Controllers:** Handles incoming HTTP requests and returns responses.
- **Data:** Contains the database context used by Entity Framework Core.
- **DTOs:** Contains Data Transfer Objects used to receive and return information.
- **Models:** Represents the database entities.
- **Services:** Contains reusable application logic.
- **Properties:** Contains application launch settings.

## 5. Database Design

RaceDay uses Microsoft SQL Server to store application information.

The database is called `RaceDayDB`.

The database includes the following main entities:

| Entity | Description |
|---|---|
| Participant | Stores participant account information. |
| Organiser | Stores event organiser account information. |
| Events | Stores sporting event information. |
| Category | Stores event categories and entry fees. |
| Enrolment | Stores participant event enrolments. |
| Result | Stores event results and participant performance information. |

### Database connection

The API connects to SQL Server through the connection string configured in `appsettings.json`.

Example configuration:

```json
{
  "ConnectionStrings": {
    "RaceDayConnection": "Server=YOUR_SERVER_NAME;Database=RaceDayDB;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

Replace `YOUR_SERVER_NAME` with the name of your own SQL Server instance.

**Important:** Do not publish real database passwords or sensitive connection details in a public repository.

## 6. Getting Started

Follow these steps to open and run the project.

### Requirements

Before running the application, ensure that the following software is installed:

1. Visual Studio 2022.
2. .NET 8 SDK.
3. Microsoft SQL Server.
4. SQL Server Management Studio (SSMS).
5. Entity Framework Core packages compatible with the project's .NET version.

### Installation

1. Open Visual Studio 2022.
2. Open the existing RaceDay solution.
3. Locate the `RaceDayApi` project.
4. Restore the NuGet packages.
5. Check the database connection string in `appsettings.json`.
6. Confirm that the SQL Server database exists.
7. Build the project to check for compilation errors.
8. Set the API project as the startup project.
9. Run the application.

### Running the API

Once the project starts successfully, Swagger should open in the browser.

Swagger allows developers to view available API endpoints, submit requests and inspect the responses returned by the application.

If Swagger does not open automatically, check the API project's launch settings and open the configured application URL followed by `/swagger`.

## 7. API Functionality

The API is designed to support the following functionality:

### User Authentication

- Register a new participant.
- Register a new organiser.
- Log in using an email address and password.
- Validate user credentials.
- Prevent duplicate email registrations.

### Participant Management

- Create participant records.
- Retrieve participant information.
- Update participant details.
- Delete participant records where permitted.

### Organiser Management

- Create organiser records.
- Retrieve organiser information.
- Update organiser details.
- Manage events associated with organisers.

### Event Management

- Create sporting events.
- Retrieve available events.
- Update event information.
- Delete events where permitted.

### Category Management

- Create event categories.
- View available categories.
- Update category information.
- Manage entry fees and event distances.

### Enrolment Management

- Register participants for events.
- Retrieve enrolment information.
- Update enrolment details.
- Cancel enrolments where permitted.

### Results Management

- Record participant results.
- Retrieve event results.
- Update result information.
- Display participant performance.

*Note: This section describes the intended functionality. Confirm that each operation has been implemented before describing it as completed.*

## 8. Testing the API

Swagger is used to test API functionality during development.

The following testing activities should be performed:

- Test successful and unsuccessful login attempts.
- Test participant registration.
- Test organiser registration.
- Check that duplicate email addresses are rejected.
- Test retrieving existing records.
- Test creating new records.
- Test updating records.
- Test deleting records.
- Confirm that invalid input returns appropriate error responses.
- Confirm that database changes are saved correctly.

### Testing process

1. Run the API project.
2. Open Swagger.
3. Select the endpoint to test.
4. Click **Try it out**.
5. Enter the required information.
6. Click **Execute**.
7. Review the response code and response body.
8. Check the database in SSMS where necessary.

## 9. Security Considerations

Security is important because RaceDay stores user and participant information.

The following practices should be followed:

- Passwords should be stored as secure password hashes, never as plain text.
- User input should be validated.
- Duplicate account registrations should be prevented.
- Database credentials should not be exposed publicly.
- Sensitive information should not be returned unnecessarily through API responses.
- Authentication and authorisation should be applied to protected endpoints.
- HTTPS should be used when communicating with the application.

## 10. Troubleshooting

The following are common issues that may occur while developing or running the API.

| Problem | Possible solution |
|---|---|
| API does not start | Check the build errors and startup configuration. |
| Swagger does not open | Confirm that Swagger services and middleware are configured. |
| Database connection fails | Check the SQL Server instance and connection string. |
| Invalid object name | Confirm that the database and table names match the application models. |
| Missing NuGet packages | Restore packages and check package versions. |
| HTTP 400 response | Check the request body and validation errors. |
| HTTP 401 response | Check the login details and authentication configuration. |
| HTTP 500 response | Review the application output and exception details. |

## 11. Future Improvements

Possible improvements to the RaceDay application include:

- Implementing JWT-based authentication where required.
- Adding stronger input validation.
- Improving error handling and logging.
- Adding automated unit and integration tests.
- Improving the user interface and user experience.
- Adding event search and filtering.
- Adding participant notifications.
- Improving reporting and event statistics.

## 12. Important Development Notes

Before applying changes to the existing RaceDay solution:

1. Back up the project or commit the current version to source control.
2. Review existing files before replacing them.
3. Check that namespaces match the project.
4. Confirm that NuGet package versions are compatible.
5. Compare database models with the actual SQL Server tables.
6. Preserve existing `Program.cs` configuration and merge only the required changes.
7. Avoid dropping or recreating database tables without confirming the impact.
8. Build and test the solution after making changes.

## 13. Conclusion

The RaceDay API provides the backend foundation for the RaceDay event management application. It is intended to support user account management, sporting event administration, participant enrolments and event results.

By using ASP.NET Core Web API, Entity Framework Core and Microsoft SQL Server, the project provides a structured approach to managing application data and supporting communication between the backend and frontend components.

The application can be extended in future development stages with additional functionality, improved security and automated testing.
