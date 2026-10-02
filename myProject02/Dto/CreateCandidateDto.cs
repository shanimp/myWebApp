namespace myProject02.Dto
{
    public class CreateCandidateDto
    {
        public string CandidateName { get; set; } = string.Empty;

        public string NIC { get; set; } = string.Empty;

        public int PartyId { get; set; }
    }
}
