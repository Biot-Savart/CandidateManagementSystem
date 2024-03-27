namespace CandidateManagementSystem.Models
{
    public class Position
    {
        public int PositionId { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }

        // Navigation property for the many-to-many relationship
        public required ICollection<CandidatePosition> CandidatePositions { get; set; }
    }
}
