namespace HireWise_AI.Data
{
	public class InterviewQuestion
	{
		public int Id { get; set; }

		public int CandidateId { get; set; }

		public int JobId { get; set; }

		public string Question { get; set; } = null!;

		public string? Category { get; set; }

		public string? Difficulty { get; set; }

		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

		// Navigation Properties
		public Candidate Candidate { get; set; } = null!;
		public Job Job { get; set; } = null!;
	}
}