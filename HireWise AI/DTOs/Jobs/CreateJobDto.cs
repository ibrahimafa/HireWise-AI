using AutoMapper;
using HireWise_AI.Data;

namespace HireWise_AI.DTOs.Jobs
{
	[AutoMap(typeof(Job),ReverseMap = true)]
	public class CreateJobDto
	{
		public string Title { get; set; } = null!;
		public string Department { get; set; } = null!;
		public string Description { get; set; } = null!;
		public string Requirements { get; set; } = null!;
		public int MinYearsExperience { get; set; }
		public string? Location { get; set; }
		public string? EmploymentType { get; set; }
	}
}
