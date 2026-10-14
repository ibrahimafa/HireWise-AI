using AutoMapper;
using HireWise_AI.Data;
using System.ComponentModel.DataAnnotations;

namespace HireWise_AI.DTOs.Candidates
{
	[AutoMap(typeof(Candidate), ReverseMap = true)]

	public class CandidateResponseDto
	{
		public int Id { get; set; }
		public string FullName { get; set; } = null!;
		public string Email { get; set; } = null!;
		public string PhoneNumber { get; set; } = null!;
		public int YearsOfExperience { get; set; }
		public string? CurrentTitle { get; set; }
		public string? Education { get; set; }
		public string? Summary { get; set; }
		public DateTime CreatedAt { get; set; }
	}
}


