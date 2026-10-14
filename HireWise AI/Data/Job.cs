namespace HireWise_AI.Data
{
	public class Job
	{
		public int Id { get; set; }

		public string Title { get; set; } = null!;

		public string Department { get; set; } = null!;

		public string Description { get; set; } = null!;

		public string Requirements { get; set; } = null!;

		public int MinYearsExperience { get; set; }

		public string? Location { get; set; }

		public string? EmploymentType { get; set; }

		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

		// Navigation Properties
		public List<JobSkill> JobSkills { get; set; } = new();
		public List<MatchResult> MatchResults { get; set; } = new();
		public List<InterviewQuestion> InterviewQuestions { get; set; } = new();
	}
}