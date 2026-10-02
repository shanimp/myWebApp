namespace myProject02.Models
{
    public class Candidate
    {
        public int Id { get; set; }

        public string CandidateName { get; set; } = string.Empty;

        public string NIC { get; set; } = string.Empty;

        // Foreign Key
        public int PartyId { get; set; }

        // Navigation Property
        public Party Party { get; set; } = null!;
    }
}
