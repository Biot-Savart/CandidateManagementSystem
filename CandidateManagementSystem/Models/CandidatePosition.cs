namespace CandidateManagementSystem.Models
{
    public class CandidatePosition
    {
        public int CandidateId { get; set; }
        public required Candidate Candidate { get; set; }

        public int PositionId { get; set; }
        public required Position Position { get; set; }
    }
}
