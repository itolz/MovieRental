using Microsoft.AspNetCore.Mvc;
using MovieRental.Movie;
using MovieRental.Rental;

namespace MovieRental.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RentalController : ControllerBase
    {

        private readonly IRentalFeatures _features;

        public RentalController(IRentalFeatures features)
        {
            _features = features;
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Rental.Rental rental)
        {
	        return Ok(await _features.Save(rental));
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Rental.Rental>), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult Get([FromQuery] string customerName)
        {
            var rentals = _features.GetRentalsByCustomerName(customerName);
            
            if (string.IsNullOrEmpty(customerName))
            {
                return BadRequest("Customer name is required.");
            }
            if (rentals == null || !rentals.Any())
            {
                return NotFound();
            }

            return Ok(rentals);
        }
    }
}
