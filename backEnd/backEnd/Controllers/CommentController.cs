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
        public ActionResult<List<CommentResponse>> GetAllComments()
        {
            return Ok(_commentService.GetAll().Select(c=>_mapper.Map<CommentResponse>(c)).ToList());
        }
        [HttpGet("{id:int}")]
        public ActionResult<MovieComment> GetCommentById([FromRoute] int id)
        {
            var comment = _commentService.GetById(id);
            if (comment == null) return NotFound();
            return Ok(_mapper.Map<CommentResponse>(comment));
        }
        [HttpPost]
        public ActionResult AddComment([FromBody] CommentRequest request)
        {
            var res = _validator.Validate(request);
            if (!res.IsValid) return BadRequest(res.Errors);
            _commentService.Add(_mapper.Map<CommentDTO>(request));
            return CreatedAtAction(nameof(AddComment), request);
        }
        [HttpDelete("{id:int}")]
        public ActionResult RemoveComment([FromRoute] int id)
        {
            if (_commentService.Remove(id)) return Ok();
            return NotFound();
        }
        [HttpPut("{id:int}")]
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
