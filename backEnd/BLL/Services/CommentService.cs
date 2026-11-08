using BLL.DTOs;
using DLL.Repository;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;

namespace BLL.Services
{
    public class CommentService(CommentRepository _commentRepository)
    {
        public List<CommentDTO> GetAll()=> _commentRepository.GetAll().Select(c => new CommentDTO()
        {
            Id = c.Id,
            MovieId = c.MovieId,
            Text = c.Text,
            AuthorName = c.AuthorName,
            CreatedAt = c.CreatedAt
        }).ToList();
        public CommentDTO? GetById(int id)
        {
            var com = _commentRepository.GetById(id);
            if (com == null) return null;
            return new CommentDTO()
            {
                Id = com.Id,
                MovieId = com.MovieId,
                Text = com.Text,
                AuthorName = com.AuthorName,
                CreatedAt = com.CreatedAt
            };
        }
        public bool Add(CommentDTO commentDTO)
        {
            var comment = new MovieComment()
            {
                MovieId = commentDTO.MovieId,
                Text = commentDTO.Text,
                AuthorName = commentDTO.AuthorName,
                CreatedAt = DateTime.Now.ToShortDateString()
            };
            return _commentRepository.Add(comment);
        }
        public bool Remove(int id)
        {
            var com = _commentRepository.GetById(id);
            if (com == null) return false;
            return _commentRepository.Remove(com);
        }
        public bool Update(CommentDTO commentDTO)
        {
            var comment = _commentRepository.GetById(commentDTO.Id);
            if (comment == null) return false;
            comment.Text = commentDTO.Text;
            return _commentRepository.Update(comment);
        }
    }
}
