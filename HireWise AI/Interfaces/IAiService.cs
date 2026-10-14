using HireWise_AI.DTOs.AI.AnalyzeResume;
using HireWise_AI.DTOs.AI.InterviewQuestions;
using HireWise_AI.DTOs.AI.MatchCandidate;

namespace HireWise_AI.Interfaces
{
	public interface IAiService
	{
		Task<ResumeAnalysisResponseDto> AnalyzeResumeAsync(string resumeText);
		Task<MatchResultResponseDto> MatchCandidateWithJobAsync(MatchCandidateRequestDto request);
		Task<InterviewQuestionsResponseDto> GenerateInterviewQuestionsAsync(GenerateInterviewQuestionsRequestDto request);

	}
}
