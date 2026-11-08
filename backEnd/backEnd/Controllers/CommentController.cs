using backEnd.Contracts;
using BLL.DTOs;
using BLL.Services;
using Domain.Models;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController(CommentService _commentService, IValidator<CommentRequest> _validator) : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<CommentResponse>> GetAllComments()
        {
            return Ok(_commentService.GetAll().Select(c=> new CommentResponse()
            {
                Id = c.Id,
                Text = c.Text,
                AuthorName = c.AuthorName,
                CreatedAt = c.CreatedAt
            }).ToList());
        }
        [HttpGet("id:int")]
        public ActionResult<MovieComment> GetCommentById([FromRoute] int id)
        {
            var comment = _commentService.GetById(id);
            if (comment == null) return NotFound();
            return Ok(new CommentResponse()
            {
                Id = comment.Id,
                Text = comment.Text,
                AuthorName = comment.AuthorName,
                CreatedAt = comment.CreatedAt
            });
        }
        [HttpPost]
        public ActionResult AddComment([FromBody] CommentRequest request)
        {
            var res = _validator.Validate(request);
            if (!res.IsValid) return BadRequest(res.Errors);
            var dto = new CommentDTO()
            {
                MovieId = request.MovieId,
                Text = request.Text,
                AuthorName = request.AuthorName
            };
            _commentService.Add(dto);
            return CreatedAtAction(nameof(AddComment), request);
        }
        [HttpDelete("id:int")]
        public ActionResult RemoveComment([FromRoute] int id)
        {
            if (_commentService.Remove(id)) return Ok();
            return NotFound();
        }
        [HttpPut("id:int")]
        public ActionResult UpdateComment([FromRoute] int id, [FromBody] CommentRequest request)
        {
            var comment = _commentService.GetById(id);
            if (comment == null) return NotFound();
            comment.Text = request.Text;
            if (_commentService.Update(comment)) return Ok();
            return BadRequest();
        }
    }
}
