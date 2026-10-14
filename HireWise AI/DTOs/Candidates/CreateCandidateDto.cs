using AutoMapper;
using HireWise_AI.Data;
using System.ComponentModel.DataAnnotations;

namespace HireWise_AI.DTOs.Candidates
{
	[AutoMap(typeof(Candidate), ReverseMap = true)]

	public class CreateCandidateDto
	{
		[Required]
		public string FullName { get; set; } = null!;

		[Required]
		public string Email { get; set; } = null!;
		[Required]
		public string PhoneNumber { get; set; } = null!;

		[Required]
		public int YearsOfExperience { get; set; }

		[Required]
		public string? CurrentTitle { get; set; }

		[Required]
		public string? Education { get; set; }

		[Required]
		public string? Summary { get; set; }

		}
}
