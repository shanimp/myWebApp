namespace myProject02.Models
{
    public class Election
    {
        public int Id { get; set; }

        public string ElectionName { get; set; } = string.Empty;

        public DateTime ElectionDate { get; set; }

        public string Description { get; set; } = string.Empty;

        // Many-to-many relationship
        public ICollection<ElectionParty> ElectionParties { get; set; }
            = new List<ElectionParty>();
    }
}
