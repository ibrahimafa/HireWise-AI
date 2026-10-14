using HireWise_AI.DTOs.Resumes;

namespace HireWise_AI.Interfaces
{
	public interface IResumeService
	{
		Task UploadAsync(UploadResumeDto dto);

	}
}
