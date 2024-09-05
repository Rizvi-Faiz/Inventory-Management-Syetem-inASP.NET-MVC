# Inventory Management System

This project is an **Inventory Management System** built using **ASP.NET MVC**. It allows users to manage product inventories by performing CRUD (Create, Read, Update, Delete) operations and supports other inventory-related features.

## Features

- **CRUD operations** for inventory management (Products, Categories, etc.)
- User-friendly interface using **Views** in ASP.NET MVC
- Migrations for database schema updates
- JSON-based configuration for development and production environments
- Basic web structure using the **wwwroot** folder for static assets

## Project Structure

- **Controllers**: Handles the requests and defines logic for different functionalities.
- **Models**: Contains the data structures and validation rules.
- **Views**: Provides the user interface for interacting with the system.
- **Migrations**: Handles database schema changes through Entity Framework migrations.
- **wwwroot**: Contains static files like CSS, JavaScript, and images.
- **appsettings.json**: Configuration settings for the application, including environment variables.
- **Program.cs**: The entry point for the application.

## Setup

### Prerequisites

- .NET 6 SDK or higher
- Visual Studio 2022 (or any IDE supporting .NET)
- SQL Server or any compatible database

### Installation

1. Clone this repository:
   ```bash
   git clone https://github.com/your-username/InventoryManagementSystem.git
   ```
2. Navigate to the project directory:
   ```bash
   cd InventoryManagementSystem
   ```
3. Install dependencies:
   ```bash
   dotnet restore
   ```
4. Update the `appsettings.json` file with your database connection string.

5. Apply migrations to the database:
   ```bash
   dotnet ef database update
   ```

6. Run the application:
   ```bash
   dotnet run
   ```

### Usage

Once the application is running, you can access it via the following URL:
```
http://localhost:5000
```

## Technologies Used

- **ASP.NET MVC** for web application structure
- **Entity Framework Core** for database access and migrations
- **SQL Server** as the primary database
- **Bootstrap** for front-end styling (or mention any other front-end framework used)

## Contributing

Contributions are welcome! Please follow the standard GitHub flow:

1. Fork the repository
2. Create a feature branch (`git checkout -b feature-name`)
3. Commit your changes (`git commit -m 'Add new feature'`)
4. Push to the branch (`git push origin feature-name`)
5. Open a Pull Request
