namespace HireWise_AI.Data
{
	public class CandidateSkill
	{
		public int Id { get; set; }

		public int CandidateId { get; set; }

		public int SkillId { get; set; }

		public string? Level { get; set; }

		public int? YearsUsed { get; set; }

		// Navigation Properties
		public Candidate Candidate { get; set; } = null!;
		public Skill Skill { get; set; } = null!;
	}
}