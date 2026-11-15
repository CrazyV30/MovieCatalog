using AutoMapper;
using Bll.DTOs;
using DLL.Repository;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;

namespace BLL.Services
{
    public class CommentService(CommentRepository _commentRepository, IMapper _mapper)
    {
        public List<CommentDTO> GetAll()=> _commentRepository.GetAll().Select(c => _mapper.Map<CommentDTO>(c)).ToList();
        public CommentDTO? GetById(int id)
        {
            var com = _commentRepository.GetById(id);
            if (com == null) return null;
            return _mapper.Map<CommentDTO>(com);
        }
        public bool Add(CommentDTO commentDTO)
        {
            return _commentRepository.Add(_mapper.Map<MovieComment>(commentDTO));
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
