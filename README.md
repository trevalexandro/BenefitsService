# BenefitsService

**BenefitsService** is a REST API built with **.NET 9** that allows clients to:

- View a list of employees
- View an employee’s dependents
- View an employee’s salary (gross & net)
- View an employee’s benefit deductions
- Add new dependents to an employee’s record

The service is built using **Domain-Driven Design (DDD)** principles and uses **Entity Framework Core (EF Core)** with a **SQLite** database. Swagger UI is included for easy API testing and exploration.

---

## 🧱 Project Structure

The solution is organized into the following projects:

- **BenefitsService.API** – Entry point for the web API. Hosts all REST endpoints and handles HTTP concerns.
- **BenefitsService.Application** – Application layer responsible for coordinating domain operations and invoking services/repositories.
- **BenefitsService.Domain** – Core domain logic and business rules (e.g., employee entities, benefit deduction rules, salary calculations).
- **BenefitsService.Infrastructure** – Infrastructure-specific code such as EF Core DbContext, repository implementations, and data access.

---

## 🚀 Getting Started

### Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/)
- Git

> Note: No database setup or migrations are necessary. The SQLite database files are included in the repository.

### Running the API

1. Clone the repository:
```
git clone https://github.com/your-org/BenefitsService.git
cd BenefitsService/src/BenefitsService.API
```

2. Run the API:

`dotnet run`

3. Open your browser and navigate to:

https://localhost:7204/swagger to explore and test the API using Swagger UI.


🧪 Running Tests
Unit tests are located in the BenefitsService.Tests project.

The tests are written using xUnit and Moq.

To run tests:
```
cd BenefitsService.Tests
dotnet test
```


🔍 Technologies Used

.NET 9

Entity Framework Core (SQLite provider)

Swagger/Swashbuckle

xUnit + Moq (for testing)

Domain-Driven Design (DDD)


📂 Example Endpoints

Endpoint	Method	Description

/api/v1/employees	GET	Get list of employees

/api/v1/employees/{id}	GET	Get dependents, deductions, and net pay for a specific employee

/api/v1/employees/{id}/dependents	POST	Add a new dependent
