namespace HireWise_AI.DTOs.Resumes
{
	public class ResumeResponseDto
	{
		public int Id { get; set; }
		public int CandidateId { get; set; }
		public string FileName { get; set; } = null!;
		public DateTime UploadedAt { get; set; }
	}
}
