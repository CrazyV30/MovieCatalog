using Domain.Models;

namespace backEnd.Contracts
{
    public class SessionRequest
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public DateTime StartsAt { get; set; }
        public string HallName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int AvailableSeats { get; set; }
    }
}
