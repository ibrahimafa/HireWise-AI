namespace HireWise_AI.Data
{
	public class MatchResult
	{
		public int Id { get; set; }

		public int CandidateId { get; set; }

		public int JobId { get; set; }

		public decimal MatchScore { get; set; }

		public string? Strengths { get; set; }

		public string? Weaknesses { get; set; }

		public string? MissingSkills { get; set; }

		public string? Recommendation { get; set; }

		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

		// Navigation Properties
		public Candidate Candidate { get; set; } = null!;
		public Job Job { get; set; } = null!;
	}
}