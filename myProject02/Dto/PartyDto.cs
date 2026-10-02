namespace myProject02.Dto
{
    public class PartyDto
    {
        public int Id { get; set; }

        public string PartyName { get; set; } = string.Empty;

        public string PartySymbol { get; set; } = string.Empty;

        public int CandidateCount { get; set; }
    }
}
