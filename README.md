# TestAmazonQ - JWT Web API

A .NET 7 Web API project with JWT authentication, built using clean architecture principles.

## Features

- ✅ JWT Authentication (Login/Logout)
- ✅ User Management CRUD APIs
- ✅ Repository Pattern with Base Repository
- ✅ Clean Architecture (Controllers → Services → Repositories)
- ✅ Entity Framework Core with SQLite
- ✅ Swagger UI with JWT Authorization
- ✅ ApiResponse wrapper for consistent responses
- ✅ Unit Tests with Moq
- ✅ Error Handling and Constants

## Tech Stack

- **.NET 7** - Framework
- **Entity Framework Core** - ORM
- **SQLite** - Database
- **JWT Bearer** - Authentication
- **BCrypt** - Password hashing
- **Swagger/OpenAPI** - API documentation
- **xUnit + Moq** - Unit testing

## Project Structure

```
TestAmazonQ/
├── Controllers/           # API Controllers
├── Services/             # Business Logic
├── Repositories/         # Data Access Layer
│   ├── Interfaces/       # Repository Interfaces
│   └── Implementations/  # Repository Implementations
├── Models/               # Data Models
│   ├── Requests/         # Request DTOs
│   └── Responses/        # Response DTOs
├── Data/                 # DbContext
├── Constants/            # Application Constants
└── Migrations/           # EF Migrations

TestAmazonQ.Tests/
└── Controllers/          # Unit Tests
```

## APIs

### Authentication
- `POST /api/auth/login` - User login
- `POST /api/auth/logout` - User logout
- `POST /api/auth/register` - User registration

### User Management (Requires JWT)
- `GET /api/users?pageNumber=1&pageSize=10` - Get users with pagination
- `GET /api/users/{id}` - Get user by ID
- `POST /api/users` - Create new user
- `PUT /api/users/{id}` - Update user
- `DELETE /api/users/{id}` - Delete user

### Test Endpoints
- `GET /api/test/public` - Public endpoint
- `GET /api/test/protected` - Protected endpoint (requires JWT)

## Getting Started

### Prerequisites
- .NET 7 SDK
- Git

### Installation

1. Clone the repository
```bash
git clone https://github.com/xuanta1/TestAmazonQ.git
cd TestAmazonQ
```

2. Restore packages
```bash
dotnet restore
```

3. Run database migrations
```bash
cd TestAmazonQ
dotnet ef database update
```

4. Run the application
```bash
dotnet run
```

5. Open Swagger UI
```
https://localhost:7xxx/swagger
```

### Testing

Run unit tests:
```bash
cd TestAmazonQ.Tests
dotnet test
```

## Usage

### 1. Register/Login
```json
POST /api/auth/register
{
  "username": "admin",
  "password": "password123"
}
```

### 2. Get JWT Token
```json
POST /api/auth/login
{
  "username": "admin", 
  "password": "password123"
}
```

### 3. Use Token in Swagger
1. Click "Authorize" button
2. Enter: `Bearer <your-jwt-token>`
3. Test protected endpoints

## Configuration

Update `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=app.db"
  },
  "Jwt": {
    "Key": "YourSecretKeyHere",
    "Issuer": "TestAmazonQ",
    "Audience": "TestAmazonQUsers"
  }
}
```

## Architecture Patterns

- **Repository Pattern** - Data access abstraction
- **Service Layer** - Business logic separation
- **Dependency Injection** - Loose coupling
- **ApiResponse Wrapper** - Consistent API responses
- **Constants** - Centralized message management

## Testing

- **14 Unit Tests** covering all CRUD operations
- **Happy Path & Error Cases** testing
- **Mocking** with Moq for isolated testing
- **AAA Pattern** (Arrange-Act-Assert)

## License

This project is for educational purposes.