# Solution Reference - Worked Examples

## Important
These are **complete implementations** for reference. Participants should attempt to implement these themselves first using the skeleton code!

---

## Case Study 1: Product Inventory API - Complete Solution

### ProductService Implementation
```csharp
public class ProductService : IProductService
{
    // In-memory storage for demonstration
    private static readonly List<Product> Products = new()
    {
        new Product { Id = 1, Name = "Laptop", Description = "High-performance laptop", Price = 999.99m, Quantity = 5 },
        new Product { Id = 2, Name = "Mouse", Description = "Wireless mouse", Price = 29.99m, Quantity = 50 },
        new Product { Id = 3, Name = "Keyboard", Description = "Mechanical keyboard", Price = 149.99m, Quantity = 20 }
    };

    public async Task<Product> GetProductByIdAsync(int id)
    {
        // Simulate async database call
        await Task.Delay(10);
        
        return Products.FirstOrDefault(p => p.Id == id);
    }
}
```

### ProductController Implementation
```csharp
[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProduct(int id)
    {
        var product = await _productService.GetProductByIdAsync(id);
        
        if (product == null)
            return NotFound();
        
        return Ok(product);
    }
}
```

### xUnit Tests - Complete Implementation
```csharp
public class ProductControllerTests
{
    private readonly Mock<IProductService> _mockProductService;
    private readonly ProductController _controller;

    public ProductControllerTests()
    {
        _mockProductService = new Mock<IProductService>();
        _controller = new ProductController(_mockProductService.Object);
    }

    [Fact]
    public async Task GetProduct_WithValidId_ReturnsOkObjectResult()
    {
        // Arrange
        var productId = 1;
        var expectedProduct = new Product 
        { 
            Id = 1, 
            Name = "Laptop", 
            Description = "High-performance laptop",
            Price = 999.99m, 
            Quantity = 5 
        };

        _mockProductService
            .Setup(x => x.GetProductByIdAsync(productId))
            .ReturnsAsync(expectedProduct);

        // Act
        var result = await _controller.GetProduct(productId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.NotNull(okResult.Value);
        
        var returnedProduct = Assert.IsType<Product>(okResult.Value);
        Assert.Equal(1, returnedProduct.Id);
        Assert.Equal("Laptop", returnedProduct.Name);
        Assert.Equal(999.99m, returnedProduct.Price);
        
        // Verify the service was called correctly
        _mockProductService.Verify(
            x => x.GetProductByIdAsync(productId), 
            Times.Once()
        );
    }

    [Fact]
    public async Task GetProduct_WithInvalidId_ReturnsNotFoundResult()
    {
        // Arrange
        var invalidProductId = 99;
        
        _mockProductService
            .Setup(x => x.GetProductByIdAsync(invalidProductId))
            .ReturnsAsync((Product)null);

        // Act
        var result = await _controller.GetProduct(invalidProductId);

        // Assert
        Assert.IsType<NotFoundResult>(result);
        
        // Verify the service was called with the invalid ID
        _mockProductService.Verify(
            x => x.GetProductByIdAsync(invalidProductId), 
            Times.Once()
        );
    }
}
```

### NUnit Tests - Complete Implementation
```csharp
[TestFixture]
public class ProductControllerTests
{
    private Mock<IProductService> _mockProductService;
    private ProductController _controller;

    [SetUp]
    public void Setup()
    {
        _mockProductService = new Mock<IProductService>();
        _controller = new ProductController(_mockProductService.Object);
    }

    [Test]
    public async Task GetProduct_WithValidId_ReturnsOkObjectResult()
    {
        // Arrange
        var productId = 1;
        var expectedProduct = new Product 
        { 
            Id = 1, 
            Name = "Laptop", 
            Description = "High-performance laptop",
            Price = 999.99m, 
            Quantity = 5 
        };

        _mockProductService
            .Setup(x => x.GetProductByIdAsync(productId))
            .ReturnsAsync(expectedProduct);

        // Act
        var result = await _controller.GetProduct(productId);

        // Assert
        Assert.IsInstanceOf<OkObjectResult>(result);
        Assert.IsNotNull(result);
        
        var okResult = (OkObjectResult)result;
        var returnedProduct = (Product)okResult.Value;
        
        Assert.That(returnedProduct.Id, Is.EqualTo(1));
        Assert.That(returnedProduct.Name, Is.EqualTo("Laptop"));
        Assert.That(returnedProduct.Price, Is.EqualTo(999.99m));
        
        _mockProductService.Verify(
            x => x.GetProductByIdAsync(productId), 
            Times.Once()
        );
    }

    [Test]
    public async Task GetProduct_WithInvalidId_ReturnsNotFoundResult()
    {
        // Arrange
        var invalidProductId = 99;
        
        _mockProductService
            .Setup(x => x.GetProductByIdAsync(invalidProductId))
            .ReturnsAsync((Product)null);

        // Act
        var result = await _controller.GetProduct(invalidProductId);

        // Assert
        Assert.IsInstanceOf<NotFoundResult>(result);
        
        _mockProductService.Verify(
            x => x.GetProductByIdAsync(invalidProductId), 
            Times.Once()
        );
    }
}
```

---

## Case Study 2: Order Processing API - Complete Solution

### OrderService Implementation
```csharp
public class OrderService : IOrderService
{
    public async Task<bool> PlaceOrderAsync(Order order)
    {
        // Simulate async database call
        await Task.Delay(10);
        
        // Validation logic
        if (order == null)
            return false;
        
        if (!order.Items.Any())
            return false;
        
        if (order.Items.Any(item => item.Quantity <= 0))
            return false;
        
        if (order.TotalAmount <= 0)
            return false;
        
        // If all validations pass, save the order
        order.Id = GenerateOrderId();
        order.OrderDate = DateTime.UtcNow;
        order.Status = "Pending";
        
        return true;
    }

    private static int GenerateOrderId()
    {
        // Simulate ID generation
        return new Random().Next(1000, 9999);
    }
}
```

### OrderController Implementation
```csharp
[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpPost]
    public async Task<IActionResult> PlaceOrder([FromBody] Order order)
    {
        var success = await _orderService.PlaceOrderAsync(order);
        
        if (!success)
            return BadRequest(new { message = "Failed to place order" });
        
        return CreatedAtAction(nameof(PlaceOrder), new { id = order.Id }, order);
    }
}
```

### xUnit Tests - Complete Implementation
```csharp
public class OrderControllerTests
{
    private readonly Mock<IOrderService> _mockOrderService;
    private readonly OrderController _controller;

    public OrderControllerTests()
    {
        _mockOrderService = new Mock<IOrderService>();
        _controller = new OrderController(_mockOrderService.Object);
    }

    [Fact]
    public async Task PlaceOrder_WithValidOrder_ReturnsCreatedResult()
    {
        // Arrange
        var order = new Order
        {
            CustomerId = 1,
            TotalAmount = 1000m,
            Items = new List<OrderItem>
            {
                new OrderItem { ProductId = 1, Quantity = 2, Price = 500m }
            }
        };

        _mockOrderService
            .Setup(x => x.PlaceOrderAsync(It.IsAny<Order>()))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.PlaceOrder(order);

        // Assert
        var createdResult = Assert.IsType<CreatedAtActionResult>(result);
        Assert.Equal(nameof(OrderController.PlaceOrder), createdResult.ActionName);
        Assert.NotNull(createdResult.Value);
        
        var returnedOrder = Assert.IsType<Order>(createdResult.Value);
        Assert.Equal(1, returnedOrder.CustomerId);
        Assert.Equal(1000m, returnedOrder.TotalAmount);
        
        _mockOrderService.Verify(
            x => x.PlaceOrderAsync(It.IsAny<Order>()), 
            Times.Once()
        );
    }

    [Fact]
    public async Task PlaceOrder_WithInvalidOrder_ReturnsBadRequestResult()
    {
        // Arrange
        var invalidOrder = new Order
        {
            CustomerId = 1,
            TotalAmount = 0,
            Items = new List<OrderItem>()
        };

        _mockOrderService
            .Setup(x => x.PlaceOrderAsync(It.IsAny<Order>()))
            .ReturnsAsync(false);

        // Act
        var result = await _controller.PlaceOrder(invalidOrder);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.NotNull(badRequestResult.Value);
        
        _mockOrderService.Verify(
            x => x.PlaceOrderAsync(It.IsAny<Order>()), 
            Times.Once()
        );
    }
}
```

### NUnit Tests - Complete Implementation
```csharp
[TestFixture]
public class OrderControllerTests
{
    private Mock<IOrderService> _mockOrderService;
    private OrderController _controller;

    [SetUp]
    public void Setup()
    {
        _mockOrderService = new Mock<IOrderService>();
        _controller = new OrderController(_mockOrderService.Object);
    }

    [Test]
    public async Task PlaceOrder_WithValidOrder_ReturnsCreatedResult()
    {
        // Arrange
        var order = new Order
        {
            CustomerId = 1,
            TotalAmount = 1000m,
            Items = new List<OrderItem>
            {
                new OrderItem { ProductId = 1, Quantity = 2, Price = 500m }
            }
        };

        _mockOrderService
            .Setup(x => x.PlaceOrderAsync(It.IsAny<Order>()))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.PlaceOrder(order);

        // Assert
        Assert.IsInstanceOf<CreatedAtActionResult>(result);
        Assert.IsNotNull(result);
        
        var createdResult = (CreatedAtActionResult)result;
        Assert.That(createdResult.ActionName, Is.EqualTo(nameof(OrderController.PlaceOrder)));
        
        var returnedOrder = (Order)createdResult.Value;
        Assert.That(returnedOrder.CustomerId, Is.EqualTo(1));
        Assert.That(returnedOrder.TotalAmount, Is.EqualTo(1000m));
        
        _mockOrderService.Verify(
            x => x.PlaceOrderAsync(It.IsAny<Order>()), 
            Times.Once()
        );
    }

    [Test]
    public async Task PlaceOrder_WithInvalidOrder_ReturnsBadRequestResult()
    {
        // Arrange
        var invalidOrder = new Order
        {
            CustomerId = 1,
            TotalAmount = 0,
            Items = new List<OrderItem>()
        };

        _mockOrderService
            .Setup(x => x.PlaceOrderAsync(It.IsAny<Order>()))
            .ReturnsAsync(false);

        // Act
        var result = await _controller.PlaceOrder(invalidOrder);

        // Assert
        Assert.IsInstanceOf<BadRequestObjectResult>(result);
        Assert.IsNotNull(result);
        
        _mockOrderService.Verify(
            x => x.PlaceOrderAsync(It.IsAny<Order>()), 
            Times.Once()
        );
    }
}
```

---

## Key Implementation Notes

### 1. **Async/Await Pattern**
- All service methods are `async Task<T>` to support real database calls
- Controllers use `await` to wait for service responses
- Tests also use `async Task` for test methods

### 2. **Dependency Injection in Controllers**
```csharp
// Constructor injection
public ProductController(IProductService productService)
{
    _productService = productService;
}
```

### 3. **HTTP Status Codes**
- **200 OK** - `Ok(object)` - Resource found and returned
- **201 Created** - `CreatedAtAction(...)` - New resource created
- **400 Bad Request** - `BadRequest(object)` - Client error
- **404 Not Found** - `NotFound()` - Resource not found

### 4. **Moq Patterns**
- **Setup return value** - `.Returns(value)` or `.ReturnsAsync(value)`
- **Match any argument** - `It.IsAny<T>()`
- **Verify call** - `.Verify(expression, Times.Once())`

### 5. **Assert Differences**
- **xUnit** - `Assert.IsType<T>()`, `Assert.Equal(expected, actual)`
- **NUnit** - `Assert.IsInstanceOf<T>()`, `Assert.That(actual, Is.EqualTo(expected))`

---

## Testing Best Practices Demonstrated

✅ **Arrange-Act-Assert Pattern** - Clear test structure  
✅ **Mocking External Dependencies** - No database calls in tests  
✅ **Verify Mock Calls** - Ensure service is called correctly  
✅ **Test Both Success and Failure Paths** - Positive and negative cases  
✅ **Meaningful Test Names** - Clear intent of each test  
✅ **One Assertion Per Test** (mostly) - Focus on single behavior  

