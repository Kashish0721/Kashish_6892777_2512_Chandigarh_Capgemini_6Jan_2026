using Microsoft.AspNetCore.Mvc;
using log4net;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private static readonly ILog log = LogManager.GetLogger(typeof(ProductController));

    [HttpGet("{id}")]
    public IActionResult GetProduct(int id)
    {
        log.Info($"Fetching product {id}");

        if (id != 1)
        {
            log.Warn("Product not found");
            return NotFound();
        }

        return Ok("Product Found");
    }
}