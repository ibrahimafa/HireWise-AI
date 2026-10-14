namespace HireWise_AI.DTOs.Resumes
{
	public class UploadResumeDto
	{
		public int CandidateId { get; set; }
		public IFormFile File { get; set; } = null!;
	
}
}
