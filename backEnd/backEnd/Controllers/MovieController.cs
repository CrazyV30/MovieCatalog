using backEnd.Contracts;
using backEnd.DTOs;
using backEnd.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static backEnd.Services.MovieService;

namespace backEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController(MovieService _movieService) : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<MovieResponse>> GetAllMovies()
        {
            var movies = _movieService.GetAllMovies().Select(m => new MovieResponse
            {
                Id = m.Id,
                Title = m.Title,
                Description = m.Description,
                PhotoPath = m.PhotoPath,
                Runtime = m.Runtime,
                Director = m.Director
            }).ToList();
            return Ok(movies);
        }
        [HttpGet("byName/{name}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<MovieResponse> GetMovieByName([FromRoute] string name)
        {
            var movie = _movieService.GetMovieByName(name);
            if (movie == null) return NotFound();
            var res = new MovieResponse
            {
                Id = movie.Id,
                Title = movie.Title,
                Description = movie.Description,
                PhotoPath = movie.PhotoPath,
                Runtime = movie.Runtime,
                Director = movie.Director
            };
            return Ok(res);
        }
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<MovieResponse> GetMovieById([FromRoute] int id)
        {
            var movie = _movieService.GetMovieById(id);
            if (movie == null) return NotFound();
            var res = new MovieResponse
            {
                Id = movie.Id,
                Title = movie.Title,
                Description = movie.Description,
                PhotoPath = movie.PhotoPath,
                Runtime = movie.Runtime,
                Director = movie.Director
            };
            return Ok(res);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult AddMovie([FromBody] CreateMovieRequest movieRequest)
        {
            var dto = new MovieDTO
            {
                Title = movieRequest.Title,
                Description = movieRequest.Description,
                PhotoPath = movieRequest.PhotoPath,
                Runtime = movieRequest.Runtime,
                Director = movieRequest.Director
            };
            var movie = _movieService.AddMovie(dto);
            if (movie == MovieResult.NullMovieError) return BadRequest("Movie cannot be null.");
            return CreatedAtAction(nameof(GetMovieByName), new { name = movieRequest.Title }, movieRequest);
        }
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult DeleteMovie([FromRoute] int id)
        {
            var movie = _movieService.GetMovieById(id);
            if (movie == null) return NotFound();
            _movieService.DeleteMovie(movie);
            return NoContent();
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult UpdateMovie([FromRoute] int id, [FromBody] UpdateMovieRequest movieDTO)
        {
            if (_movieService.GetMovieById(id) == null) return NotFound();
            var movie = new MovieDTO
            {
                Id = id,
                Title = movieDTO.Title,
                Description = movieDTO.Description,
                PhotoPath = movieDTO.PhotoPath,
                Runtime = movieDTO.Runtime,
                Director = movieDTO.Director
            };
            var res = _movieService.UpdateMovie(movie);
            return Ok(movie);
        }
    }
}
