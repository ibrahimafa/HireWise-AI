using HireWise_AI.DTOs.Candidates;

namespace HireWise_AI.Interfaces
{
	public interface ICandidateService
	{
		Task AddAsync(CreateCandidateDto createCandidateDto);
		Task<List<CandidateResponseDto>> GetAllCandidateAsync();

	}
}
