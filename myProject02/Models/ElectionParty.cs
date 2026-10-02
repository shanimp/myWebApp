namespace myProject02.Models
{
    public class ElectionParty
    {
        public int ElectionId { get; set; }

        public int PartyId { get; set; }

        // Navigation properties
        public Election Election { get; set; } = null!;

        public Party Party { get; set; } = null!;
    }
}
