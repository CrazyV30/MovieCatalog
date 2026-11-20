using Domain.Models;

namespace backEnd.Contracts
{
    public class SessionResponse
    {
        public int Id { get; set; }
        public string MovieTitle { get; set; } = string.Empty;
        public DateTime StartsAt { get; set; }
        public string HallName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int AvailableSeats { get; set; }
    }
}
