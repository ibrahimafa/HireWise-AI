using HireWise_AI.DTOs.Candidates;
using HireWise_AI.DTOs.Jobs;

namespace HireWise_AI.Interfaces
{
	public interface IJobService
	{
		Task AddAsync(CreateJobDto createJobDto);
		Task<List<JobResponseDto>> GetAllJobsAsync();
		Task<JobResponseDto?> GetByIdAsync(int id);

	}
}
