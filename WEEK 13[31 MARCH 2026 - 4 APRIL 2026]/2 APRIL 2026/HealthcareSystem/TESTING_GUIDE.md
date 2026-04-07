# Healthcare System - Testing & Verification Guide

## Quick Start Setup

### 1. Database Connection String
Ensure your `appsettings.Development.json` has a valid SQL Server connection:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER;Database=HealthcareSystemDb;Trusted_Connection=true;TrustServerCertificate=true;"
  }
}
```

### 2. Start the API Server
```bash
cd HealthcareSystem.API
dotnet run
```
**Expected Output**:
```
Now listening on: http://localhost:5000
Now listening on: https://localhost:5001
```

### 3. Access Swagger UI
Open browser and navigate to:
```
http://localhost:5000/swagger
```

---

## Testing Department Endpoints

### 1. Get All Departments (Public)
```http
GET /api/departments
```
**Expected Response** (200 OK):
```json
[
  {
    "id": 1,
    "name": "Cardiology",
    "description": "Heart and cardiovascular system",
    "doctorCount": 0
  },
  {
    "id": 2,
    "name": "Neurology",
    "description": "Brain and nervous system",
    "doctorCount": 0
  }
  // ... more departments
]
```

### 2. Get Department by ID
```http
GET /api/departments/1
```
**Expected Response** (200 OK):
```json
{
  "id": 1,
  "name": "Cardiology",
  "description": "Heart and cardiovascular system",
  "doctorCount": 0
}
```

### 3. Create Department (Admin only)
```http
POST /api/departments
Content-Type: application/json
Authorization: Bearer {admin_token}

{
  "name": "Oncology",
  "description": "Cancer treatment and research"
}
```
**Expected Response** (201 Created):
```json
{
  "id": 7,
  "name": "Oncology",
  "description": "Cancer treatment and research",
  "doctorCount": 0
}
```

### 4. Update Department
```http
PUT /api/departments/1
Content-Type: application/json
Authorization: Bearer {admin_token}

{
  "name": "Cardiology - Updated",
  "description": "Heart, cardiovascular, and thoracic system"
}
```
**Expected Response** (200 OK): Updated department data

### 5. Delete Department
```http
DELETE /api/departments/1
Authorization: Bearer {admin_token}
```
**Expected Response** (204 No Content)

---

## Testing Bill Endpoints

### 1. Create Bill (Admin)
**Prerequisite**: Must have an existing appointment ID
```http
POST /api/bills
Content-Type: application/json
Authorization: Bearer {admin_token}

{
  "appointmentId": 1,
  "consultationFee": 500.00,
  "medicineCharges": 250.00
}
```
**Expected Response** (201 Created):
```json
{
  "id": 1,
  "appointmentId": 1,
  "patientName": "John Doe",
  "doctorName": "Dr. Smith",
  "consultationFee": 500.00,
  "medicineCharges": 250.00,
  "totalAmount": 750.00,
  "paymentStatus": "Unpaid",
  "createdAt": "2026-04-04T07:15:00Z",
  "paidAt": null
}
```

### 2. Get My Bills (Patient)
```http
GET /api/bills/my
Authorization: Bearer {patient_token}
```
**Expected Response** (200 OK): Array of patient's bills

### 3. Get Bill by Appointment
```http
GET /api/bills/appointment/1
Authorization: Bearer {token}
```
**Expected Response** (200 OK): Bill data for the appointment

### 4. Mark Bill as Paid
```http
PUT /api/bills/1/pay
Authorization: Bearer {patient_or_admin_token}
```
**Expected Response** (200 OK):
```json
{
  "id": 1,
  "appointmentId": 1,
  "patientName": "John Doe",
  "doctorName": "Dr. Smith",
  "consultationFee": 500.00,
  "medicineCharges": 250.00,
  "totalAmount": 750.00,
  "paymentStatus": "Paid",
  "createdAt": "2026-04-04T07:15:00Z",
  "paidAt": "2026-04-04T07:20:00Z"
}
```

### 5. Update Bill
```http
PUT /api/bills/1
Content-Type: application/json
Authorization: Bearer {admin_token}

{
  "consultationFee": 525.00,
  "medicineCharges": 275.00,
  "paymentStatus": "Unpaid"
}
```
**Expected Response** (200 OK): Updated bill data

### 6. Delete Bill
```http
DELETE /api/bills/1
Authorization: Bearer {admin_token}
```
**Expected Response** (204 No Content)

---

## Testing Doctor-Department Association

### 1. Create Doctor with Department
```http
POST /api/doctors
Content-Type: application/json
Authorization: Bearer {admin_token}

{
  "departmentId": 1,  // Cardiology
  "licenseNumber": "MD123456",
  "phoneNumber": "+1234567890",
  "yearsOfExperience": 10,
  "consultationFee": 500.00,
  "biography": "Expert cardiologist with 10 years experience",
  "specializationIds": [1]
}
```

### 2. Get Doctor (will show Department name)
```http
GET /api/doctors/1
```
**Expected Response**:
```json
{
  "id": 1,
  "userId": 1,
  "departmentId": 1,
  "departmentName": "Cardiology",  // ← Added field
  "fullName": "Dr. Smith",
  "email": "smith@hospital.com",
  "phoneNumber": "+1234567890",
  "licenseNumber": "MD123456",
  "yearsOfExperience": 10,
  "consultationFee": 500.00,
  "biography": "Expert cardiologist with 10 years experience",
  "isAvailable": true,
  "specializations": ["Cardiology"],
  "totalAppointments": 0
}
```

---

## Common Error Scenarios & Fixes

### Error 1: "Unauthorized" (401)
**Cause**: Missing or invalid JWT token
**Solution**: 
1. Login first to get token
2. Add `Authorization: Bearer {token}` header to requests

### Error 2: "Forbidden" (403)
**Cause**: User doesn't have required role
**Solution**: 
- Use admin token for admin-only operations
- Check role in token matches endpoint requirement

### Error 3: "Not Found" (404)
**Cause**: Resource doesn't exist
**Solution**:
- Verify ID exists in database
- Check resource type matches endpoint

### Error 4: "Bad Request" (400)
**Cause**: Validation error
**Solution**:
- Check request body matches DTO requirements
- Verify all Required fields are present
- Ensure field lengths don't exceed maximums

### Error 5: Database Connection Error
**Cause**: Connection string invalid or SQL Server not running
**Solution**:
1. Verify SQL Server is running
2. Check connection string in appsettings.json
3. Verify credentials and database name

---

## Database Verification

### Check if Migration Applied
```sql
USE HealthcareSystemDb;

-- Check tables exist
SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo';

-- Check Departments table
SELECT * FROM Departments;

-- Check Bills table
SELECT * FROM Bills;

-- Check Doctor-Department relationship
SELECT d.Id, d.UserId, d.DepartmentId, dep.Name 
FROM Doctors d 
LEFT JOIN Departments dep ON d.DepartmentId = dep.Id;

-- Check migration history
SELECT * FROM __EFMigrationsHistory;
```

---

## Logging & Debugging

### View Detailed Logs
Set log level in `appsettings.Development.json`:
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "HealthcareSystem": "Debug"
    }
  }
}
```

### Common Log Messages to Look For
- ✅ "Department {Name} created with ID {Id}"
- ✅ "Bill created for Appointment {AppointmentId}"
- ✅ "Bill {Id} marked as paid"
- ✅ "Fetching all bills" for GET operations

---

## Performance Considerations

### Caching
Department endpoints use in-memory caching with:
- **Duration**: 30 minutes absolute expiration
- **Sliding**: 10 minutes sliding expiration
- **Cache Key**: "departments_all"

**Note**: Cache is automatically invalidated on Create/Update/Delete operations

---

## Security Checklist

- ✅ JWT token validation enabled
- ✅ Role-based authorization on sensitive endpoints
- ✅ HTTPS configured in appsettings
- ✅ Password hashing (uses BCrypt in auth service)
- ✅ CORS enabled for MVC app (localhost:5001)
- ✅ SQL injection protection (EF Core parameterized queries)

---

## Integration with MVC

To test MVC integration with the new endpoints:

### 1. Create ApiService Methods (in MVC)
```csharp
public async Task<DepartmentDto> GetDepartmentAsync(int id)
{
    var response = await _httpClient.GetAsync($"/api/departments/{id}");
    if (response.IsSuccessStatusCode)
        return await response.Content.ReadAsAsync<DepartmentDto>();
    return null;
}

public async Task<BillDto> GetBillAsync(int id)
{
    var response = await _httpClient.GetAsync($"/api/bills/{id}");
    if (response.IsSuccessStatusCode)
        return await response.Content.ReadAsAsync<BillDto>();
    return null;
}
```

### 2. Create Controller Actions (in MVC)
```csharp
[HttpGet]
public async Task<IActionResult> Departments()
{
    var departments = await _apiService.GetAllDepartmentsAsync();
    return View(departments);
}

[HttpGet]
public async Task<IActionResult> Bills()
{
    var bills = await _apiService.GetMyBillsAsync();
    return View(bills);
}
```

### 3. Create Views
- `Views/Departments/Index.cshtml` - List all departments
- `Views/Bills/Index.cshtml` - List patient's bills
- `Views/Bills/Details.cshtml` - Bill details with payment option

---

## Troubleshooting Checklist

- [ ] Database migration applied successfully (check __EFMigrationsHistory)
- [ ] Departments table has 6 seed rows
- [ ] API builds without errors (`dotnet build`)
- [ ] API starts without errors (`dotnet run`)
- [ ] Swagger UI loads (`http://localhost:5000/swagger`)
- [ ] Can get departments without auth token
- [ ] Can create department with admin token
- [ ] Can create bill with valid appointment ID
- [ ] Payment status changes from "Unpaid" to "Paid" after MarkAsPaid
- [ ] MVC app can connect to API endpoints

---

## Success Metrics

✅ **Complete** when:
1. All Department endpoints return expected responses
2. All Bill endpoints return expected responses  
3. Database contains Departments and Bills tables
4. Doctor entity has DepartmentId column populated
5. Payment status changes work correctly
6. MVC app displays department and bill information
7. No compilation errors in solution

---

**Last Updated**: April 4, 2026  
**Status**: Ready for Testing ✅
