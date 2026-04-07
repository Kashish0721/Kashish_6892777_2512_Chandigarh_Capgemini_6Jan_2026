# Inventory System API - Hands-On Learning Guide

## Overview
This is a skeleton implementation of two case studies for learning ASP.NET Core API development with unit testing using xUnit and NUnit frameworks. Participants will implement the controllers, services, and tests.

## Project Structure

```
InventorySystem/
├── InventorySystem.API/                 # Main Web API project
│   ├── Controllers/
│   │   ├── ProductController.cs        # TODO: Implement GetProduct endpoint
│   │   └── OrderController.cs          # TODO: Implement PlaceOrder endpoint
│   ├── Program.cs                      # TODO: Configure dependency injection
│
├── InventorySystem.Services/            # Business logic library
│   ├── Models/
│   │   ├── Product.cs                  # Product model
│   │   └── Order.cs                    # Order and OrderItem models
│   ├── Interfaces/
│   │   ├── IProductService.cs          # Interface definition
│   │   └── IOrderService.cs            # Interface definition
│   └── Services/
│       ├── ProductService.cs           # TODO: Implement business logic
│       └── OrderService.cs             # TODO: Implement business logic
│
├── InventorySystem.API.Tests.xUnit/     # xUnit test project
│   ├── ProductControllerTests.cs       # TODO: Implement xUnit tests
│   └── OrderControllerTests.cs         # TODO: Implement xUnit tests
│
└── InventorySystem.API.Tests.NUnit/     # NUnit test project
    ├── ProductControllerTests.cs       # TODO: Implement NUnit tests
    └── OrderControllerTests.cs         # TODO: Implement NUnit tests
```

---

## Case Study 1: Product Inventory API

### Requirements

**Endpoint:** `GET /api/product/{id}`

**Service Interface:**
```csharp
Task<Product> GetProductByIdAsync(int id)
```

**Expected Responses:**
- ✅ **200 OK**: Returns product details when product exists
- ❌ **404 Not Found**: When product does not exist

### What You Need to Implement

#### 1. ProductService (InventorySystem.Services/Services/ProductService.cs)
```csharp
public async Task<Product> GetProductByIdAsync(int id)
{
    // TODO: Implement logic to:
    // - Find product by ID from data source
    // - Return Product object if found
    // - Return null if not found
    // For now, you can use a hardcoded list or return null for testing
}
```

#### 2. ProductController (InventorySystem.API/Controllers/ProductController.cs)
```csharp
[HttpGet("{id}")]
public async Task<IActionResult> GetProduct(int id)
{
    // TODO: Implement logic to:
    // - Call _productService.GetProductByIdAsync(id)
    // - If product != null: return Ok(product) [200 OK]
    // - If product == null: return NotFound() [404 Not Found]
}
```

#### 3. xUnit Tests (InventorySystem.API.Tests.xUnit/ProductControllerTests.cs)
Two test cases to implement:

**Test 1: Valid Product ID**
```csharp
[Fact]
public async Task GetProduct_WithValidId_ReturnsOkObjectResult()
{
    // TODO: Arrange
    // - Mock _mockProductService.Setup(x => x.GetProductByIdAsync(1))
    //   .ReturnsAsync(new Product { Id = 1, Name = "Laptop", Price = 999 })
    
    // TODO: Act
    // - Call _controller.GetProduct(1)
    
    // TODO: Assert
    // - Verify result is OkObjectResult
    // - Verify product details are correct
}
```

**Test 2: Invalid Product ID**
```csharp
[Fact]
public async Task GetProduct_WithInvalidId_ReturnsNotFoundResult()
{
    // TODO: Arrange
    // - Mock _mockProductService.Setup(x => x.GetProductByIdAsync(99))
    //   .ReturnsAsync((Product)null)
    
    // TODO: Act
    // - Call _controller.GetProduct(99)
    
    // TODO: Assert
    // - Verify result is NotFoundResult
}
```

#### 4. NUnit Tests (InventorySystem.API.Tests.NUnit/ProductControllerTests.cs)
Same tests as xUnit but using NUnit syntax:
- Replace `[Fact]` with `[Test]`
- Note: Constructor injection → `[SetUp]` method pattern

---

## Case Study 2: Order Processing API

### Requirements

**Endpoint:** `POST /api/order`

**Service Interface:**
```csharp
Task<bool> PlaceOrderAsync(Order order)
```

**Expected Responses:**
- ✅ **201 Created**: When order is successfully placed
- ❌ **400 Bad Request**: When order placement fails

### What You Need to Implement

#### 1. OrderService (InventorySystem.Services/Services/OrderService.cs)
```csharp
public async Task<bool> PlaceOrderAsync(Order order)
{
    // TODO: Implement logic to:
    // - Validate the order (check items, quantities, etc.)
    // - If invalid: return false
    // - If valid: save order and return true
    // For testing purposes, you can validate that:
    //   - Order has items
    //   - Each item has positive quantity
    //   - Total amount is greater than 0
}
```

#### 2. OrderController (InventorySystem.API/Controllers/OrderController.cs)
```csharp
[HttpPost]
public async Task<IActionResult> PlaceOrder([FromBody] Order order)
{
    // TODO: Implement logic to:
    // - Call _orderService.PlaceOrderAsync(order)
    // - If true (success): return CreatedAtAction(...) [201 Created]
    // - If false (failure): return BadRequest() [400 Bad Request]
}
```

#### 3. xUnit Tests (InventorySystem.API.Tests.xUnit/OrderControllerTests.cs)
Two test cases to implement:

**Test 1: Valid Order**
```csharp
[Fact]
public async Task PlaceOrder_WithValidOrder_ReturnsCreatedResult()
{
    // TODO: Arrange
    // - Create valid order with items
    // - Mock _mockOrderService.Setup(x => x.PlaceOrderAsync(It.IsAny<Order>()))
    //   .ReturnsAsync(true)
    
    // TODO: Act
    // - Call _controller.PlaceOrder(order)
    
    // TODO: Assert
    // - Verify result is CreatedResult (status 201)
}
```

**Test 2: Invalid Order**
```csharp
[Fact]
public async Task PlaceOrder_WithInvalidOrder_ReturnsBadRequestResult()
{
    // TODO: Arrange
    // - Create invalid order
    // - Mock _mockOrderService.Setup(x => x.PlaceOrderAsync(It.IsAny<Order>()))
    //   .ReturnsAsync(false)
    
    // TODO: Act
    // - Call _controller.PlaceOrder(order)
    
    // TODO: Assert
    // - Verify result is BadRequestResult
}
```

#### 4. NUnit Tests (InventorySystem.API.Tests.NUnit/OrderControllerTests.cs)
Same tests as xUnit but using NUnit syntax.

---

## Key Differences: xUnit vs NUnit

| Aspect | xUnit | NUnit |
|--------|-------|-------|
| **Test Attribute** | `[Fact]` | `[Test]` |
| **Test Class** | No attribute needed | `[TestFixture]` |
| **Setup** | Constructor injection | `[SetUp]` method |
| **Teardown** | `IDisposable.Dispose()` | `[TearDown]` method |
| **Theory/Parameterized** | `[Theory]` + `[InlineData]` | `[TestCase(...)]` |
| **Assertion Library** | `Xunit.Assert` | `NUnit.Framework.Assert` |

---

## Common Moq Patterns

### Setting up a mock to return a value
```csharp
_mockProductService
    .Setup(x => x.GetProductByIdAsync(1))
    .ReturnsAsync(new Product { Id = 1, Name = "Laptop" });
```

### Setting up a mock to return null
```csharp
_mockProductService
    .Setup(x => x.GetProductByIdAsync(99))
    .ReturnsAsync((Product)null);
```

### Using It.IsAny for flexible matching
```csharp
_mockOrderService
    .Setup(x => x.PlaceOrderAsync(It.IsAny<Order>()))
    .ReturnsAsync(true);
```

### Verifying mock was called
```csharp
_mockProductService.Verify(
    x => x.GetProductByIdAsync(1),
    Times.Once()
);
```

---

## Dependency Injection Setup

You need to configure dependency injection in `Program.cs`. Add these services:

```csharp
// In Program.cs
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IOrderService, OrderService>();
```

---

## Running Tests

### Run all tests
```bash
dotnet test
```

### Run only xUnit tests
```bash
dotnet test InventorySystem.API.Tests.xUnit/InventorySystem.API.Tests.xUnit.csproj
```

### Run only NUnit tests
```bash
dotnet test InventorySystem.API.Tests.NUnit/InventorySystem.API.Tests.NUnit.csproj
```

### Run with verbose output
```bash
dotnet test --verbosity detailed
```

---

## Implementation Checklist

### Case Study 1 - Product API
- [ ] Implement `ProductService.GetProductByIdAsync()`
- [ ] Implement `ProductController.GetProduct()`
- [ ] Implement xUnit: `GetProduct_WithValidId_ReturnsOkObjectResult`
- [ ] Implement xUnit: `GetProduct_WithInvalidId_ReturnsNotFoundResult`
- [ ] Implement NUnit: `GetProduct_WithValidId_ReturnsOkObjectResult`
- [ ] Implement NUnit: `GetProduct_WithInvalidId_ReturnsNotFoundResult`
- [ ] All tests pass ✅

### Case Study 2 - Order API
- [ ] Implement `OrderService.PlaceOrderAsync()`
- [ ] Implement `OrderController.PlaceOrder()`
- [ ] Implement xUnit: `PlaceOrder_WithValidOrder_ReturnsCreatedResult`
- [ ] Implement xUnit: `PlaceOrder_WithInvalidOrder_ReturnsBadRequestResult`
- [ ] Implement NUnit: `PlaceOrder_WithValidOrder_ReturnsCreatedResult`
- [ ] Implement NUnit: `PlaceOrder_WithInvalidOrder_ReturnsBadRequestResult`
- [ ] All tests pass ✅

---

## Learning Goals

✅ Understand dependency injection and mocking  
✅ Learn controller testing patterns  
✅ Compare xUnit vs NUnit syntax and structure  
✅ Practice TDD approach (write tests, implement logic)  
✅ Understand async/await in controllers and services  
✅ Master Moq setup patterns  
✅ Learn HTTP status codes and REST conventions  

---

## Tips

1. **Start with the service implementation** - Implement the business logic first, then the controller, then the tests
2. **Use meaningful test names** - Follow the pattern: `MethodName_Condition_ExpectedResult`
3. **Test one thing per test** - Each test should verify a single behavior
4. **Mock external dependencies** - Never hit a real database in tests
5. **Run tests frequently** - Validate your work as you go

---

## Useful Resources

- [Moq Documentation](https://github.com/moq/moq4)
- [xUnit Docs](https://xunit.net/)
- [NUnit Docs](https://docs.nunit.org/)
- [ASP.NET Core Testing Best Practices](https://docs.microsoft.com/en-us/dotnet/core/testing/)
- [Microsoft REST API Guidelines](https://github.com/microsoft/api-guidelines)
