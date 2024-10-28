using Microsoft.AspNetCore.Mvc;

namespace MathApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MathController : ControllerBase
    {
        [HttpPost("add")]
        public IActionResult Add([FromBody] MathRequest request)
        {
            if (request == null || !request.IsValid())
                return BadRequest("Invalid input. Please ensure two numbers are provided.");

            try
            {
                // Performing the addition
                var result = request.Number1 + request.Number2;
                return Ok(new
                {
                    Result = result
                });
            }
            catch (Exception ex)
            {
                // Log error in real-world applications
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("subtract")]
        public IActionResult Subtract([FromBody] MathRequest request)
        {
            if (request == null || !request.IsValid())
                return BadRequest("Invalid input. Please ensure two numbers are provided.");

            try
            {
                // Performing the subtraction
                var result = request.Number1 - request.Number2;
                return Ok(new
                {
                    Result = result
                });
            }
            catch (Exception ex)
            {
                // Log error in real-world applications
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }

    public class MathRequest
    {
        public double? Number1 { get; set; }
        public double? Number2 { get; set; }

        // Input validation
        public bool IsValid()
        {
            return Number1.HasValue && Number2.HasValue;
        }
    }
}