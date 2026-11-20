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
        public async Task<List<CommentDTO>> GetAll()=> (await _commentRepository.GetAll()).Select(c => _mapper.Map<CommentDTO>(c)).ToList();
        public async Task<CommentDTO?> GetById(int id)
        {
            var com = await _commentRepository.GetById(id);
            if (com == null) return null;
            return _mapper.Map<CommentDTO>(com);
        }
        public async Task<bool> Add(CommentDTO commentDTO)
        {
            await _commentRepository.Add(_mapper.Map<MovieComment>(commentDTO));
            await _commentRepository.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Remove(int id)
        {
            var com = await _commentRepository.GetById(id);
            if (com == null) return false;
            _commentRepository.Remove(com);
            await _commentRepository.SaveChangesAsync();
            return true; 
        }
        public async Task<bool> Update(CommentDTO commentDTO)
        {
            var comment = await _commentRepository.GetById(commentDTO.Id);
            if (comment == null) return false;
            comment.Text = commentDTO.Text;
            await _commentRepository.Update(comment);
            await _commentRepository.SaveChangesAsync();
            return true;
        }
    }
}
