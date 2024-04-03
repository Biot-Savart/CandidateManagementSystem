namespace CandidateManagementSystemV2.Server.DTOs
{
    public class CandidatePositionDto
    {
        public int CandidateId { get; set; }
        public int PositionId { get; set; }
        public PositionDto? Position { get; set; }
    }

    public class PositionDto
    {
        public int PositionId { get; set; }
        public string Title { get; set; }
    }

}
