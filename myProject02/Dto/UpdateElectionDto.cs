namespace myProject02.Dto
{
    public class UpdateElectionDto
    {
        public string ElectionName { get; set; } = string.Empty;

        public DateTime ElectionDate { get; set; }

        public string Description { get; set; } = string.Empty;
    }
}
