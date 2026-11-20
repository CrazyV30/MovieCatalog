using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class MovieSession
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public Movie Movie { get; set; } = new Movie();
        public DateTime StartsAt { get; set; }
        public string HallName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int AvailableSeats { get; set; }
    }
}
