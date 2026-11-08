using backEnd.Context;
using Dll.Repository;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DLL.Repository
{
    public class CommentRepository(AppDbContext _context) : IRepository<MovieComment>
    {
        public bool Add(MovieComment entity)
        {
            _context.Comments.Add(entity);
            _context.SaveChanges();
            return true;
        }

        public bool ExistId(int id) => _context.Comments.Any<MovieComment>(c => c.Id == id);

        public List<MovieComment> GetAll() => _context.Comments.ToList();

        public MovieComment? GetById(int id) => _context.Comments.FirstOrDefault(c => c.Id == id);

        public bool Remove(MovieComment entity)
        {
            _context.Comments.Remove(entity);
            _context.SaveChanges();
            return true;
        }

        public bool Update(MovieComment entity)
        {
            var com = _context.Comments.FirstOrDefault(c => c.Id == entity.Id);
            if (com == null) return false;
            com.Text = entity.Text;
            _context.SaveChanges();
            return true;
        }
    }
}
