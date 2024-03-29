namespace CandidateManagementSystem.Models
{
    public class CandidatePosition
    {
        public int CandidateId { get; set; }
        public Candidate? Candidate { get; set; }

        public int PositionId { get; set; }
        public Position? Position { get; set; }
    }
}
