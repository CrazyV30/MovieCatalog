using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.DTOs
{
    public class CommentDTO
    {

        public int Id { get; set; }
        public int MovieId { get; set; }
        public string AuthorName { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public string CreatedAt { get; set; } = string.Empty;
    }
}
