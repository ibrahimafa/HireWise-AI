namespace HireWise_AI.Data
{
	public class JobSkill
	{
		public int Id { get; set; }

		public int JobId { get; set; }

		public int SkillId { get; set; }

		public string? RequiredLevel { get; set; }

		public bool IsMandatory { get; set; }

		// Navigation Properties
		public Job Job { get; set; } = null!;
		public Skill Skill { get; set; } = null!;
	}
}