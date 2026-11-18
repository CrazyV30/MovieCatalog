using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class SearchFilter
    {
        public int? Id { get; set; }
        public string? Title { get; set; }
        public string? Genre { get; set; }
        public int? Year { get; set; }
        public string? Country { get; set; }
        public string? KeyWord { get; set; }
    }
}
