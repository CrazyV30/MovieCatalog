using backEnd.Contracts;
using Bll.DTOs;
using Bll.Services;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Bll.Services.MovieService;

namespace backEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController(MovieService _movieService, IValidator<CreateMovieRequest> _validator, IValidator<UpdateMovieRequest> _validatorUpdate) : ControllerBase
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
                Director = m.Director,
                ReleaseYear = m.ReleaseYear,
                MoviePath = m.MoviePath
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
                Director = movie.Director,
                ReleaseYear = movie.ReleaseYear,
                MoviePath = movie.MoviePath
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
                Director = movie.Director,
                ReleaseYear = movie.ReleaseYear,
                MoviePath = movie.MoviePath,
                Comments = movie.Comments.Count>0? movie.Comments.Select(c=>new CommentResponse()
                {
                    Id = c.Id,
                    AuthorName = c.AuthorName,
                    Text = c.Text,
                    CreatedAt = c.CreatedAt
                }).ToList() : new List<CommentResponse>()
            };
            return Ok(res);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult AddMovie([FromBody] CreateMovieRequest movieRequest)
        {
            var res = _validator.Validate(movieRequest);
            if (!res.IsValid) return BadRequest(res.Errors);
            var dto = new MovieDTO
            {
                Title = movieRequest.Title,
                Description = movieRequest.Description,
                PhotoPath = movieRequest.PhotoPath,
                Runtime = movieRequest.Runtime,
                Director = movieRequest.Director,
                ReleaseYear = movieRequest.ReleaseYear,
                MoviePath = movieRequest.MoviePath,
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
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult UpdateMovie([FromRoute] int id, [FromBody] UpdateMovieRequest movieDTO)
        {
            var res = _validatorUpdate.Validate(movieDTO);
            if (!res.IsValid) return BadRequest(res.Errors);
            if (_movieService.GetMovieById(id) == null) return NotFound();
            var movie = new MovieDTO
            {
                Id = id,
                Title = movieDTO.Title,
                Description = movieDTO.Description,
                PhotoPath = movieDTO.PhotoPath,
                Runtime = movieDTO.Runtime,
                Director = movieDTO.Director,
                ReleaseYear = movieDTO.ReleaseYear,
                MoviePath = movieDTO.MoviePath
            };
            _movieService.UpdateMovie(movie);
            return NoContent();
        }
    }
}
