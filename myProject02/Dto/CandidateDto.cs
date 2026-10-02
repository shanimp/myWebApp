namespace myProject02.Dto
{
    public class CandidateDto
    {
        public int Id { get; set; }

        public string CandidateName { get; set; } = string.Empty;

        public string NIC { get; set; } = string.Empty;

        public int PartyId { get; set; }

        public string PartyName { get; set; } = string.Empty;
    }
}
