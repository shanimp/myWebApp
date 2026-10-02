namespace myProject02.Dto
{
    public class CreateElectionDto
    {
        public string ElectionName { get; set; } = string.Empty;

        public DateTime ElectionDate { get; set; }

        public string Description { get; set; } = string.Empty;
    }
}
