using AutoMapper;
using backEnd.Contracts;
using Bll.DTOs;
using BLL.Services;
using Domain.Models;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController(CommentService _commentService, IValidator<CommentRequest> _validator, IMapper _mapper) : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<CommentResponse>>> GetAllComments() => Ok((await _commentService.GetAll()).Select(c=>_mapper.Map<CommentResponse>(c)).ToList());
        
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MovieComment>> GetCommentById([FromRoute] int id)
        {
            var comment = await _commentService.GetById(id);
            if (comment == null) return NotFound();
            return Ok(_mapper.Map<CommentResponse>(comment));
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> AddComment([FromBody] CommentRequest request)
        {
            var res = _validator.Validate(request);
            if (!res.IsValid) return BadRequest(res.Errors);
            await _commentService.Add(_mapper.Map<CommentDTO>(request));
            return CreatedAtAction(nameof(AddComment), request);
        }
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> RemoveComment([FromRoute] int id)
        {
            if (await _commentService.Remove(id)) return Ok();
            return NotFound();
        }
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdateComment([FromRoute] int id, [FromBody] CommentRequest request)
        {
            var comment = await _commentService.GetById(id);
            if (comment == null) return NotFound();
            comment.Text = request.Text;
            if (await _commentService.Update(comment)) return Ok();
            return BadRequest();
        }
    }
}
