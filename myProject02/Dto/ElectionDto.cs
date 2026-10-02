namespace myProject02.Dto
{
    public class ElectionDto
    {
        public int Id { get; set; }

        public string ElectionName { get; set; } = string.Empty;

        public DateTime ElectionDate { get; set; }

        public string Description { get; set; } = string.Empty;

        public int PartyCount { get; set; }
    }
}
