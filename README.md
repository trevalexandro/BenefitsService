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

\---src
    +---BenefitsService.API # Entry point for the web API. Hosts all REST endpoints and handles HTTP concerns.
    |   +---Controllers # Controllers to handle routing of HTTP requests & returning responses.
    |   \---Properties # Configure launch settings for running application.
    +---BenefitsService.Application # Responsible for coordinating domain operations & invoking services/repositories.
    |   +---DTO # POCO objects for transferring data between the various projects.
    |   +---Extensions # Extension helper methods, source of mapping methods.
    |   +---Interfaces # Interfaces for application services.
    |   \---Services # Services that orchestrate request fulfillment.
    +---BenefitsService.Domain # Core domain logic/business rules (e.g., employee entities, benefit deduction rules, salary calculations).
    |   +---Aggregates # Key entry point for entity with business logic & dependent entities.
    |   +---Entities # Core domain entities that don't warrant an aggregate, also includes dependent entities.
    |   +---Enums # Enums for less dynamic data.
    |   +---Interfaces # Core domain interfaces that can be implemented by external consumers.
    \---BenefitsService.Infrastructure # Third party infrastructure such as ORM & repository implementations.
        +---Migrations # Code-first migrations for ORM implementation.
        \---Repositories # Data access implementations using domain entities.


---

## 🚀 Getting Started

### Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)
- Git

> Note: No database setup or migrations are necessary. The SQLite database files are included in the repository.
<br />

### Running the API

> 1. Clone the repository:
```
git clone https://github.com/your-org/BenefitsService.git
cd BenefitsService/src/BenefitsService.API
```

> 2. Run the API:

`dotnet run`

> 3. Open your browser and navigate to:

https://localhost:7204/swagger - to explore and test the API using Swagger UI.
<br />
<br />

🧪 Running Tests

Unit tests are located in the BenefitsService.Tests project.

The tests are written using xUnit and Moq.

To run tests:
```
cd BenefitsService.Tests
dotnet test
```
<br />

🔍 Technologies Used

- **.NET 9**

- **Entity Framework Core (SQLite provider)**

- **Swagger/Swashbuckle**

- **xUnit + Moq (for testing)**

- **Domain-Driven Design (DDD)**
<br />

📂 Example Endpoints

| Endpoint | Method | Descrption |
| --- | --- | --- |
| `/api/v1/employees` | GET | Get list of employees |
| `/api/v1/employees/{id}` | GET | Get dependents, deductions, and net pay for a specific employee |
| `/api/v1/employees/{id}` | PUT | Update basic employee information |
| `/api/v1/employees/{id}/dependents` | POST | Add a new dependent |
