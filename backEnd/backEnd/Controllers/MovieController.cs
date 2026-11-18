using AutoMapper;
using backEnd.Contracts;
using Bll.DTOs;
using Bll.Services;
using BLL.DTOs;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using static Bll.Services.MovieService;

namespace backEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController(MovieService _movieService, IValidator<CreateMovieRequest> _validator, IValidator<UpdateMovieRequest> _validatorUpdate, IValidator<SearchFilterContract> _validatorSearch, IMapper _mapper) : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<MovieResponse>> GetAllMovies()
        {
            var movies = _movieService.GetAllMovies().Select(m => _mapper.Map<MovieResponse>(m)).ToList();
            return Ok(movies);
        }
        [HttpGet("byName/{name}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<MovieResponse> GetMovieByName([FromRoute] string name)
        {
            var movie = _movieService.GetMovieByName(name);
            if (movie == null) return NotFound();
            return Ok(_mapper.Map<MovieResponse>(movie));
        }
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<MovieResponse> GetMovieById([FromRoute] int id)
        {
            var movie = _movieService.GetMovieById(id);
            if (movie == null) return NotFound();
            var res = _mapper.Map<MovieResponse>(movie);
            res.Comments = movie.Comments.Count > 0 ? movie.Comments.Select(c => _mapper.Map<CommentResponse>(c)).ToList() : new List<CommentResponse>();
            return Ok(res);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult AddMovie([FromBody] CreateMovieRequest movieRequest)
        {
            var res = _validator.Validate(movieRequest);
            if (!res.IsValid) return BadRequest(res.Errors);
            var movie = _movieService.AddMovie(_mapper.Map<MovieDTO>(movieRequest));
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
            _movieService.UpdateMovie(_mapper.Map<MovieDTO>(movieDTO));
            return NoContent();
        }

        [HttpGet("search")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<List<MovieResponse>>> Search([FromQuery] SearchFilterContract searchFilter)
        {
            var res = _validatorSearch.Validate(searchFilter);
            if (!res.IsValid) return BadRequest(res.Errors);
            return Ok((await _movieService.SearchByFilter(_mapper.Map<SearchFilterDTO>(searchFilter))).Select(m => _mapper.Map<MovieResponse>(m)));
        }
    }
}
