using Microsoft.AspNetCore.Mvc;
using MovieRental.Movie;

namespace MovieRental.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MovieController : ControllerBase
    {

        private readonly IMovieFeatures _features;

        public MovieController(IMovieFeatures features)
        {
            _features = features;
        }

        [HttpGet]
        public IActionResult Get()
        {
	        return Ok(_features.GetAll().ToList());
        }

        [HttpPost]
        public IActionResult Post([FromBody] Movie.Movie movie)
        {
            //try-catch implemented for this specific endpoint. For all other endpoints there is a Global Exception Handler
            try
            {
                return Ok(_features.Save(movie));
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }
    }
}
