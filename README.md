# Candidate Management System

The Candidate Management System is a full-stack web application designed to streamline the process of managing candidate information for recruitment purposes. This system allows for the creation, storage, and management of candidate profiles, including their skills and positions applied for. Built with Angular for the frontend and ASP.NET Core for the backend, it leverages modern web development practices to offer a responsive and intuitive user interface.

## Features

- **Candidate Management**: Create, read, update, and delete (CRUD) operations for candidate profiles.
- **Skill Tracking**: Associate specific skills with candidates and manage these associations.
- **Position Application**: Track positions candidates have applied for, allowing for easy management of candidate-position relationships.
- **Data Reporting**: Generate reports on aggregated candidate data, such as the number of candidates per skill.

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes.

### Prerequisites

Before you begin, ensure you have the following tools installed:

- [.NET 6 SDK](https://dotnet.microsoft.com/download)
- [Node.js and npm](https://nodejs.org/en/download/)
- [Angular CLI](https://angular.io/cli)
- [Docker](https://docs.docker.com/get-docker/) (optional for containerization)

### Login Credentials
   To access the application, you can use the following default credentials. These credentials are intended for initial testing and development purposes only. Please ensure to change the default passwords and enforce stronger security measures in production environments.

- For Development and Testing
  - Username: admin
  - Password: password

Important Security Notice
   The provided default credentials are for demonstration and development purposes only. Do not use these credentials in production environments.
   Ensure that all default passwords are changed before deploying the application.
   Consider implementing additional authentication measures, such as two-factor authentication (2FA), especially for administrative accounts.

### Installation

1. **Clone the repository**

   ```bash
   git clone https://github.com/Biot-Savart/CandidateManagementSystem.git
   ```

2. **Set up the backend**

   - Navigate to the server project directory and restore dependencies:

   ```bash
   cd CandidateManagementSystem/CandidateManagementSystemV2.Server
   dotnet restore
   ```

   - Ensure you have the EF Core CLI installed globally. If you don't have it installed, you can install it using the following command:

      ```bash
      dotnet tool install --global dotnet-ef
      ```
      Make sure you're in the project directory where your .csproj file is located before running the commands below.

   - Updating the Database
      To apply the pending migrations to your database, use:
      
      ```bash
      dotnet ef database update
      ```
   
   Run the application:

   ```bash
   dotnet run
   ```

4. **Set up the front end**

   Navigate to the client project directory and install npm packages:

   ```bash
   cd CandidateManagementSystem/candidatemanagementsystemv2.client
   npm install
   ```

   Serve the Angular application:

   ```bash
   ng serve
   ```

   Visit `http://localhost:4200` in your browser.

### Docker (Optional)

If you prefer to run the application using Docker, follow these steps:

```bash
docker-compose up --build
```

This command builds and runs the Docker containers specified in the `docker-compose.yml` file.

## Contributing

We welcome contributions! Please read our [CONTRIBUTING.md](CONTRIBUTING.md) for details on our code of conduct and the process for submitting pull requests.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## API Documentation

For detailed API documentation, please refer to the Swagger UI once the server is running. Navigate to `/swagger` to access the API documentation.


### Remember to replace placeholders (like URLs and feature descriptions) with actual data from your project. Tailoring the README to reflect your project accurately will help new users and contributors understand how to get involved.



