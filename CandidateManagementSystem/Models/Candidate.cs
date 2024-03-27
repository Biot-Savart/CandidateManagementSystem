namespace CandidateManagementSystem.Models
{
    public class Candidate
    {
        public int CandidateId { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Phone { get; set; }

        // Navigation property for the many-to-many relationship
        public required ICollection<CandidatePosition> CandidatePositions { get; set; }
    }
}
