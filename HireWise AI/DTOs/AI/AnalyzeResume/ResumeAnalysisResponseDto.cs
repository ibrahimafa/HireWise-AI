namespace HireWise_AI.DTOs.AI.AnalyzeResume
{
	public class ResumeAnalysisResponseDto
	{
		public string FullName { get; set; } = null!;
		public string CurrentTitle { get; set; } = null!;
		public int YearsOfExperience { get; set; }

		public List<string> Skills { get; set; } = new();

		public List<string> Strengths { get; set; } = new();
		public List<string> Weaknesses { get; set; } = new();

		public string Summary { get; set; } = null!;
	}
}
