using System.ComponentModel.DataAnnotations;

namespace HireWise_AI.Data
{
	public class Candidate
	{
		public int Id { get; set; }
		[Required]
		[MaxLength(100)]
		public string FullName { get; set; } = null!;
		[Required]
		[EmailAddress]
		public string Email { get; set; } = null!;
		[Required]
		public string PhoneNumber { get; set; } = null!;

		public int YearsOfExperience { get; set; }

		public string? CurrentTitle { get; set; }

		public string? Education { get; set; }

		public string? Summary { get; set; }

		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

		// Navigation Properties
		public List<Resume> Resumes { get; set; } = new();
		public List<CandidateSkill> CandidateSkills { get; set; } = new();
		public List<MatchResult> MatchResults { get; set; } = new();
		public List<InterviewQuestion> InterviewQuestions { get; set; } = new();
	}
}