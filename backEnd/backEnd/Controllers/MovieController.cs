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
        public async Task<ActionResult<List<MovieResponse>>> GetAllMovies()
        {
            var movies = (await _movieService.GetAllMovies()).Select(m => _mapper.Map<MovieResponse>(m)).ToList();
            return Ok(movies);
        }
        [HttpGet("byName/{name}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MovieResponse>> GetMovieByName([FromRoute] string name)
        {
            var movie = await _movieService.GetMovieByName(name);
            if (movie == null) return NotFound();
            return Ok(_mapper.Map<MovieResponse>(movie));
        }
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MovieResponse>> GetMovieById([FromRoute] int id)
        {
            var movie = await _movieService.GetMovieById(id);
            if (movie == null) return NotFound();
            var res = _mapper.Map<MovieResponse>(movie);
            res.Comments = movie.Comments.Count > 0 ? movie.Comments.Select(c => _mapper.Map<CommentResponse>(c)).ToList() : new List<CommentResponse>();
            return Ok(res);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> AddMovie([FromBody] CreateMovieRequest movieRequest)
        {
            var res = _validator.Validate(movieRequest);
            if (!res.IsValid) return BadRequest(res.Errors);
            if (!(await _movieService.AddMovie(_mapper.Map<MovieDTO>(movieRequest)))) return BadRequest("Movie cannot be null.");
            return CreatedAtAction(nameof(GetMovieByName), new { name = movieRequest.Title }, movieRequest);
        }
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteMovie([FromRoute] int id)
        {
            var movie = await _movieService.GetMovieById(id);
            if (movie == null) return NotFound();
            await _movieService.DeleteMovie(movie);
            return NoContent();
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdateMovie([FromRoute] int id, [FromBody] UpdateMovieRequest movieDTO)
        {
            var res = _validatorUpdate.Validate(movieDTO);
            if (!res.IsValid) return BadRequest(res.Errors);
            if ((await _movieService.GetMovieById(id)) == null) return NotFound();
            await _movieService.UpdateMovie(_mapper.Map<MovieDTO>(movieDTO));
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
