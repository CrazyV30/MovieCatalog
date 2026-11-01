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
        public ActionResult GetAllMovies()
        {
            var movies = _movieService.GetAllMovies();
            return Ok(movies);
        }
        [HttpGet("{name}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult GetMovieByName(string name)
        {
            var movie = _movieService.GetMovieByName(name);
            if (movie == null) return NotFound();
            return Ok(movie);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult AddMovie(MovieDTO movieDTO)
        {
            var movie = _movieService.AddMovie(movieDTO);
            switch(movie)
            {
                case MovieResult.TitleError:
                    return BadRequest("Title is required and cannot be repeated.");
                case MovieResult.DescriptionError:
                    return BadRequest("Description is required.");
                case MovieResult.UrlError:
                    return BadRequest("Photo URL is required.");
                case MovieResult.RuntimeError:
                    return BadRequest("Runtime is required.");
                case MovieResult.DirectorError:
                    return BadRequest("Director is required.");
                case MovieResult.NullMovieError:
                    return BadRequest("Movie cannot be null.");
            }
            return CreatedAtAction(nameof(GetMovieByName), new { name = movieDTO.Title }, movieDTO);
        }
        [HttpDelete("{name}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult DeleteMovie(string name)
        {
            var movie = _movieService.GetMovieByName(name);
            if (movie == null) return NotFound();
            _movieService.DeleteMovie(movie);
            return NoContent();
        }

    }
}
