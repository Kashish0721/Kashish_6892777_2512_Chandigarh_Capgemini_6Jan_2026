using Microsoft.AspNetCore.Mvc;
using log4net;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private static readonly ILog log = LogManager.GetLogger(typeof(OrderController));

    [HttpPost]
    public IActionResult CreateOrder()
    {
        log.Info("Order creation started");

        try
        {
            // simulate success
            log.Info("Order created successfully");
            return Ok("Order placed");
        }
        catch (Exception ex)
        {
            log.Error("Order failed", ex);
            return StatusCode(500);
        }
    }
}