namespace myProject02.Models
{
    public class Party
    {
        public int Id { get; set; }

        public string PartyName { get; set; } = string.Empty;

        public string PartySymbol { get; set; } = string.Empty;

        // One Party has many Candidates
        public ICollection<Candidate> Candidates { get; set; }
            = new List<Candidate>();

        public ICollection<ElectionParty> ElectionParties { get; set; }
            = new List<ElectionParty>();
    }
}
