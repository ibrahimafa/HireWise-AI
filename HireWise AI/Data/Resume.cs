namespace HireWise_AI.Data
{
	public class Resume
	{
		public int Id { get; set; }

		public int CandidateId { get; set; }

		public string FileName { get; set; } = null!;

		public string FilePath { get; set; } = null!;

		public string? ExtractedText { get; set; }

		public DateTime UploadedAt { get; set; } = DateTime.UtcNow;

		// Navigation Property
		public Candidate Candidate { get; set; } = null!;
	}
}