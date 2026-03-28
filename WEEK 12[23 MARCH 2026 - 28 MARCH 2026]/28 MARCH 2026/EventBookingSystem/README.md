# 🎪 EventHub — Event Booking & Management System
### Capgemini Week-12 Assessment | ASP.NET Core 8

---

## 📋 Project Overview

A full-stack Event Booking & Management System built with:

| Layer      | Technology                                      |
|------------|-------------------------------------------------|
| Backend    | ASP.NET Core 8 Web API                          |
| ORM        | Entity Framework Core 8 (SQL Server)            |
| Auth       | JWT Bearer Authentication                       |
| Mapping    | AutoMapper 13                                   |
| Passwords  | BCrypt.Net                                      |
| Frontend   | Razor Pages + Bootstrap 5 + Vanilla JS          |
| Docs       | Swagger / OpenAPI                               |

---

## 🗂️ Solution Structure

```
EventBookingSystem/
├── EventBookingSystem.sln
│
├── EventBooking.API/                  ← ASP.NET Core Web API
│   ├── Controllers/
│   │   ├── AuthController.cs          ← Register / Login → JWT
│   │   ├── EventsController.cs        ← GET/POST/PUT/DELETE api/events
│   │   └── BookingsController.cs      ← GET/POST/DELETE api/bookings
│   ├── Data/
│   │   └── AppDbContext.cs            ← EF Core DbContext + seed data
│   ├── DTOs/
│   │   └── Dtos.cs                    ← EventDto, BookingDto, AuthDto …
│   ├── Entities/
│   │   ├── Event.cs                   ← Event entity
│   │   ├── Booking.cs                 ← Booking entity
│   │   └── User.cs                    ← User entity
│   ├── Mappings/
│   │   └── MappingProfile.cs          ← AutoMapper profiles
│   ├── Migrations/                    ← EF Core migrations
│   ├── Services/
│   │   └── JwtService.cs              ← JWT token generation
│   ├── Validators/
│   │   └── FutureDateAttribute.cs     ← Custom [FutureDate] annotation
│   ├── appsettings.json
│   └── Program.cs                     ← DI, middleware, JWT, CORS, Swagger
│
└── EventBooking.Web/                  ← Razor Pages Frontend
    ├── Pages/
    │   ├── Index.cshtml               ← Home page with featured events
    │   ├── Events/
    │   │   ├── Index.cshtml           ← Browse & filter all events
    │   │   ├── Details.cshtml         ← Event detail + booking form
    │   │   └── Create.cshtml          ← Admin: create new event
    │   ├── Bookings/
    │   │   ├── MyBookings.cshtml      ← User: view & cancel bookings
    │   │   └── AllBookings.cshtml     ← Admin: all system bookings
    │   ├── Account/
    │   │   ├── Login.cshtml           ← Login form
    │   │   ├── Register.cshtml        ← Register form
    │   │   └── Logout.cshtml          ← Sign-out handler
    │   └── Shared/
    │       └── _Layout.cshtml         ← Shared navbar + footer
    ├── wwwroot/
    │   ├── css/site.css               ← Custom premium styles
    │   └── js/site.js                 ← Auth helpers, API fetch, toasts
    ├── appsettings.json
    └── Program.cs
```

---

## ⚡ Quick Setup

### Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8)
- [SQL Server LocalDB](https://learn.microsoft.com/en-us/sql/database-engine/configure-windows/sql-server-express-localdb) (ships with Visual Studio)
- Visual Studio 2022 (v17.8+) **or** VS Code

---

### Step 1 — Open the Solution

```
File → Open → Solution/Project → EventBookingSystem.sln
```

---

### Step 2 — Configure the Connection String

Open `EventBooking.API/appsettings.json`.  
The default uses LocalDB (no setup needed for Visual Studio):

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=EventBookingDB;Trusted_Connection=True;"
}
```

For full SQL Server, replace with your connection string.

---

### Step 3 — Apply Database Migration

**Option A — Visual Studio Package Manager Console:**
```powershell
# Make sure Default Project = EventBooking.API
Update-Database
```

**Option B — .NET CLI:**
```bash
cd EventBooking.API
dotnet ef database update
```

This creates the `EventBookingDB` database and seeds:
- Admin user: `admin@eventbooking.com` / `Admin@123`
- Regular user: `john@example.com` / `User@123`
- 4 sample events

---

### Step 4 — Configure Multiple Startup Projects

Right-click **Solution** → **Set Startup Projects** → **Multiple startup projects**:

| Project             | Action  |
|---------------------|---------|
| EventBooking.API    | Start   |
| EventBooking.Web    | Start   |

Click **OK**, then press **F5**.

Default URLs:
- **API**: `https://localhost:7000` | Swagger: `https://localhost:7000/swagger`
- **Web**: `https://localhost:7001`

---

## 🔑 API Endpoints

### Auth
| Method | Endpoint              | Auth     | Description       |
|--------|-----------------------|----------|-------------------|
| POST   | `/api/auth/register`  | Public   | Create account    |
| POST   | `/api/auth/login`     | Public   | Get JWT token     |

### Events
| Method | Endpoint              | Auth     | Description       |
|--------|-----------------------|----------|-------------------|
| GET    | `/api/events`         | Public   | List all events   |
| GET    | `/api/events/{id}`    | Public   | Get event detail  |
| POST   | `/api/events`         | Admin    | Create event      |
| PUT    | `/api/events/{id}`    | Admin    | Update event      |
| DELETE | `/api/events/{id}`    | Admin    | Soft-delete event |

### Bookings
| Method | Endpoint                | Auth   | Description            |
|--------|-------------------------|--------|------------------------|
| GET    | `/api/bookings`         | User   | My bookings            |
| GET    | `/api/bookings/all`     | Admin  | All system bookings    |
| GET    | `/api/bookings/{id}`    | User   | Single booking         |
| POST   | `/api/bookings`         | User   | Book tickets           |
| DELETE | `/api/bookings/{id}`    | User   | Cancel booking         |

---

## ✅ Assessment Checklist

| Requirement                                      | Status |
|--------------------------------------------------|--------|
| EF Core — Event & Booking entities               | ✅     |
| Custom entity fields (TicketPrice, Category …)   | ✅     |
| EF Core CRUD operations                          | ✅     |
| DTOs — EventDto, BookingDto, AuthResponseDto     | ✅     |
| AutoMapper — entity ↔ DTO mappings               | ✅     |
| JWT Authentication (register + login)            | ✅     |
| Secure booking endpoints [Authorize]             | ✅     |
| Attribute-based routing                          | ✅     |
| [Required] on EventTitle                         | ✅     |
| [Range] on seat numbers                          | ✅     |
| [FutureDate] custom validation attribute         | ✅     |
| Razor Pages UI with Bootstrap                    | ✅     |
| Bootstrap cards for event display                | ✅     |
| Booking form with Bootstrap styling              | ✅     |
| JS: seat number validation before submit         | ✅     |
| JS: cannot exceed available seats check          | ✅     |
| fetch() API integration from Razor Pages         | ✅     |
| JWT token sent in fetch() Authorization header   | ✅     |
| Dynamic booking confirmation display             | ✅     |
| Swagger / OpenAPI documentation                  | ✅     |
| Seed data (users + events)                       | ✅     |
| Admin role — manage events & view all bookings   | ✅     |

---

## 🎨 Features Beyond Requirements

- **Password strength meter** on register page
- **Pagination & debounced search** on events list
- **Seat availability progress bar** (turns red when < 25%)
- **Booking stats dashboard** (total, confirmed, seats, spent)
- **Cancel confirmation modal** with Bootstrap
- **Toast notifications** for all async actions
- **Soft-delete** for events (IsActive flag)
- **Admin-only routes** protected both on API and Razor Pages
- **24-hour cancellation policy** enforced server-side
- **Duplicate booking prevention**
- **Responsive design** — mobile-friendly navbar + cards

---

## 📝 Demo Credentials

| Role  | Email                      | Password   |
|-------|----------------------------|------------|
| Admin | admin@eventbooking.com     | Admin@123  |
| User  | john@example.com           | User@123   |

---

*Built for Capgemini Week-12 Assessment — ASP.NET Core 8 · EF Core · JWT · AutoMapper · Razor Pages · Bootstrap 5*
