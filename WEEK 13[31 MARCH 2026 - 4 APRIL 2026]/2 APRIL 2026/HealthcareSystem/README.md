# Smart Healthcare Management System

A full-stack ASP.NET Core 8 application with MVC frontend, Web API backend, and SQL Server database.

---

## Tech Stack

| Layer | Technology |
|---|---|
| Frontend | ASP.NET Core 8 MVC · Bootstrap 5 · JS Client Validation |
| Backend | ASP.NET Core 8 Web API · RESTful · Swagger |
| Database | SQL Server · EF Core 8 · Migrations |
| Auth | JWT Bearer Tokens · Refresh Tokens · Role-based Auth |
| Architecture | Repository Pattern · Service Layer · AutoMapper · DTOs |
| Logging | Serilog (Console + Rolling File) |
| Caching | IMemoryCache (Doctors + Specializations) |
| Docs | Swagger / OpenAPI |

---

## Project Structure

```
HealthcareSystem/
├── HealthcareSystem.Models/          ← Shared class library
│   ├── Entities/                     ← EF Core entity models
│   │   ├── User.cs
│   │   ├── Patient.cs
│   │   ├── Doctor.cs
│   │   ├── Appointment.cs
│   │   ├── Specialization.cs
│   │   └── Prescription.cs
│   └── DTOs/
│       └── AllDtos.cs                ← All DTOs + PagedResult
│
├── HealthcareSystem.API/             ← Web API backend
│   ├── Controllers/                  ← AuthController, PatientsController, etc.
│   ├── Services/                     ← Business logic layer
│   │   └── Interfaces/
│   ├── Repositories/                 ← Data access layer
│   │   └── Interfaces/
│   ├── Data/
│   │   └── AppDbContext.cs           ← EF Core DbContext + Fluent API
│   ├── Mappings/
│   │   └── MappingProfile.cs         ← AutoMapper profiles
│   ├── Middleware/
│   │   └── Middleware.cs             ← Exception handler + Request logging
│   ├── Helpers/
│   │   └── JwtHelper.cs
│   ├── Migrations/
│   └── Program.cs                    ← DI, JWT, Serilog, Swagger config
│
└── HealthcareSystem.MVC/             ← MVC frontend
    ├── Controllers/
    │   ├── AccountController.cs
    │   └── MvcControllers.cs         ← Home, Patients, Doctors, Appointments, Admin
    ├── Services/
    │   └── ApiService.cs             ← HttpClient wrapper for all API calls
    ├── Views/
    │   ├── Account/                  ← Login, Register, AccessDenied
    │   ├── Home/                     ← AdminDashboard, PatientDashboard, DoctorDashboard
    │   ├── Patients/                 ← Index, Details, Create, Edit
    │   ├── Doctors/                  ← Index, Details, Create
    │   ├── Appointments/             ← Index, MyAppointments, Book
    │   ├── Admin/                    ← Users
    │   └── Shared/                   ← _Layout.cshtml
    ├── wwwroot/css/site.css
    ├── wwwroot/js/site.js
    └── Program.cs
```

---

## Database Relationships

| Relationship | Entities |
|---|---|
| One-to-One | `User` → `Patient` |
| One-to-One | `User` → `Doctor` |
| One-to-Many | `Doctor` → `Appointments` |
| One-to-Many | `Patient` → `Appointments` |
| Many-to-Many | `Doctor` ↔ `Specialization` (via `DoctorSpecialization`) |
| One-to-One | `Appointment` → `Prescription` |
| Many-to-Many | `Prescription` ↔ `Medicine` (via `PrescriptionMedicine`) |

---

## Roles & Permissions

| Action | Admin | Doctor | Patient |
|---|---|---|---|
| View all users | ✅ | ❌ | ❌ |
| Manage users | ✅ | ❌ | ❌ |
| View all patients | ✅ | ✅ | ❌ |
| View all appointments | ✅ | ✅ | ❌ |
| Book appointment | ❌ | ❌ | ✅ |
| Confirm/update appointment | ✅ | ✅ | ❌ |
| Issue prescription | ❌ | ✅ | ❌ |
| View own prescriptions | ❌ | ❌ | ✅ |
| Delete any record | ✅ | ❌ | ❌ |

---

## API Endpoints

### Auth
| Method | Endpoint | Description |
|---|---|---|
| POST | `/api/auth/login` | Login, returns JWT + refresh token |
| POST | `/api/auth/register` | Register new user |
| POST | `/api/auth/refresh` | Refresh expired token |

### Patients
| Method | Endpoint | Auth |
|---|---|---|
| GET | `/api/patients` | Admin |
| GET | `/api/patients/{id}` | Admin/Doctor/Patient |
| GET | `/api/patients/me` | Patient |
| POST | `/api/patients` | Patient/Admin |
| PUT | `/api/patients/{id}` | Patient/Admin |
| DELETE | `/api/patients/{id}` | Admin |

### Doctors
| Method | Endpoint | Auth |
|---|---|---|
| GET | `/api/doctors` | Public |
| GET | `/api/doctors/{id}` | Public |
| GET | `/api/doctors/by-specialization/{id}` | Public |
| POST | `/api/doctors` | Doctor/Admin |
| PUT | `/api/doctors/{id}` | Doctor/Admin |
| DELETE | `/api/doctors/{id}` | Admin |

### Appointments
| Method | Endpoint | Auth |
|---|---|---|
| GET | `/api/appointments` | Admin/Doctor |
| GET | `/api/appointments/{id}` | Authenticated |
| GET | `/api/appointments/my` | Patient |
| GET | `/api/appointments/by-date?date=YYYY-MM-DD` | Admin/Doctor |
| POST | `/api/appointments` | Patient |
| PUT | `/api/appointments/{id}` | Doctor/Admin |
| PATCH | `/api/appointments/{id}` | Doctor/Admin |
| DELETE | `/api/appointments/{id}` | Admin |

---

## Quick Setup

### Prerequisites
- .NET 8 SDK
- SQL Server (or SQL Server Express / LocalDB)
- Visual Studio 2022 or VS Code

### 1. Clone and configure

```bash
git clone <repo>
cd HealthcareSystem
```

Edit `HealthcareSystem.API/appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=HealthcareDB;Trusted_Connection=True;TrustServerCertificate=True;"
  },
  "Jwt": {
    "Key": "YourSuperSecretKeyHere_ChangeInProduction_MinLength32Chars!",
    "Issuer": "HealthcareAPI",
    "Audience": "HealthcareMVC",
    "ExpiryHours": "8"
  }
}
```

Edit `HealthcareSystem.MVC/appsettings.json`:
```json
{
  "ApiBaseUrl": "https://localhost:7000/"
}
```

### 2. Apply migrations (auto-applied on startup, or run manually)

```bash
cd HealthcareSystem.API
dotnet ef database update
```

### 3. Run both projects

**Terminal 1 — API (port 7000):**
```bash
cd HealthcareSystem.API
dotnet run --urls https://localhost:7000
```

**Terminal 2 — MVC (port 7001):**
```bash
cd HealthcareSystem.MVC
dotnet run --urls https://localhost:7001
```

Or in Visual Studio: Set multiple startup projects → both API and MVC.

### 4. Access the application

| URL | Description |
|---|---|
| `https://localhost:7001` | MVC Frontend |
| `https://localhost:7000/swagger` | Swagger API docs |

### 5. Default Admin Login

```
Email:    admin@healthcare.com
Password: Admin@123
```

---

## Architecture Highlights

### Repository Pattern
Each entity has a dedicated repository implementing `IGenericRepository<T>` with additional
entity-specific methods. All repositories are injected via `IServiceCollection.AddScoped`.

### Service Layer
Business logic is cleanly separated in service classes. Services use repositories
and AutoMapper to transform entities → DTOs before returning to controllers.

### AutoMapper
Configured in `MappingProfile.cs`. Maps bidirectionally between entities and DTOs,
including nested properties (e.g., `Doctor.User.FullName` → `DoctorDto.FullName`).

### Caching
`IMemoryCache` caches the doctor list by specialization (10 min TTL) and all specializations
(30 min absolute, 10 min sliding). Cache is invalidated on any mutation.

### Middleware Pipeline
```
Request → ExceptionMiddleware → RequestLoggingMiddleware → HTTPS → CORS →
Authentication → Authorization → Controllers
```

### JWT Flow
1. User POSTs credentials to `/api/auth/login`
2. API returns `{ token, refreshToken, role, expiry }`
3. MVC stores token in `ISession`
4. Every API call attaches `Authorization: Bearer <token>`
5. On 401, client POSTs to `/api/auth/refresh` with the refresh token

---

## Deployment (IIS)

1. Publish both projects: `dotnet publish -c Release`
2. Create two IIS sites pointing to the publish folders
3. Set environment variables (`ASPNETCORE_ENVIRONMENT=Production`)
4. Update `appsettings.json` connection strings and JWT key for production
5. Ensure HTTPS bindings are configured

---

## Modules Implemented

| Module | Status |
|---|---|
| 1. Project Setup & Architecture | ✅ Clean layered architecture |
| 2. Database Design (EF Core) | ✅ All 7 entities, all relationship types, Fluent API |
| 3. Web API (all HTTP verbs) | ✅ GET/POST/PUT/PATCH/DELETE with proper status codes |
| 4. DTO & AutoMapper | ✅ Full DTO layer + mapping profiles |
| 5. JWT Authentication | ✅ Login, token generation, refresh tokens |
| 6. Role-Based Authorization | ✅ Admin/Doctor/Patient roles enforced |
| 7. Client & Server Validation | ✅ Data annotations + JS validation |
| 8. Caching | ✅ In-memory cache with expiry policies |
| 9. Logging (Serilog) | ✅ Console + rolling file logs |
| 10. Exception Middleware | ✅ Structured error responses |
| 11. MVC Frontend | ✅ Full UI with HttpClient integration |
| 12. Security & HTTPS | ✅ HTTPS redirect, JWT, CORS |
| 13. Advanced Routing | ✅ Attribute routing, route constraints, query params |
| 14. Bonus Features | ✅ Pagination, search, Swagger, refresh tokens |
