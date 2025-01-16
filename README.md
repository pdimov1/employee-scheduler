## Employee Shifts Web Application

This is a web application designed to manage employee shifts, including creating, updating, deleting, and viewing shifts. It also supports CRUD operations for roles and employees.

## Prerequisites

Before running the solution, make sure you have the following installed:

- [.NET 8](https://dotnet.microsoft.com/download/dotnet)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (or use a local database)
- A code editor such as [Visual Studio](https://visualstudio.microsoft.com/) or [Visual Studio Code](https://code.visualstudio.com/)

## Clone the Repository

Start by cloning the repository to your local machine:

```bash
git clone https://github.com/pdimov1/employee-scheduler.git
```

## Setup the Database

Update the Connection String
Open the appsettings.json file and configure the connection string to point to your local or cloud database instance.

```
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=EmployeeShiftsDb;Trusted_Connection=True;MultipleActiveResultSets=true;"
}
```


## Apply Database Migrations

Open Package Manager Console, select EmployeeScheduler.Infrasturcture as default project and run the following command to apply migrations and create the database:

```
Add-Migration InitialCreate
Update-Database
```


## Run the Application

- Build and run EmployeeScheduler.Api project
- Get the API localhost URL and paste it in appsettings.json -> ApiEndpoint within EmployeeScheduler project
- Build and run EmployeeScheduler project
