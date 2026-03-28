using Microsoft.AspNetCore.Mvc;
using log4net;

[ApiController]
[Route("api/[controller]")]
public class PaymentController : ControllerBase
{
    private static readonly ILog log = LogManager.GetLogger(typeof(PaymentController));

    [HttpPost]
    public IActionResult ProcessPayment()
    {
        log.Info("Payment started");

        try
        {
            Thread.Sleep(6000); // simulate delay

            log.Warn("Payment delay > 5 sec");

            return Ok("Payment success");
        }
        catch (Exception ex)
        {
            log.Error("Payment failed", ex);
            return StatusCode(500);
        }
    }
}