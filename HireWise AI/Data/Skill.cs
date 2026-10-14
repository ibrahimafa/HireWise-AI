namespace HireWise_AI.Data
{
	public class Skill
	{
		public int Id { get; set; }

		public string Name { get; set; } = null!;

		public string? Category { get; set; }

		// Navigation Properties
		public List<CandidateSkill> CandidateSkills { get; set; } = new();
		public List<JobSkill> JobSkills { get; set; } = new();
	}
}