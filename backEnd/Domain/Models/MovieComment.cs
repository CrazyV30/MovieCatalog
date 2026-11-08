using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class MovieComment
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public Movie Movie { get; set; } = new Movie();
        public string AuthorName { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public string CreatedAt { get; set; } = string.Empty;
    }
}
