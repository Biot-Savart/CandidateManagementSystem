# CandidateManagementSystem

# Candidate Management System Server

The server side of the Candidate Management System is built using ASP.NET Core, providing a robust and scalable backend for managing candidates, their skills, positions, and related operations within the system.

## Technology Stack

- **Framework**: ASP.NET Core
- **Database**: EF Core for ORM, with migrations for easy database schema updates
- **Authentication**: Basic Authentication middleware for securing endpoints
- **Logging and Configuration**: Built-in ASP.NET Core Logging, with appsettings.json for configuration management

## Getting Started

### Prerequisites

- .NET 5.0 SDK or later
- Visual Studio 2019 or later (or VS Code with C# extension)
- SQL Server (for development, SQL Server Express is sufficient)

### Setup Instructions

1. Clone the repository to your local machine.
2. Navigate to the `CandidateManagementSystemV2.Server` directory.
3. Update the `appsettings.json` file with your database connection string.
4. Run the following commands in the terminal to apply the database migrations:
   ```
   dotnet ef database update
   ```
5. Start the server by running:
   ```
   dotnet run
   ```
   Alternatively, you can use Visual Studio to build and run the server.

## Features

- **Candidate Management**: Create, read, update, and delete operations for candidates.
- **Position Management**: Manage positions that candidates can apply for.
- **Skill Management**: Handle skills that can be assigned to candidates.
- **Authentication**: Basic authentication to secure the API endpoints.

## API Documentation

For detailed API documentation, please refer to the Swagger UI once the server is running. Navigate to `/swagger` to access the API documentation.

## Contribution

We welcome contributions! Please feel free to fork the repository and submit pull requests.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
