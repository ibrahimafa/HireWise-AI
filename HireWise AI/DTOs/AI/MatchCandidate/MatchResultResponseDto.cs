namespace HireWise_AI.DTOs.AI.MatchCandidate
{
	public class MatchResultResponseDto
	{
		public int CandidateId { get; set; }
		public int JobId { get; set; }

		public decimal MatchScore { get; set; }

		public List<string> Strengths { get; set; } = new();
		public List<string> Weaknesses { get; set; } = new();
		public List<string> MissingSkills { get; set; } = new();

		public string Recommendation { get; set; } = null!;
	}
}
